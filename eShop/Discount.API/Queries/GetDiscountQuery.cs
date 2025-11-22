using Discount.API.DTOs;
using MediatR;

namespace Discount.API.Queries;

public record GetDiscountQuery(string productName) : IRequest<CouponDto>;
