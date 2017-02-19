using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stakeholders.Web.Models;

namespace Stakeholders.Web.Data
{
    public class ActivityRepository: IRepository<Activity>
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// The entities
        /// </summary>
        private readonly DbSet<Activity> entities;

        /// <summary>
        /// The error message
        /// </summary>
        string errorMessage = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public ActivityRepository(ApplicationDbContext context)
        {
            this.context = context;
            this.entities = context.Set<Activity>();
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>The entities</returns>
        public IEnumerable<Activity> GetAll(int start, int count)
        {
            return
                this.entities.Include(it => it.Company)
                    .Include(it => it.ObserverUsersCompanies)
                    .Include(it => it.Type)
                    .Include(it => it.User)
                    .Include(it => it.Contact)
                    .Include(it => it.Task).Skip(start)
                    .Take(count)

                    .ToList();
        }

        /// <summary>
        /// Gets all entities asynchronously.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>The entities</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<Activity>> GetAllAsync(int start, int count)
        {
            return await this.entities.Include(it => it.Company)
                .Include(it => it.ObserverUsersCompanies)
                .Include(it => it.Type)
                .Include(it => it.User)
                .Include(it => it.Contact)
                .Include(it => it.Task).Skip(start).Take(count)

                .ToListAsync();
        }

        /// <summary>
        /// Returns total number of entities.
        /// </summary>
        /// <returns>System.Int64.</returns>
        public long Count()
        {
            return this.entities.LongCount();
        }

        /// <summary>
        /// Returns total number of entities asynchronously.
        /// </summary>
        /// <returns>System.Int64.</returns>
        public async Task<long> CountAsync()
        {
            return await this.entities.LongCountAsync();
        }

        /// <summary>
        /// Gets the entity by the specified identifier and throws an exception if not found.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity.</returns>
        public Activity GetById(long id)
        {
            return this.entities.Single(s => s.Id == id);
        }

        /// <summary>
        /// Gets the entity by the specified identifier and throws an exception if not found asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity.</returns>
        public async Task<Activity> GetByIdAsync(long id)
        {
            return await this.entities.SingleAsync(s => s.Id == id);
        }

        /// <summary>
        ///Gets the entity by specified identifier, returning null if no corresponding entity is found.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity.</returns>
        public Activity FindById(long id)
        {
            return this.entities.SingleOrDefault(s => s.Id == id);
        }

        /// <summary>
        ///Gets the entity by specified identifier, returning null if no corresponding entity is found asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity.</returns>
        public async Task<Activity> FindByIdAsync(long id)
        {
            return await this.entities.SingleOrDefaultAsync(s => s.Id == id);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="ArgumentNullException">entity</exception>
        public void Insert(Activity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.entities.Add(entity);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Inserts the specified entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task InsertAsync(Activity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.entities.Add(entity);
            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="ArgumentNullException">entity</exception>
        public void Update(Activity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.context.SaveChanges();
        }

        /// <summary>
        /// Updates the specified entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public async Task UpdateAsync(Activity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            await this.context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="ArgumentNullException">entity</exception>
        public void Delete(Activity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.entities.Remove(entity);
            this.context.SaveChanges();
        }

        /// <summary>
        /// Deletes the specified entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>Task.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task DeleteAsync(Activity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            this.entities.Remove(entity);
            await this.context.SaveChangesAsync();
        }
    }
}
