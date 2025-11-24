using Discount.API.DTOs;
using Discount.API.Mappers;
using Discount.API.Queries;
using Discount.API.Repositories.Interfaces;
using Grpc.Core;
using MediatR;

namespace Discount.API.Handlers;

public class GetDiscountHandler  : IRequestHandler<GetDiscountQuery, CouponDto>
{
    private readonly IDiscountRepository _discountRepository;

    public GetDiscountHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }
    public async Task<CouponDto> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        //Validate
        if (string.IsNullOrWhiteSpace(request.productName))
        {
            var validationErrors = new Dictionary<string, string>
                {
                    { "ProductName", "Product name must not be empty."}
                };
        }
        //Fetch
        var coupon = await _discountRepository.GetDiscount(request.productName);
        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount for the Product Name = {request.productName} not found"));
        }
        //Mapping 
        return coupon.ToDto();
    }
}
