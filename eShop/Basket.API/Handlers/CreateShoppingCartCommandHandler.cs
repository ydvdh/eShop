using Basket.API.Commands;
using Basket.API.GrpcServices;
using Basket.API.Mappers;
using Basket.API.Repositories.Interfaces;
using Basket.API.Responses;
using MediatR;

namespace Basket.API.Handlers;

public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    private readonly IBasketRepository _basketRepository;
    private readonly DiscountGrpcService _discountGrpcService;

    public CreateShoppingCartCommandHandler(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
    {
        _basketRepository = basketRepository;
        _discountGrpcService = discountGrpcService;
    }

    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        //Apply discount for each item in the cart
        foreach (var item in request.Items)
        {
            var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
            item.Price -= (decimal)coupon.Amount;
        }
        // Convert Command to domain entity
        var shoppingCartEntity = request.ToEntity();

        //Save to Redis
        var updatedCart = await _basketRepository.UpdateBasket(shoppingCartEntity);

        //Convert back to Response
        return updatedCart.ToResponse();
    }
}
