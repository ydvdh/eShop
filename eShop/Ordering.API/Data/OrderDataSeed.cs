using Ordering.API.Entities;

namespace Ordering.API.Data;

public class OrderDataSeed
{
    public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderDataSeed> logger)
    {
        if (!orderContext.Orders.Any())
        {
            orderContext.Orders.AddRange(GetOrders());
            await orderContext.SaveChangesAsync();
            logger.LogInformation($"Ordering Database: {typeof(OrderContext).Name} seeded!!!");
        }
    }

    private static IEnumerable<Order> GetOrders()
    {
        return new List<Order>
            {
                new()
                {
                    UserName = "davdh",
                    FirstName = "dav",
                    LastName = "dh",
                    EmailAddress = "davdh@gmail.net",
                    AddressLine = "Dallas",
                    Country = "USA",
                    TotalPrice = 750,
                    State = "TX",
                    ZipCode = "560001",

                    CardName = "Credit",
                    CardNumber = "7838678",
                    CreatedBy = "Dav",
                    Expiration = "11/25",
                    Cvv = "345",
                    PaymentMethod = 1,
                    LastModifiedBy = "dav",
                    LastModifiedDate = new DateTime(),
                }
            };
    }
}
