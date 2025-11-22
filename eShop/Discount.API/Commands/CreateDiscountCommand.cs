using Discount.API.DTOs;
using MediatR;

namespace Discount.API.Commands;

public record CreateDiscountCommand(string ProductName, string Description, double Amount) : IRequest<CouponDto>;
