using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentForFriend.Data;
using PresentForFriend.Models;
using PresentForFriend.Service;
using System;
using System.IO;
using System.Linq;
using System.Net;
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
            var presents = await _context.Presents.Where(present=>present.UserID == UserID).ToListAsync();

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
                using (var memoryStream = new MemoryStream())
                {
                    await present.ImageFile.CopyToAsync(memoryStream);
                    present.DataFiles = memoryStream.ToArray();//массив байтов
                }

                present.UserID = _userService.GetUserId();

                _context.Presents.Add(present);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(present);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var presentInDB = await _context.Presents.FirstOrDefaultAsync(e => e.Id == id);

            return View(presentInDB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Present present)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await present.ImageFile.CopyToAsync(memoryStream);
                        present.DataFiles = memoryStream.ToArray();
                    }
                }
                catch (Exception ex)
                {
                    var dataFiles = _context.Presents.Where(el => el.Id == present.Id)
                        .Select(el => el.DataFiles).FirstOrDefault();

                    present.DataFiles = dataFiles;
                }

                present.UserID = _userService.GetUserId();

                _context.Presents.Update(present);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(present);
        }

        public async Task<IActionResult> Delete(int id)
        {

            var presentInDB = await _context.Presents.FirstOrDefaultAsync(e => e.Id == id);

            return View(presentInDB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Present present)
        {
            var presentInDB = await _context.Presents.FindAsync(present.Id);

            _context.Presents.Remove(presentInDB);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
