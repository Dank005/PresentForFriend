using Microsoft.AspNetCore.Mvc;

namespace PresentForFriend.Controllers
{
    public class CardsController : Controller
    {
        [Route("cards/{id}")]
        public IActionResult Index(int id) //показывает в View/Cards файл Index
        {
            ViewBag.ItemId = id;

            return View();
        }
    }
}
