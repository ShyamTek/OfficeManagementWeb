using Microsoft.EntityFrameworkCore;
using Tawuniya.Core;
using Tawuniya.Core.Data;

namespace Tawuniya.Data
{
    public partial class EfRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        #region Fields

        private readonly TawuniyaDBContext _context;

        private DbSet<TEntity> _entities;

        #endregion

        #region Ctor

        public EfRepository(TawuniyaDBContext context,
            DbSet<TEntity> entities)
        {
            _context = context;
            _entities = entities;
        }

        #endregion

        #region Utilities

        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                _entities ??= _context.Set<TEntity>();
                return _entities;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// gets a table
        /// </summary>
        public IQueryable<TEntity> Table => Entities;

        /// <summary>
        /// delete entity async
        /// </summary>
        /// <param name="entity">entity</param>
        public async Task DeleteAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entities.Remove(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                throw new Exception(exception.ToString());
            }
        }

        /// <summary>
        /// delete entities async
        /// </summary>
        /// <param name="entities">entities</param>
        public async Task DeleteAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                Entities.RemoveRange(entities);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(exception.ToString());
            }
        }

        /// <summary>
        /// get entity by id async
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>entity</returns>
        public async Task<TEntity> GetbyIdAsync(int id)
        {
            return await Entities.FindAsync(id);
        }

        /// <summary>
        /// get ids async
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns>entities</returns>
        public async Task<IList<TEntity>> GetByIdsAsync(IList<int> ids)
        {
            if (!ids?.Any() ?? true)
                return new List<TEntity>();
            var sortedEntries = new List<TEntity>();
            if (ids?.Count > 0)
            {
                var entries = await Table.Where(e => ids.Contains(e.Id)).ToListAsync();
                //sort by passed identifiers
                foreach (var id in ids)
                {
                    var sortedEntry = entries.Find(entry => entry.Id == id);
                    if (sortedEntry != null)
                        sortedEntries.Add(sortedEntry);
                }
            }

            return sortedEntries;
        }

        /// <summary>
        /// insert entity async
        /// </summary>
        /// <param name="entity">entity</param>
        public async Task InsertAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                await Entities.AddAsync(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(exception.ToString());
            }
        }

        /// <summary>
        /// insert entitis async
        /// </summary>
        /// <param name="entities">entities</param>
        public async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                await Entities.AddRangeAsync(entities);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(exception.ToString());
            }
        }

        /// <summary>
        /// update entity async
        /// </summary>
        /// <param name="entity">entity</param>
        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                Entities.Update(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(exception.ToString());
            }
        }

        /// <summary>
        /// update entities async
        /// </summary>
        /// <param name="entities">entities</param>
        public async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            try
            {
                Entities.UpdateRange(entities);
                _context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(exception.ToString());
            }
        } 

        #endregion
    }
}
