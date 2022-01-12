using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PresentForFriend.Data;
using PresentForFriend.Models;
using PresentForFriend.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PresentForFriend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<HomeController> _logger;

        public static List<string> imagesNames;
        private string wwwRootPath;

        private bool getImages = true;
        public IWebHostEnvironment HostEnviroment { get; }

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostEnvironment, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
            _hostEnvironment = hostEnvironment;

            wwwRootPath = _hostEnvironment.WebRootPath;

            if (getImages)
            {
                GetSiteImages();
                getImages = false;
            }
        }

        private void GetSiteImages()
        {
            string pathToImage = Path.Combine(wwwRootPath + "/image/");

            imagesNames = Directory.GetFiles(pathToImage)
                                     .Select(Path.GetFileName)
                                     .ToList();
        }

        private void UpdateImages(List<Present> presents)
        {           
            foreach (var present in presents)
            {
                if (!imagesNames.Contains(present.ImageName))
                {
                    string path = Path.Combine(wwwRootPath + "/image/", present.ImageName + ".jpg");
                    System.IO.File.WriteAllBytes(path, present.DataFiles);
                }
            }
        }

        public async Task<IActionResult> Index()
        {
            var presents = await _context.Presents.ToListAsync();

            if (presents.Count != imagesNames.Count)
                UpdateImages(presents);

            return View(presents);
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
