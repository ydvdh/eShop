using Discount.API.Commands;
using Discount.API.Extensions;
using Discount.API.Repositories.Interfaces;
using MediatR;

namespace Discount.API.Handlers;

public class DeleteDiscountHandler : IRequestHandler<DeleteDiscountCommand, bool>
{
    private readonly IDiscountRepository _discountRepository;

    public DeleteDiscountHandler(IDiscountRepository discountRepository)
    {
        _discountRepository = discountRepository;
    }
    public async Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.ProductName))
        {
            var validationErrors = new Dictionary<string, string>
                {
                    { "ProductName", "Product name must not be empty."}
                };
            throw GrpcException.CreateValidationException(validationErrors);
        }

        var deleted = await _discountRepository.DeleteDiscount(request.ProductName);
        return deleted;
    }
}
