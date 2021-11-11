using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentForFriend.Controllers
{
    public class DiscussingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
