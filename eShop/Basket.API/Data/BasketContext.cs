using Basket.API.Data.Interfaces;
using StackExchange.Redis;

namespace Basket.API.Data;

public class BasketContext : IBasketContext
{
    private readonly ConnectionMultiplexer _redisconnection;

    public BasketContext(ConnectionMultiplexer redisconnection)
    {
        _redisconnection = redisconnection;
        Redis = redisconnection.GetDatabase();
    }

    public IDatabase Redis { get; }
}

