using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentForFriend.Data;
using System.Threading.Tasks;

namespace PresentForFriend.Controllers
{
    public class CardInfoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CardInfoController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index(int id)
        {
            var presentInDB = await _context.Presents.FirstOrDefaultAsync(e => e.Id == id);

            return View(presentInDB);
        }
    }
}
