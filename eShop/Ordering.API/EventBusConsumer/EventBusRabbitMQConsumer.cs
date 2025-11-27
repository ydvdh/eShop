using EventBus.Message.Events;
using MassTransit;
using MediatR;
using Ordering.API.Mappers;

namespace Ordering.API.EventBusConsumer;

public class EventBusRabbitMQConsumer : IConsumer<BasketCheckoutEvent>
{
    private readonly IMediator _mediator;
    private readonly ILogger<EventBusRabbitMQConsumer> _logger;

    public EventBusRabbitMQConsumer(IMediator mediator, ILogger<EventBusRabbitMQConsumer> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }
    public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
    {
        using var scope = _logger.BeginScope("Consuming Basket Checkout Event for {CorrelationId}", context.Message.CorrelationId);
        var command = context.Message.ToCheckoutOrderCommand();
        var result = await _mediator.Send(command);
        _logger.LogInformation("Basket Checkout Event completed Successfully.");
    }
}
