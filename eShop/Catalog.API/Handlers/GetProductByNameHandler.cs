using Catalog.API.Mappers;
using Catalog.API.Queries;
using Catalog.API.Repositories.Interface;
using Catalog.API.Responses;
using MediatR;

namespace Catalog.API.Handlers;

public class GetProductByNameHandler : IRequestHandler<GetProductByNameQuery, IList<ProductResponse>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByNameHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<IList<ProductResponse>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
    {
        var productList = await _productRepository.GetProductByNameAsync(request.Name);
        var productResponseList = productList.ToResponseList().ToList();
        return productResponseList;
    }
}
