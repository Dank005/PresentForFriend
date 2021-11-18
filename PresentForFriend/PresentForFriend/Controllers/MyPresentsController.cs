using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentForFriend.Data;
using PresentForFriend.Models;
using System.Threading.Tasks;

namespace PresentForFriend.Controllers
{
    public class MyPresentsController : Controller
    {       
        private readonly ApplicationDbContext _context;

        public MyPresentsController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var presents = await _context.Presents.ToListAsync();
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

            _context.Presents.Remove(presentInDB);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
