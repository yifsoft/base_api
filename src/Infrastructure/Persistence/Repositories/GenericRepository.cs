using Application.Interfaces.Repositories;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbContext context;
    protected DbSet<TEntity> entity => context.Set<TEntity>();

    public GenericRepository(DbContext context)
    {
        this.context = context ?? throw new ArgumentException(nameof(context));
    }

    #region Add Methods
    public virtual bool Add(TEntity entity)
    {
        this.entity.Add(entity);
        var result = context.SaveChanges();
        return result == 1;
    }

    public virtual async Task<bool> AddAsync(TEntity entity)
    {
        await this.entity.AddAsync(entity);
        var result = await context.SaveChangesAsync();
        return result == 1;
    }
    #endregion

    #region Update Methods
    public virtual bool Update(TEntity entity)
    {
        this.entity.Attach(entity);
        context.Entry(entity).State = EntityState.Modified;

        var result = context.SaveChanges();
        return result == 1;
    }

    public virtual async Task<bool> UpdateAsync(TEntity entity)
    {
        this.entity.Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
        var result = await context.SaveChangesAsync();

        return result == 1;
    }
    #endregion

    #region Delete Methods
    public bool Delete(int id)
    {
        throw new NotImplementedException();
        //TODO:yapılacak
    }

    public Task<bool> DeleteAsync(int id)
    {
        throw new NotImplementedException();
        //TODO:yapılacak
    }
    #endregion

    #region Get Methods
    public IQueryable<TEntity?> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        var query = entity.AsQueryable();

        if (predicate != null)
            query = query.Where(predicate);

        query = ApplyIncludes(query, includes);

        if (noTracking)
            query = query.AsNoTracking();

        return query;
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await entity.FindAsync(id);

        if (result == null)
            return null;

        foreach (var item in includes)
            context.Entry(result).Reference(item).Load();

        if (noTracking)
            context.Entry(result).State = EntityState.Detached;

        return result;
    }


    public virtual async Task<TEntity?> FindAsync(object keyValue, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await entity.FindAsync(keyValue);

        if (result == null)
            return null;

        foreach (var item in includes)
            context.Entry(result).Reference(item).Load();

        if (noTracking)
            context.Entry(result).State = EntityState.Detached;

        return result;
    }

    public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await entity.FirstOrDefaultAsync(predicate);

        if (result == null)
            return null;

        foreach (var item in includes)
            context.Entry(result).Reference(item).Load();

        if (noTracking)
            context.Entry(result).State = EntityState.Detached;

        return result;
    }

    public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate = null, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = entity;

        if (predicate != null)
            query = query.Where(predicate);

        query = ApplyIncludes(query, includes);

        if (orderBy != null)
            query = orderBy(query);

        if (noTracking)
            query = query.AsNoTracking();

        return await query.ToListAsync();
    }

    public virtual IQueryable<TEntity?> AsQueryable() => entity.AsQueryable();

    #endregion

    #region ApplyIncludes
    private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
    {
        if (includes != null)
            foreach (var include in includes)
                query = query.Include(include);

        return query;
    }
    #endregion


}

