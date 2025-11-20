using Catalog.API.Commands;
using Catalog.API.Repositories.Interface;
using MediatR;

namespace Catalog.API.Handlers;

public class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdCommand, bool>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductByIdHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<bool> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        return await _productRepository.DeleteProductAsync(request.Id);
    }
}
