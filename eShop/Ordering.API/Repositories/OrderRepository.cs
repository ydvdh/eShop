using Microsoft.EntityFrameworkCore;
using Ordering.API.Data;
using Ordering.API.Entities;
using Ordering.API.Repositories.Interfaces;

namespace Ordering.API.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(OrderContext orderContext) : base(orderContext)
    {
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        var orderList = await _orderContext.Orders.AsNoTracking() .Where(o => o.UserName == userName).ToListAsync();
        return orderList;
    }
}
