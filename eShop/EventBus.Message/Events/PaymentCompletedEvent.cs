namespace EventBus.Message.Events;

public class PaymentCompletedEvent : IntegrationBaseEvent
{
    public int OrderId { get; set; }
    public string UserName { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime TimeStamp { get; set; } = DateTime.UtcNow;
}