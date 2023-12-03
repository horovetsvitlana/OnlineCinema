using Microsoft.AspNetCore.Mvc;
using OnlineCinema.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace OnlineCinema.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private IConfiguration _configuration;

        public FileController(IConfiguration configuration, IFileService fileService)
        {
            _configuration = configuration;
            _fileService = fileService;
        }


        #region Upload  
        [HttpPost(nameof(Upload))]
        public IActionResult Upload([Required] List<IFormFile> formFiles, [Required] string subDirectory)
        {
            try
            {
                _fileService.UploadFile(formFiles, subDirectory);

                return Ok(new { formFiles.Count, Size = _fileService.SizeConverter(formFiles.Sum(f => f.Length)) });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        [HttpGet(nameof(Download))]
        public IActionResult Download([Required] string path,[Required]  string filename)
        {
            using(WebClient client = new WebClient())
            {
                client.DownloadFile(path, filename);
                
            }
            return Ok("Downloaded");

        }
        [HttpGet(nameof(Downloadv2))]
        public byte[] Downloadv2([Required] string path)
        {
            using (WebClient client = new WebClient())
            {
                return client.DownloadData(path);

            }
        }



        [HttpGet("/GetFile/{formFile}")]
        public FileStreamResult GetFile( [Required] string subDirectory, [Required] string formFile)
        {
            //var physicalPath = $"./{subDirectory}/{formFile}";
            var files = _fileService.GetFiles(subDirectory, formFile);
            //var pdfBytes = System.IO.File.ReadAllBytes(physicalPath);
            //using var ms = new MemoryStream(pdfBytes);
            //return new FileStreamResult(ms, GetMimeType(formFile));

            return files;

        }
        
        private string GetMimeType(string formFile)
        {
            var extension = formFile.Split(".").Last();
            switch (extension)
            {
                case "jpg":
                case "jpeg":
                    return "image/jpeg";
                case "csv":
                    return "text/csv";
                case "pdf":
                    return "application/pdf";
                case "html":
                    return "text/html";
                default:
                    throw new ArgumentException($"Unsupported file type, file: {formFile}");
            }

        }

    }
}



