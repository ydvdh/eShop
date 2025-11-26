using MediatR;
using Ordering.API.Commands;
using Ordering.API.Entities;
using Ordering.API.Exceptions;
using Ordering.API.Mappers;
using Ordering.API.Repositories.Interfaces;

namespace Ordering.API.Handlers;

public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<UpdateOrderHandler> _logger;

    public UpdateOrderHandler(IOrderRepository orderRepository, ILogger<UpdateOrderHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }
    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);
        if (orderToUpdate == null)
        {
            throw new OrderNotFoundException(nameof(Order), request.Id);
        }

        orderToUpdate.MapUpdate(request);
        await _orderRepository.UpdateAsync(orderToUpdate);
     
        _logger.LogInformation($"Order {orderToUpdate.Id} is successfully updated.");
        return Unit.Value;
    }
}
