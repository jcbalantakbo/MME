using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MME.Persistence.Interfaces;

public interface ICrudRepository<TEntity> where TEntity : class
{
    Task InsertAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<TEntity> GetAsync(string id, params string[] includes);
    IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params string[] includes);
    Task<int> SaveChangesAsync();
    public DbContext Context { get; }
}

