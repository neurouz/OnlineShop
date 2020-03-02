using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class SettingsController : Controller
    {
        private UserManager<AppUser> userMgr { get; }
        private Context _context { get; }
        public SettingsController(UserManager<AppUser> mgr, Context context)
        {
            userMgr = mgr;
            _context = context;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PromijeniUsername()
        {
            return PartialView("PromijeniUsername");
        }

        [Authorize(Roles = "Administrator")]
        public async Task<bool> SpasiUsername(string userId, string noviUsername)
        {
            if (noviUsername == null)
            {
                return false;
            }

            var user = await userMgr.FindByIdAsync(userId);
            user.UserName = noviUsername;

            await userMgr.UpdateAsync(user);

            return true;
        }

    }
}