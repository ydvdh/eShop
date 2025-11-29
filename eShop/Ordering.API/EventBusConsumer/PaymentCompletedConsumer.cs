using EventBus.Message.Events;
using MassTransit;
using Ordering.API.Repositories.Interfaces;

namespace Ordering.API.EventBusConsumer;

public class PaymentCompletedConsumer : IConsumer<PaymentCompletedEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ILogger<PaymentCompletedConsumer> _logger;

    public PaymentCompletedConsumer(IOrderRepository orderRepository, ILogger<PaymentCompletedConsumer> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
    {
        var order = await _orderRepository.GetByIdAsync(context.Message.OrderId);
        if (order == null)
        {
            _logger.LogWarning("Order not found for Id: {OrderId} and {CorrelationId}", context.Message.OrderId, context.Message.CorrelationId);
            return;
        }

        order.Status = Entities.OrderStatus.Completed;
        await _orderRepository.UpdateAsync(order);
        _logger.LogInformation("Order Id {OrderId} marked as Paid", context.Message.OrderId);
    }
}
