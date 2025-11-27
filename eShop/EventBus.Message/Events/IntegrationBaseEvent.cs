namespace EventBus.Message.Events;

public class IntegrationBaseEvent
{
    public Guid CorrelationId { get; set; }
    public DateTime CreationDate { get; set; }

    public IntegrationBaseEvent()
    {
        CorrelationId = Guid.NewGuid();
        CreationDate = DateTime.UtcNow;
    }

    public IntegrationBaseEvent( Guid correlationId, DateTime creationDate)
    {
        CorrelationId = correlationId;
        CreationDate = creationDate;
    }
}
