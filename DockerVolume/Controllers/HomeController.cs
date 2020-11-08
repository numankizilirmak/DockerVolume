using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DockerVolume.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.Extensions.FileProviders;
using System.Linq;

namespace DockerVolume.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileProvider _fileProvider;
        public HomeController(ILogger<HomeController> logger,IFileProvider fileProvider)
        {
            _logger = logger;
            _fileProvider = fileProvider;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SaveImage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SaveImage(IFormFile image)
        {
            if(image !=null && image.Length>0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }                    
            }
            return View();
        }
        public IActionResult ShowImage()
        {
            var images = _fileProvider.GetDirectoryContents("wwwroot/images").ToList().Select(x => x.Name);
            return View(images);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
