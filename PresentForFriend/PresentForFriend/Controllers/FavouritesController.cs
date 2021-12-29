using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentForFriend.Data;
using PresentForFriend.Models;
using PresentForFriend.Models.PresentForFriend.Models;
using PresentForFriend.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentForFriend.Controllers
{
    public class FavouritesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public FavouritesController(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public async Task<IActionResult> Index()
        {
            var UserID = _userService.GetUserId();

            var favouitesPresentsID = await _context.Favourites
                .Where(favouitesPresents => favouitesPresents.UserID == UserID)
                .Select(favouitesPresents => favouitesPresents.PresentId).ToListAsync();

            var presents = new List<Present>();

            for (var i=0; i<favouitesPresentsID.Count; i++)
            {
                presents.Add(_context.Presents.Single(present => present.Id == favouitesPresentsID[i]));
            }

            return View(presents);
        }

        public async Task<IActionResult> Add(int id)
        {
            var presentInDB = await _context.Presents.FirstOrDefaultAsync(e => e.Id == id);

            var favouritePresent = new Favourites();
            favouritePresent.UserID = _userService.GetUserId();
            favouritePresent.PresentId = presentInDB.Id;
            _context.Favourites.Add(favouritePresent);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
