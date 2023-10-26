using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace LakeXplorer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;

        public ImageController(IWebHostEnvironment env)
        {
            _env = env;
        }


        /// <summary>
        /// Handles the uploading and saving of image files.
        /// </summary>
        /// <returns>Returns a JSON object containing the image's relative path if successful; otherwise, returns a 400 Bad Request with an error message.</returns>
        [HttpPost]
        [Route("SaveFile")]
        public IActionResult SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Request;
                var postedFile = httpRequest.Form.Files[0];
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(postedFile.FileName);
                var physicalPath = Path.Combine(_env.ContentRootPath, "Photos", filename);

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                string relativePath = "Photos/" + filename;
                string imagePath = Url.Content(relativePath);

                return Ok(new { imagePath });
            }
            catch (Exception)
            {
                return BadRequest("Error saving the file.");
            }
        }
    }
}
