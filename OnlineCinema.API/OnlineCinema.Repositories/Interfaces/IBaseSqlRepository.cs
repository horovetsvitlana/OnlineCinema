using System.Linq.Expressions;

namespace OnlineCinema.Repository.Interfaces
{
    /// <summary>
    /// The IBaseSqlRepository is interface of base class for querying SQL tables thru EntityFrameworkCore.
    /// </summary>
    /// <typeparam name="TEntity">Entity of DataBase.</typeparam>
    /// <typeparam name="TDataContext">DataBase context.</typeparam>
    public interface IBaseSqlRepository<TEntity, TDataContext>
    {
        /// <summary>
        /// Get all items.
        /// </summary>
        /// <returns>Query for getting all items.</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Get all items by condition.
        /// </summary>
        /// <param name="expression">LINQ expression.</param>
        /// <returns>Query for getting all items.</returns>
        IQueryable<TEntity> GetAllByCondition(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// Get all rows async.
        /// </summary>
        /// <returns>List of items.</returns>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Get all items by condition async.
        /// </summary>
        /// <param name="expression">LINQ expression.</param>
        /// <param name="enableTracking">True if tracking should be enabled.</param>
        /// <returns>List of items.</returns>
        Task<List<TEntity>> GetAllByConditionAsync(Expression<Func<TEntity, bool>> expression, bool enableTracking = false);

        /// <summary>
        /// Create row async.
        /// </summary>
        /// <param name="entity">Item for added in table.</param>
        /// <returns>Created item.</returns>
        Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// Update row async.
        /// </summary>
        /// <param name="entity">Item for updated in table.</param>
        /// <returns>Updated item.</returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Save changes async.
        /// </summary>
        /// <returns>Task of saving changes.</returns>
        Task SaveAsync();

        /// <summary>
        /// Create list of items in table.
        /// </summary>
        /// <param name="entities">List of items.</param>
        /// <returns>Created items.</returns>
        Task<List<TEntity>> CreateRangeAsync(List<TEntity> entities);

        /// <summary>
        /// Update rows in table.
        /// </summary>
        /// <param name="entities">List of items.</param>
        /// <returns>Updated items.</returns>
        Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entities);

        /// <summary>
        /// Delete rows in table.
        /// </summary>
        /// <param name="entities">List of items.</param>
        /// <returns>Task of deleting items.</returns>
        Task DeleteRangeAsync(List<TEntity> entities);

        /// <summary>
        /// Delete rows in table by condition.
        /// </summary>
        /// <param name="expression">LINQ Expression.</param>
        /// <returns>Count of removed items.</returns>
        Task<int> DeleteItemRangeByConditonsAsync(Expression<Func<TEntity, bool>> expression);
    }
}
