using Catalog.API.Responses;
using MediatR;

namespace Catalog.API.Queries;

public record GetAllBrandsQuery : IRequest<IList<BrandResponse>>
{
}

