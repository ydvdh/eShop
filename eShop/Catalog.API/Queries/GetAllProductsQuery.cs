using Catalog.API.Responses;
using Catalog.API.Specification;
using MediatR;

namespace Catalog.API.Queries;

public record GetAllProductsQuery(CatalogSpecParams CatalogSpecParams) : IRequest<Pagination<ProductResponse>>
{
}

