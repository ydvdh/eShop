using Discount.API.DTOs;
using MediatR;

namespace Discount.API.Commands;

public record UpdateDiscountCommand(int Id, string ProductName, string Description, double Amount) : IRequest<CouponDto>;
