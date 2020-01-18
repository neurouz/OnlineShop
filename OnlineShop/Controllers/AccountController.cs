using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> userMgr { get; }
        private SignInManager<AppUser> signInMgr { get; }

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            userMgr = userManager;
            signInMgr = signInManager;
        }
        public async Task<IActionResult> Register()
        {
            try
            {
                ViewBag.Message = "Korisnik je već registrovan!";

                AppUser user = await userMgr.FindByNameAsync("TestKorisnik");
                if (user == null)
                {
                    user = new AppUser();
                    user.UserName = "TestKorisnik";
                    user.Email = "test.korisnik@gmail.com";
                    user.Ime = "Ajdin";
                    user.Prezime = "Hukara";

                    IdentityResult result = await userMgr.CreateAsync(user, "neurouzmedia");
                    ViewBag.Message = "Novi korisnik je kreiran!";
                }

            }
            catch (Exception e)
            { 
                ViewBag.Message = e.Message;
            }

            return View();
        }
    }
}