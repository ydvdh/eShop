using MediatR;

namespace Catalog.API.Commands;

public record DeleteProductByIdCommand(string Id) : IRequest<bool>;
