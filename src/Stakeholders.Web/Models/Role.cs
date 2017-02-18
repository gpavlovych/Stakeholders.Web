using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Stakeholders.Web.Models
{
    /// <summary>
    /// Class Role.
    /// </summary>
    /// <seealso cref="string" />
    public class Role: IdentityRole<string>
    {
        /// <summary>
        /// Gets or sets the name en.
        /// </summary>
        /// <value>The name en.</value>
        public string NameEn { get; set; }
    }
}