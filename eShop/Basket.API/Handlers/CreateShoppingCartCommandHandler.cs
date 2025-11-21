using Basket.API.Commands;
using Basket.API.Mappers;
using Basket.API.Repositories.Interfaces;
using Basket.API.Responses;
using MediatR;

namespace Basket.API.Handlers;

public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
{
    private readonly IBasketRepository _basketRepository;

    public CreateShoppingCartCommandHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        // Convert Command to domain entity
        var shoppingCartEntity = request.ToEntity();

        //Save to Redis
        var updatedCart = await _basketRepository.UpdateBasket(shoppingCartEntity);

        //Convert back to Response
        return updatedCart.ToResponse();
    }
}
