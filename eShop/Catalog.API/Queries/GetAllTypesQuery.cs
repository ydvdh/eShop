using Catalog.API.Responses;
using MediatR;

namespace Catalog.API.Queries;

public record GetAllTypesQuery : IRequest<IList<TypesResponse>>
{
}

