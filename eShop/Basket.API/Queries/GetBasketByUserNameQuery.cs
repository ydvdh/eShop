using Basket.API.Responses;
using MediatR;

namespace Basket.API.Queries;
public record GetBasketByUserNameQuery(string UserName) : IRequest<ShoppingCartResponse>;
