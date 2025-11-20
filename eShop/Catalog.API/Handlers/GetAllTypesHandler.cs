using Catalog.API.Mappers;
using Catalog.API.Queries;
using Catalog.API.Repositories.Interface;
using Catalog.API.Responses;
using MediatR;

namespace Catalog.API.Handlers;

public class GetAllTypesHandler : IRequestHandler<GetAllTypesQuery, IList<TypesResponse>>
{
    private readonly ITypeRepository _typeRepository;

    public GetAllTypesHandler(ITypeRepository typeRepository)
    {
        _typeRepository = typeRepository;
    }
    public async Task<IList<TypesResponse>> Handle(GetAllTypesQuery request, CancellationToken cancellationToken)
    {
        var typesList = await _typeRepository.GetAllTypesAsync();
        return typesList.ToResponseList();
    }
}

