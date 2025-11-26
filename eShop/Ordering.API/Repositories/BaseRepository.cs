using MassTransit;
using Microsoft.EntityFrameworkCore;
using Ordering.API.Data;
using Ordering.API.Entities;
using Ordering.API.Repositories.Interfaces;
using System.Linq.Expressions;

namespace Ordering.API.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly OrderContext _orderContext;

    public BaseRepository(OrderContext orderContext)
    {
        _orderContext = orderContext;
    }
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _orderContext.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync(Expression<Func<T, bool>> predicate)
    {
        return await _orderContext.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _orderContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<T> AddAsync(T entity)
    {
        _orderContext.Set<T>().Add(entity);
        await _orderContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _orderContext.Entry(entity).State = EntityState.Modified;
        await _orderContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _orderContext.Set<T>().Remove(entity);
        await _orderContext.SaveChangesAsync();
    }
}
