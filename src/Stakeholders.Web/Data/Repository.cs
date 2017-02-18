// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-18-2017
//
// Last Modified By : George
// Last Modified On : 02-18-2017
// ***********************************************************************
// <copyright file="Repository.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Stakeholders.Web.Models;

namespace Stakeholders.Web.Data
{
    /// <summary>
    /// Class Repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Stakeholders.Web.Data.IRepository{T}" />
    public class Repository<T> : IRepository<T> where T : BaseEntity
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
        /// Initializes a new instance of the <see cref="Repository{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            this.entities = context.Set<T>();
        }
        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
        public IEnumerable<T> GetAll()
        {
            return this.entities.AsEnumerable();
        }
        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>T.</returns>
        public T Get(long id)
        {
            return this.entities.SingleOrDefault(s => s.Id == id);
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
                throw new ArgumentNullException("entity");
            }

            this.entities.Add(entity);
            this.context.SaveChanges();
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
                throw new ArgumentNullException("entity");
            }

            this.context.SaveChanges();
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
                throw new ArgumentNullException("entity");
            }

            this.entities.Remove(entity);
            this.context.SaveChanges();
        }
    }
}