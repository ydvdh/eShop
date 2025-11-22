using Discount.API.Commands;
using Discount.API.DTOs;
using Discount.API.Extensions;
using Discount.API.Mappers;
using Discount.API.Repositories.Interfaces;
using Grpc.Core;
using MediatR;

namespace Discount.API.Handlers;

public class CreateDiscountHandler : IRequestHandler<CreateDiscountCommand, CouponDto>
{
    private readonly IDiscountRepository _discountRepository;

    public CreateDiscountHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }
    public async Task<CouponDto> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        // Input Validations
        var validationErrors = new Dictionary<string, string>();
        if (string.IsNullOrWhiteSpace(request.ProductName))
            validationErrors["ProductName"] = "Product Name must not be empty.";
        if (string.IsNullOrWhiteSpace(request.Description))
            validationErrors["Description"] = "Product Description must not be empty.";
        if (request.Amount <= 0)
            validationErrors["Amount"] = "Amount must be greater than zero.";

        if (validationErrors.Any())
            throw GrpcException.CreateValidationException(validationErrors);

        //Convert to Entity
        var coupon = request.ToEntity();

        //Save to Db
        var created = await _discountRepository.CreateDiscount(coupon);
        if (!created)
        {
            throw new RpcException(new Status(StatusCode.Internal, $"Could not create discount for product: {request.ProductName}"));
        }

        //Return DTO
        return coupon.ToDto();
    }
}
