using MediatR;
using Ordering.API.Commands;
using Ordering.API.Mappers;
using Ordering.API.Repositories.Interfaces;

namespace Ordering.API.Handlers;

public class CheckoutOrderHandler : IRequestHandler<CheckoutOrderCommand, int>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<CheckoutOrderHandler> _logger;

    public CheckoutOrderHandler(IOrderRepository orderRepository, ILogger<CheckoutOrderHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }
    public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
    {
        var orderEntity = request.ToEntity();
        var generatedOrder = await _orderRepository.AddAsync(orderEntity);

        _logger.LogInformation($"Order with Id: {generatedOrder.Id} successfully created with outbox message and CorrelationId: {request.CorrelationId}.");
        return generatedOrder.Id;
    }
}
