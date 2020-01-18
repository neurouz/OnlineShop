using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class HomeController : Controller
    {
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return View();
        }
    }
}