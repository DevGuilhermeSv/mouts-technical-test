using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Ambev.DeveloperEvaluation.ORM.Common;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DefaultContext _context;
    protected DbSet<TEntity> DbSet { get; set; }

    public Repository(DefaultContext context)
    {
        this._context = context;
        DbSet = _context.Set<TEntity>();
    }
     /// <summary>
    /// Creates a new user in the database
    /// </summary>
    /// <param name="user">The user to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    public async Task<TEntity> CreateAsync(TEntity user, CancellationToken cancellationToken = default)
    {
        await DbSet.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    /// <summary>
    /// Retrieves a user by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the user</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet.FirstOrDefaultAsync(o=> o.Id == id, cancellationToken);
    }

    /// <summary>
    /// Deletes a user from the database
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the user was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await GetByIdAsync(id, cancellationToken);
        if (user == null)
            return false;

        DbSet.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public IQueryable<TEntity> GetAll(CancellationToken cancellationToken = default)
    {
        return DbSet;
    }

    public IQueryable<TEntity> GetAll(string orderBy, string? orderDir = "desc", CancellationToken cancellationToken = default)
    {
        orderDir ??= "desc";
        if (orderDir is not ("desc" or "asc"))
        {
            throw new ArgumentException("Invalid order direction. Use 'asc' or 'desc'.");
        }
        return DbSet.OrderBy($"{orderBy} {orderDir}");
    }
    
    public void Update(TEntity user)
    {
         DbSet.Update(user);
    }

    public IQueryable<TEntity> Filter(IQueryable<TEntity> queryable, string property, string? filter)
    {
        return queryable.Filter(property, filter);
    }

    public IQueryable<TEntity> NumericFilter(IQueryable<TEntity> queryable, string property, decimal? filter)
    {
        return queryable.FilterNumeric(property, filter);
    }

    public IQueryable<TEntity> DateFilter(IQueryable<TEntity> queryable, string property, DateTime? filter)
    {
        
        return queryable.FilterDate(property, filter);
    }
    public IOrderedQueryable<TEntity> OrderBy(string expression, CancellationToken cancellationToken = default)
    {
        return DbSet.OrderBy(expression, cancellationToken);

    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}