using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    
      /// <summary>
    /// Creates a new entity in the repository
    /// </summary>
    /// <param name="entity">The entity to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created entity</returns>
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a entity by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the entity</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The entity if found, null otherwise</returns>
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);


    /// <summary>
    /// Deletes a entity from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the entity was deleted, false if not found</returns>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>An IQueryable of TEntitys</returns>
    IQueryable<TEntity> GetAll(CancellationToken cancellationToken = default);
    /// <summary>
    /// Retrieves all entities
    /// </summary>
    /// <param name="orderBy">The property to order the results by (e.g., "entity name")</param>
    /// <param name="orderDir">The direction of the ordering, either "asc" for ascending or "desc" for descending (default: "desc")</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation</param>
    /// <returns>An IQueryable of TEntities</returns>
    IQueryable<TEntity> GetAll(string orderBy, string? orderDir = "desc",CancellationToken cancellationToken = default);
    /// <summary>
    /// Updates a new entity in the repository
    /// </summary>
    /// <param name="entity">The entity to update</param>
    /// <returns>The created entity</returns>
    void Update(TEntity entity);

    IQueryable<TEntity> Filter(IQueryable<TEntity>queryable, string property, string? filter);
    IQueryable<TEntity> NumericFilter(IQueryable<TEntity> queryable, string property, decimal? filter);
    
    IOrderedQueryable<TEntity> OrderBy(string expression, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    
}