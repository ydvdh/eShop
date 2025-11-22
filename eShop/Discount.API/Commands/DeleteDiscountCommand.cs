using MediatR;

namespace Discount.API.Commands;

public record DeleteDiscountCommand(string ProductName) : IRequest<bool>;
