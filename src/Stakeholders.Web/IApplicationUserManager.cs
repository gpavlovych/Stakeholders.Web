using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Stakeholders.Web.Models;

namespace Stakeholders.Web
{
    public interface IApplicationUserManager
    {
        Task CreateAsync(ApplicationUser user, string password);
    }

    public class ApplicationUserManager : IApplicationUserManager
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ApplicationUserManager(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task CreateAsync(ApplicationUser user, string password)
        {
            var result = await this.userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(result.Errors.ToString());
            }
        }
    }
}