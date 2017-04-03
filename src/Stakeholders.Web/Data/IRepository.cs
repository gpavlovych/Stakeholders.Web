// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-18-2017
//
// Last Modified By : George
// Last Modified On : 02-19-2017
// ***********************************************************************
// <copyright file="IRepository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stakeholders.Web.Models;

namespace Stakeholders.Web.Data
{
    /// <summary>
    /// Interface IRepository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : IBaseEntity
    {
        /// <summary>
        /// Gets all entities.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <param name="predicate">The predicate (optional).</param>
        /// <returns>The entities</returns>
        IEnumerable<T> GetAll(int start, int count, Func<T, bool> predicate = null);

        /// <summary>
        /// Gets all entities asynchronously.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <returns>The entities</returns>
        Task<IEnumerable<T>> GetAllAsync(int start, int count);

        /// <summary>
        /// Returns total number of entities.
        /// </summary>
        /// <returns>System.Int64.</returns>
        long Count();

        /// <summary>
        /// Returns total number of entities asynchronously.
        /// </summary>
        /// <returns>System.Int64.</returns>
        Task<long> CountAsync();

        /// <summary>
        /// Gets the entity by the specified identifier and throws an exception if not found.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity.</returns>
        T GetById(long id);

        /// <summary>
        /// Gets the entity by the specified identifier and throws an exception if not found asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity.</returns>
        Task<T> GetByIdAsync(long id);

        /// <summary>
        ///Gets the entity by specified identifier, returning null if no corresponding entity is found.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity.</returns>
        T FindById(long id);

        /// <summary>
        /// Gets the entity by specified identifier, returning null if no corresponding entity is found asynchronously.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The entity.</returns>
        Task<T> FindByIdAsync(long id);

        /// <summary>
        /// Inserts the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Insert(T entity);

        /// <summary>
        /// Inserts the specified entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        Task InsertAsync(T entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Update(T entity);

        /// <summary>
        /// Updates the specified entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(T entity);

        /// <summary>
        /// Deletes the specified entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        Task DeleteAsync(T entity);
    }
}