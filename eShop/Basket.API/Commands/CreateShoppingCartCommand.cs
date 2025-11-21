using Basket.API.DTOs;
using Basket.API.Responses;
using MediatR;

namespace Basket.API.Commands;

public record CreateShoppingCartCommand(string UserName, List<CreateShoppingCartItemDto> Items) : IRequest<ShoppingCartResponse>;
