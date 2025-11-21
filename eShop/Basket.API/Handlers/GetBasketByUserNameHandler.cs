using Basket.API.Mappers;
using Basket.API.Queries;
using Basket.API.Repositories.Interfaces;
using Basket.API.Responses;
using MediatR;

namespace Basket.API.Handlers;

public class GetBasketByUserNameHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
{
    private readonly IBasketRepository _basketRepository;

    public GetBasketByUserNameHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }
    public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
    {
        var shoppingCart = await _basketRepository.GetBasket(request.UserName);
        if (shoppingCart == null)
        {
            return new ShoppingCartResponse(request.UserName)
            {
                Items = new List<ShoppingCartItemResponse>()
            };
        }
        return shoppingCart.ToResponse();
    }
}
