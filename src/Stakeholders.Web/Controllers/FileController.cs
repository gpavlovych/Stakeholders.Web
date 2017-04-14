using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Stakeholders.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Files")]
    [Authorize]
    public class FileController: Controller
    {
        private IHostingEnvironment _environment;

        public FileController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        private string GetContentType(string fileName)
        {
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out contentType);
            return contentType ?? "application/octet-stream";
        }

        [HttpGet]
        public IActionResult Index(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return BadRequest();
            }

            var filePath  = Path.Combine(_environment.WebRootPath, "uploads", fileName);
            var contentType = GetContentType(filePath);
            if (System.IO.File.Exists(filePath))
            {
                return File(System.IO.File.OpenRead(filePath), contentType);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ICollection<IFormFile> files)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            return Ok();
        }
    }
}
