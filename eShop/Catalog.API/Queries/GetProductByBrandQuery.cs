using Catalog.API.Responses;
using MediatR;

namespace Catalog.API.Queries;

public record GetProductByBrandQuery(string BrandName) : IRequest<IList<ProductResponse>>;
