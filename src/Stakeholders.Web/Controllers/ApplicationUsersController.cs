// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 02-19-2017
//
// Last Modified By : George
// Last Modified On : 04-15-2017
// ***********************************************************************
// <copyright file="ApplicationUsersController.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stakeholders.Web.Data;
using Stakeholders.Web.Models;
using Stakeholders.Web.Models.ApplicationUserViewModels;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Stakeholders.Web.Controllers
{
    /// <summary>
    /// Class ApplicationUsersController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/ApplicationUsers")]
    [Authorize]
    public class ApplicationUsersController : Controller
    {
        /// <summary>
        /// The period provider
        /// </summary>
        private readonly IPeriodProvider periodProvider;

        /// <summary>
        /// The source
        /// </summary>
        private readonly IDataSource<ApplicationUser> source;

        /// <summary>
        /// The context
        /// </summary>
        private readonly IRepository<ApplicationUser> repository;

        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper mapper;

        /// <summary>
        /// The HTTP context accessor
        /// </summary>
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// The user manager
        /// </summary>
        private readonly IApplicationUserManager userManager;

        /// <summary>
        /// The role repository
        /// </summary>
        private readonly IRepository<Role> roleRepository;

        /// <summary>
        /// The company repository
        /// </summary>
        private readonly IRepository<Company> companyRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationUsersController" /> class.
        /// </summary>
        /// <param name="periodProvider">The period provider.</param>
        /// <param name="source">The source.</param>
        /// <param name="repository">The repository.</param>
        /// <param name="companyRepository">The company repository.</param>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="userManager">The user manager.</param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <exception cref="ArgumentNullException">
        /// periodProvider
        /// or
        /// source
        /// or
        /// repository
        /// or
        /// companyRepository
        /// or
        /// roleRepository
        /// or
        /// mapper
        /// or
        /// userManager
        /// or
        /// httpContextAccessor
        /// </exception>
        public ApplicationUsersController(
            IPeriodProvider periodProvider,
            IDataSource<ApplicationUser> source,
            IRepository<ApplicationUser> repository,
            IRepository<Company> companyRepository,
            IRepository<Role> roleRepository,
            IMapper mapper,
            IApplicationUserManager userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            if (periodProvider == null)
            {
                throw new ArgumentNullException(nameof(periodProvider));
            }

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (repository == null)
            {
                throw new ArgumentNullException(nameof(repository));
            }

            if (companyRepository == null)
            {
                throw new ArgumentNullException(nameof(companyRepository));
            }

            if (roleRepository == null)
            {
                throw new ArgumentNullException(nameof(roleRepository));
            }

            if (mapper == null)
            {
                throw new ArgumentNullException(nameof(mapper));
            }

            if (userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager));
            }

            if (httpContextAccessor == null)
            {
                throw new ArgumentNullException(nameof(httpContextAccessor));
            }

            this.periodProvider = periodProvider;
            this.source = source;
            this.repository = repository;
            this.mapper = mapper;
            this.userManager = userManager;
            this.roleRepository = roleRepository;
            this.companyRepository = companyRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        // GET: api/ApplicationUsers
        /// <summary>
        /// Gets the application users.
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="count">The count.</param>
        /// <param name="search">The search.</param>
        /// <param name="period">The period.</param>
        /// <param name="includeStats">The include stats.</param>
        /// <returns>ApplicationUserInfoViewModel[].</returns>
        [HttpGet]
        public ApplicationUserViewModel[] GetApplicationUsers(
            int start = 0,
            int count = 10,
            string search = "",
            int? period = null,
            int? includeStats=0)
        {
            DateRange periodRange = null;
            switch (period)
            {
                case 1:
                    //this year
                    periodRange = this.periodProvider.GetThisYearRange();
                    break;
                case 2:
                    //this quarter
                    periodRange = this.periodProvider.GetThisQuarterRange();
                    break;
                case 3:
                    //this month
                    periodRange = this.periodProvider.GetThisMonthRange();
                    break;
                case 4:
                    //this week
                    periodRange = this.periodProvider.GetThisWeekRange();
                    break;
            }

            DateTime? startPeriod = periodRange?.MinDate;
            DateTime? endPeriod = periodRange?.MaxDate;

            var query = this.source.GetDataQueryable()
                .Where(it => string.IsNullOrEmpty(search) || it.Name.Contains(search))
                .Skip(start)
                .Take(count);
            if (includeStats == 1)
            {
                var xxx =  query.Select(
                        it => new Tuple<ApplicationUser, long, long, long>(

                            //Item1 - User
                            it,

                            //Item2 - Tasks total
                            it.AssignedTasks.Distinct().Count(
                                task =>
                                    ((startPeriod == null) || (task.DateDeadline >= startPeriod)) &&
                                    ((endPeriod == null) || (task.DateDeadline <= endPeriod))),

                            //Item3 - Tasks done
                            it.AssignedTasks.Distinct().Count(
                                task => (task.Status.Alias == "Done") &&
                                        ((startPeriod == null) || (task.DateDeadline >= startPeriod)) &&
                                        ((endPeriod == null) || (task.DateDeadline <= endPeriod))),

                            //Item4 - Activities
                            it.Activities.Select(activity => activity.Task).Distinct().Count(
                                task =>
                                    ((startPeriod == null) || (task.DateDeadline >= startPeriod)) &&
                                    ((endPeriod == null) || (task.DateDeadline <= endPeriod)))
                        ))
                    .ToList()
                    .Select(
                        it =>
                        {
                            var user = it.Item1;
                            var tasksTotal = it.Item2;
                            var tasksDone = it.Item3;
                            var activities = it.Item4;

                            var result = this.mapper.Map<ApplicationUserViewModel>(user);
                            result.TasksCompletedPercentage = tasksTotal != 0 ? tasksDone*100.0/tasksTotal : 0;
                            result.ActivitiesNumber = activities;
                            return result;
                        })
                    .ToArray();
					 return xxx;
            }
            else
            {
                return query.ToList().Select(it => this.mapper.Map<ApplicationUserViewModel>(it)).ToArray();
            }
        }

        // GET: api/ApplicationUsers/current
        /// <summary>
        /// Gets the application users count.
        /// </summary>
        /// <returns>System.Int64.</returns>
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var currentUserId = this.httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentUser = await this.userManager.FindByNameAsync(currentUserId);
            currentUser = await this.repository.FindByIdAsync(currentUser.Id);
            var result = this.mapper.Map<ApplicationUserViewModel>(currentUser);
            return this.Ok(result);
        }

        // GET: api/ApplicationUsers/count
        /// <summary>
        /// Gets the application users count.
        /// </summary>
        /// <returns>System.Int64.</returns>
        [HttpGet("count")]
        public long GetApplicationUsersCount()
        {
            return this.repository.Count();
        }

        // GET: api/ApplicationUsers/5
        /// <summary>
        /// Gets the application user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetApplicationUser([FromRoute] long id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = await this.repository.FindByIdAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            var result = this.mapper.Map<ApplicationUserViewModel>(entity);

            return this.Ok(result);
        }

        // PUT: api/ApplicationUsers/5
        /// <summary>
        /// Puts the application user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="model">the application user.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicationUser(
            [FromRoute] long id,
            [FromBody] ApplicationUserViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = await this.repository.FindByIdAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            this.mapper.Map(model, entity);

            await this.repository.UpdateAsync(entity);

            return this.NoContent();
        }

        // POST: api/ApplicationUsers
        // does not require authorization
        /// <summary>
        /// Posts the application user.
        /// </summary>
        /// <param name="model">the application user.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> PostApplicationUser([FromBody] CreateUserViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = this.mapper.Map<ApplicationUser>(model);
            try
            {
                await this.userManager.CreateAsync(entity, model.Password);
                if (model.CompanyId != null)
                {
                    entity.Company = this.companyRepository.FindById(model.CompanyId.Value);
                }

                if (model.RoleId != null)
                {
                    entity.Role = this.roleRepository.FindById(model.RoleId.Value);
                }

                return this.CreatedAtAction(
                    "GetApplicationUser",
                    new
                    {
                        id = entity.Id
                    },
                    model);
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }
        }

        // DELETE: api/ApplicationUsers/5
        /// <summary>
        /// Deletes the application user.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicationUser([FromRoute] long id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = await this.repository.FindByIdAsync(id);
            if (entity == null)
            {
                return this.NotFound();
            }

            await this.repository.DeleteAsync(entity);

            var result = this.mapper.Map<ApplicationUserViewModel>(entity);

            return this.Ok(result);
        }
    }
}