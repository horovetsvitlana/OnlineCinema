using Microsoft.EntityFrameworkCore;
using OnlineCinema.Repository.Interfaces;
using System.Linq.Expressions;

namespace OnlineCinema.Repository.Implementation
{
    /// <summary>
    /// Base class for querying SQL tables thru EntityFrameworkCore.
    /// </summary>
    /// <typeparam name="TEntity">Child repository will be working with this entity.</typeparam>
    /// <typeparam name="TDataContext">DBContext for repository.</typeparam>
    public class BaseSQLRepository<TEntity, TDataContext> : IBaseSqlRepository<TEntity, TDataContext>
        where TEntity : class
        where TDataContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSQLRepository{TEntity, TDataContext}"/> class.
        /// Constructor of class.
        /// </summary>
        /// <param name="dbContext">Context of database.</param>
        public BaseSQLRepository(TDataContext dbContext)
        {
            this.DbContext = dbContext;
        }

        /// <summary>
        /// Gets or sets context of database.
        /// </summary>
        protected TDataContext DbContext { get; set; }

        /// <inheritdoc cref="IBaseSqlRepository{TEntity, TDataContext}.GetAll()"/>
        public virtual IQueryable<TEntity> GetAll()
        {
            return this.DbContext.Set<TEntity>().AsNoTracking();
        }

        /// <inheritdoc/>
        public virtual IQueryable<TEntity> GetAllByCondition(Expression<Func<TEntity, bool>> expression)
        {
            return this.DbContext.Set<TEntity>().AsNoTracking().Where(expression);
        }

        /// <summary>
        /// Get all items async.
        /// </summary>
        /// <returns>Get list of entities.</returns>
        public virtual async Task<List<TEntity>> GetAllAsync()
        {
            return await this.DbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<List<TEntity>> GetAllByConditionAsync(Expression<Func<TEntity, bool>> expression, bool enableTracking = false)
        {
            if (enableTracking)
            {
                return await this.DbContext.Set<TEntity>().Where(expression).ToListAsync();
            }
            else
            {
                return await this.DbContext.Set<TEntity>().AsNoTracking().Where(expression).ToListAsync();
            }
        }

        /// <inheritdoc/>
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            this.DbContext.Set<TEntity>().Add(entity);
            await this.SaveAsync();

            return entity;
        }

        /// <inheritdoc/>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            this.DbContext.Entry(entity).State = EntityState.Modified;
            await this.SaveAsync();
        }

        /// <inheritdoc/>
        public virtual async Task SaveAsync()
        {
            await this.DbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<List<TEntity>> CreateRangeAsync(List<TEntity> entities)
        {
            this.DbContext.Set<TEntity>().AddRange(entities);
            await this.SaveAsync();

            return entities;
        }

        /// <inheritdoc/>
        public virtual async Task<List<TEntity>> UpdateRangeAsync(List<TEntity> entities)
        {
            this.DbContext.Set<TEntity>().UpdateRange(entities);
            await this.SaveAsync();

            return entities;
        }

        /// <inheritdoc/>
        public virtual async Task DeleteRangeAsync(List<TEntity> entities)
        {
            this.DbContext.Set<TEntity>().RemoveRange(entities);
            await this.SaveAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<int> DeleteItemRangeByConditonsAsync(Expression<Func<TEntity, bool>> expression)
        {
            var query = this.DbContext.Set<TEntity>().Where(expression);
            this.DbContext.Set<TEntity>().RemoveRange(query);
            int count = await this.DbContext.SaveChangesAsync();
            return count;
        }
    }
}