namespace Tawuniya.Core.Data
{
    public partial interface IRepository<TEntity> where TEntity : BaseEntity
    {
        #region Methods

        /// <summary>
        /// get entity by id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>entity</returns>
        Task<TEntity> GetbyIdAsync(int id);

        /// <summary>
        /// insert entity async
        /// </summary>
        /// <param name="entity">entity</param>
        Task InsertAsync(TEntity entity);

        /// <summary>
        /// Insert entities async
        /// </summary>
        /// <param name="entities">Entities</param>
        Task InsertAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// update entity async
        /// </summary>
        /// <param name="entity">entity</param>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Update entities async
        /// </summary>
        /// <param name="entities">Entities</param>
        Task UpdateAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// delete entity async
        /// </summary>
        /// <param name="entity">entity</param>
        Task DeleteAsync(TEntity entity);

        /// <summary>
        /// Delete entities async
        /// </summary>
        /// <param name="entities">Entities</param>
        Task DeleteAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// get entities by ids
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns>entities</returns>
        Task<IList<TEntity>> GetByIdsAsync(IList<int> ids);

        #endregion

        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<TEntity> Table { get; }

        #endregion
    }
}
