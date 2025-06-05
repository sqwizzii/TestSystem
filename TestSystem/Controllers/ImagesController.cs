using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestSystemAPI.Data;
using TestSystemAPI.DTOs;
using TestSystemAPI.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TestSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class ImagesController : ControllerBase
    {
        private readonly TestSystemDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ImagesController(TestSystemDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(ImageUploadDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Base64Data))
                return BadRequest("Image data is empty");

            byte[] imageBytes;
            try
            {
                imageBytes = Convert.FromBase64String(dto.Base64Data);
            }
            catch
            {
                return BadRequest("Invalid base64 string");
            }

            var uploadsFolder = Path.Combine(_env.WebRootPath ?? "wwwroot", "images");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{dto.FileName}";
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

            var image = new Image
            {
                FileName = uniqueFileName,
                FilePath = $"/images/{uniqueFileName}",
                TestId = dto.TestId,
                QuestionId = dto.QuestionId
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return Ok(image);
        }
    }
}
