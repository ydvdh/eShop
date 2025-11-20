using Catalog.API.Mappers;
using Catalog.API.Queries;
using Catalog.API.Repositories.Interface;
using Catalog.API.Responses;
using MediatR;

namespace Catalog.API.Handlers;

public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductAsync(request.Id);
        var productResponse = product.ToResponse();
        return productResponse;
    }
}
