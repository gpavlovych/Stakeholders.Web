﻿// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-18-2017
//
// Last Modified By : George
// Last Modified On : 02-19-2017
// ***********************************************************************
// <copyright file="Repository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stakeholders.Web.Models;

namespace Stakeholders.Web.Data
{
    /// <summary>
    /// Class Repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Stakeholders.Web.Data.IRepository{T}" />
    public class Repository<T> : IRepository<T> where T : class, IBaseEntity
    {
        /// <summary>
        /// The context
        /// </summary>
        private readonly ApplicationDbContext context;

        /// <summary>
        /// The entities
        /// </summary>
        private readonly DbSet<T> entities;

        /// <summary>
        /// The error message
        /// </summary>
        string errorMessage = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{T}" /> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            this.entities = context.Set<T>();
        }

        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>The entities</returns>
        public IEnumerable<T> GetAll(int start, int count)
        {
            return this.entities.Skip(start).Take(count).ToList();
        }

        /// <summary>
        /// Gets all entities asynchronously.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>The entities</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<T>> GetAllAsync(int start, int count)
        {
            return await this.entities.Skip(start).Take(count).ToListAsync();
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
        public T GetById(long id)
        {
            return this.entities.Single(s => s.Id == id);
        }

        /// <summary>
        /// Gets the entity by the specified identifier and throws an exception if not found asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity.</returns>
        public async Task<T> GetByIdAsync(long id)
        {
            return await this.entities.SingleAsync(s => s.Id == id);
        }

        /// <summary>
        ///Gets the entity by specified identifier, returning null if no corresponding entity is found.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity.</returns>
        public T FindById(long id)
        {
            return this.entities.SingleOrDefault(s => s.Id == id);
        }

        /// <summary>
        ///Gets the entity by specified identifier, returning null if no corresponding entity is found asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity.</returns>
        public async Task<T> FindByIdAsync(long id)
        {
            return await this.entities.SingleOrDefaultAsync(s => s.Id == id);
        }

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="ArgumentNullException">entity</exception>
        public void Insert(T entity)
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
        public async Task InsertAsync(T entity)
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
        public void Update(T entity)
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
        public async Task UpdateAsync(T entity)
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
        public void Delete(T entity)
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
        public async Task DeleteAsync(T entity)
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