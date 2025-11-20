using Catalog.API.Responses;
using MediatR;

namespace Catalog.API.Queries;

public record GetProductByNameQuery(string Name) : IRequest<IList<ProductResponse>>;
