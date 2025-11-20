using Catalog.API.Mappers;
using Catalog.API.Queries;
using Catalog.API.Repositories.Interface;
using Catalog.API.Responses;
using MediatR;

namespace Catalog.API.Handlers;

public class GetAllBrandsHandler : IRequestHandler<GetAllBrandsQuery, IList<BrandResponse>>
{
    private readonly IBrandRepository _brandRepository;

    public GetAllBrandsHandler(IBrandRepository brandRepository)
    {
        _brandRepository = brandRepository;
    }
    public async Task<IList<BrandResponse>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var brandList = await _brandRepository.GetAllBrandsAsync();
        return brandList.ToResponseList();
    }
}

