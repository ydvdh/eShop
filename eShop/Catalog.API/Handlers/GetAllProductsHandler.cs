using Catalog.API.Mappers;
using Catalog.API.Queries;
using Catalog.API.Repositories.Interface;
using Catalog.API.Responses;
using Catalog.API.Specification;
using MediatR;

namespace Catalog.API.Handlers;

public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<Pagination<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var productList = await _productRepository.GetProducts(request.CatalogSpecParams);
        var productResponseList = productList.ToResponse();
        return productResponseList;
    }
}

