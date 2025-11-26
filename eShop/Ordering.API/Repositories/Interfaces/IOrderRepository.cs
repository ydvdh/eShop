using Ordering.API.Entities;

namespace Ordering.API.Repositories.Interfaces;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
}
