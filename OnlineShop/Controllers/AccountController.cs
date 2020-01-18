using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Models;
using OnlineShop.ViewModels;

namespace OnlineShop.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> userMgr { get; }
        private SignInManager<AppUser> signInMgr { get; }
        private RoleManager<AppRole> roleMgr { get; }
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            userMgr = userManager;
            signInMgr = signInManager;
            roleMgr = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Authorize(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInMgr.PasswordSignInAsync(model.Username,
                   model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if(model.Username == "Neurouz")
                    {
                        TempData["login"] = "Uspješno ste logovani.";
                        return RedirectToAction("Index", "Home", new { area = "Administrator" });
                    }
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid login attempt");
            return PartialView("BadLogin");
        }

        public async Task<IActionResult> Logout()
        {
            TempData["signout"] = "Uspješno ste odjavljeni!";
            await signInMgr.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AddUser(AppUser noviKorisnik)
        {
            try
            {
                TempData["sucess"] = 1;

                // Pretraga ako korisnik vec postoji
                AppUser user = await userMgr.FindByNameAsync(noviKorisnik.UserName);

                // Ako korisnik ne postoji
                if (user == null)
                {
                    noviKorisnik.EmailConfirmed = true;
                    noviKorisnik.PhoneNumberConfirmed = true;
                    noviKorisnik.TwoFactorEnabled = false;
                    noviKorisnik.LockoutEnabled = false;
                    noviKorisnik.AccessFailedCount = 1;
                    noviKorisnik.DatumRegistracije = DateTime.Now;

                    // Password mora sadrzati mala, velika slova, brojeve i najmanje jedan specijalni znak.
                    IdentityResult result = await userMgr.CreateAsync(noviKorisnik, noviKorisnik.PasswordHash);
                    TempData["sucess"] = 1;

                    if (result.Succeeded)
                    {
                        var result1 = await userMgr.AddToRoleAsync(noviKorisnik, "Korisnik");
                    }
                }
                else
                {
                    TempData["sucess"] = 0;
                }
            }
            
            catch (Exception e)
            {
                TempData["sucess"] = e.Message;
            }
            return PartialView("RegistrationMessage");
        }

        // Ovu metodu treba obrisati nakon završetka projekta
        public async Task<IActionResult> CreateRolesOnce()
        {
            bool x = await roleMgr.RoleExistsAsync("Administrator");
            if (!x)
            {
                // Kreiramo rolu za Administratora
                var role = new AppRole
                {
                    Name = "Administrator"
                };
                await roleMgr.CreateAsync(role);

                // Kreiramo rolu za Korisnika
                var roleKorisnik = new AppRole
                {
                    Name = "Korisnik"
                };
                await roleMgr.CreateAsync(roleKorisnik);

                // Kreira se nalog za Administratora
                var user = new AppUser();

                user.UserName = "Neurouz";
                user.Email = "neurouzmedia@gmail.com";
                user.EmailConfirmed = true;
                user.DatumRegistracije = DateTime.Now;
                user.Ime = "Ajdin";
                user.Prezime = "Hukara";
                user.PhoneNumber = "061550134";
                user.Spol = 'M';

                IdentityResult chkUser = await userMgr.CreateAsync(user, "$123abCdE!");

                // Dodaje se rola "Administrator" na prethodno dodani nalog
                if (chkUser.Succeeded)
                {
                    var result1 = await userMgr.AddToRoleAsync(user, "Administrator");
                }
            }

            return Content("Administrator dodan. Korisnička rola dodana. Ne pozivati ovu funkciju ponovo ako baza postoji.");
        }

    }
}