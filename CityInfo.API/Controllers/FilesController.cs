using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
	
        [Route("api/files")]
        [ApiController]
        public class FilesController : ControllerBase
        {
            private readonly FileExtensionContentTypeProvider _fileExtensionTypeProvider;

            public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
            {
                _fileExtensionTypeProvider = fileExtensionContentTypeProvider
                    ?? throw new System.ArgumentNullException(nameof(fileExtensionContentTypeProvider));
            }

            [HttpGet("{fileId}")]
            public ActionResult GetFile(string fileId)
            {
                var pathToFile = "Ankush_CV.pdf";
                if (!System.IO.File.Exists(pathToFile))
                {
                    return NotFound();
                }

                if (!_fileExtensionTypeProvider.TryGetContentType(pathToFile, out string contentType))
                {
                    contentType = "application/octet-stream"; // arbitary data type for arbitary data
                }
                var bytes = System.IO.File.ReadAllBytes(pathToFile);
                return File(bytes, contentType, Path.GetFileName(pathToFile));
            }

        }
    
}

