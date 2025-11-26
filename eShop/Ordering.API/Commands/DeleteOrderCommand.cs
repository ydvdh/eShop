using MediatR;

namespace Ordering.API.Commands;

public record DeleteOrderCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public Guid CorrelationId { get; set; }
}
