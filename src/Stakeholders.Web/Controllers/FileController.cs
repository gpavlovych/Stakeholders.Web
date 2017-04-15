// ***********************************************************************
// Assembly         : Stakeholders.Web
// Author           : George
// Created          : 04-14-2017
//
// Last Modified By : George
// Last Modified On : 04-15-2017
// ***********************************************************************
// <copyright file="FileController.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System.IO;
using System.Threading.Tasks;

namespace Stakeholders.Web.Controllers
{
    /// <summary>
    /// Class FileController.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Produces("application/json")]
    [Route("api/Files")]
    [Authorize]
    public class FileController : Controller
    {
        /// <summary>
        /// The environment
        /// </summary>
        private readonly IHostingEnvironment environment;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileController"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <exception cref="ArgumentNullException">environment</exception>
        public FileController(IHostingEnvironment environment)
        {
            if (environment == null)
            {
                throw new ArgumentNullException(nameof(environment));
            }

            this.environment = environment;
        }

        /// <summary>
        /// Downloads the file.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        public IActionResult DownloadFile([FromRoute] string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return this.BadRequest();
            }

            var filePath = Path.Combine(this.environment.WebRootPath, "uploads", fileName);
            string contentType;
            if (!new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType))
            {
                contentType = "application/octet-stream";
            }
            if (System.IO.File.Exists(filePath))
            {
                return this.File(System.IO.File.OpenRead(filePath), contentType);
            }

            return this.NoContent();
        }

        /// <summary>
        /// Uploads the files.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
        [HttpPost]
        public async Task<IActionResult> UploadFiles([FromBody] IFormFile file)
        {
            if (file == null)
            {
                return this.BadRequest();
            }

            var uploads = Path.Combine(this.environment.WebRootPath, "uploads");
            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }

            return this.Ok();
        }
    }
}
