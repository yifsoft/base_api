using Domain.Common;
using System.Linq.Expressions;

namespace Application.Interfaces.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<bool> AddAsync(TEntity entity);
    bool Add(TEntity entity);


    Task<bool> UpdateAsync(TEntity entity);
    bool Update(TEntity entity);


    Task<bool> DeleteAsync(int id);
    bool Delete(int id);

    IQueryable<TEntity?> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity?> GetByIdAsync(int id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes);
    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes);
    IQueryable<TEntity?> AsQueryable();

}

