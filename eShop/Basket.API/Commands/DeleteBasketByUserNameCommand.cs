using MediatR;

namespace Basket.API.Commands;

public record DeleteBasketByUserNameCommand(string userName) : IRequest<Unit>;
