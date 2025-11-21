using Basket.API.DTOs;
using MediatR;

namespace Basket.API.Commands;

public record CheckoutBasketCommand(BasketCheckoutDto Dto) : IRequest<Unit>;