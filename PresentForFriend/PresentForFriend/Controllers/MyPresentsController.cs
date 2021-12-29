using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentForFriend.Data;
using PresentForFriend.Models;
using PresentForFriend.Service;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PresentForFriend.Controllers
{
    public class MyPresentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IUserService _userService;

        public IWebHostEnvironment HostEnviroment { get; }

        public MyPresentsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment, IUserService userService)
        {
            _context = context;
            _userService = userService;
            _hostEnvironment = hostEnvironment;
        }


        public async Task<IActionResult> Index()
        {
            var UserID = _userService.GetUserId();
            var presents = await _context.Presents.Where(present=> present.UserID==UserID).ToListAsync();

            return View(presents);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Present present)
        {
            if (ModelState.IsValid)
            {
                //Save image to wwwroot/ image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(present.ImageFile.FileName);
                string extension = Path.GetExtension(present.ImageFile.FileName);
                present.ImageName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await present.ImageFile.CopyToAsync(fileStream);
                }
                //insert record
                present.UserID = _userService.GetUserId();
                _context.Presents.Add(present);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(present);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id <= 0)
                return BadRequest();

            var presentInDB = await _context.Presents.FirstOrDefaultAsync(e => e.Id == id);

            if (presentInDB == null)
                return NotFound();

            return View(presentInDB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Present present)
        {

            if (!ModelState.IsValid)
                return View(present);

            _context.Presents.Update(present);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id <= 0)
                return BadRequest();

            var presentInDB = await _context.Presents.FirstOrDefaultAsync(e => e.Id == id);

            if (presentInDB == null)
                return NotFound();

            return View(presentInDB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Present present)
        {
            var presentInDB = await _context.Presents.FindAsync(present.Id);

            //delete image from wwwroot/image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "image", presentInDB.ImageName);

            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            //delete the record
            _context.Presents.Remove(presentInDB);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
