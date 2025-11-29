namespace EventBus.Message.Events;

public class PaymentFailedEvent : IntegrationBaseEvent
{
    public int OrderId { get; set; }
    public string UserName { get; set; }
    public string Reason { get; set; }
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
}
