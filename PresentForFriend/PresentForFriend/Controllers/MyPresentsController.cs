using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentForFriend.Controllers
{
    public class MyPresentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
