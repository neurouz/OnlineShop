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
        private Context _context { get; }
        public AccountController(Context context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<AppRole> roleManager)
        {
            userMgr = userManager;
            signInMgr = signInManager;
            roleMgr = roleManager;
            _context = context;
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

                Drzava drzava = new Drzava()
                {
                    Naziv = "Bosna i Hercegovina"
                };

                _context.Drzava.Add(drzava);
                _context.SaveChanges();

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
                user.SjedisteId = _context.Drzava.Find(drzava.Id).Id;

                var korisnik1 = new AppUser();

                korisnik1.UserName = "Korisnik-1";
                korisnik1.Email = "k1_mail@gmail.com";
                korisnik1.EmailConfirmed = true;
                korisnik1.DatumRegistracije = DateTime.Now;
                korisnik1.Ime = "Korisnik 1";
                korisnik1.Prezime = "Prezime 1";
                korisnik1.PhoneNumber = "061666584";
                korisnik1.Spol = 'Z';
                korisnik1.SjedisteId = _context.Drzava.Find(drzava.Id).Id;

                var korisnik2 = new AppUser();

                korisnik2.UserName = "Korisnik-2";
                korisnik2.Email = "k2_mail@gmail.com";
                korisnik2.EmailConfirmed = true;
                korisnik2.DatumRegistracije = DateTime.Now;
                korisnik2.Ime = "Korisnik 2";
                korisnik2.Prezime = "Prezime 2";
                korisnik2.PhoneNumber = "066547855";
                korisnik2.Spol = 'M';
                korisnik2.SjedisteId = _context.Drzava.Find(drzava.Id).Id;

                var korisnik3 = new AppUser();

                korisnik3.UserName = "Korisnik-3";
                korisnik3.Email = "k3_mail@gmail.com";
                korisnik3.EmailConfirmed = true;
                korisnik3.DatumRegistracije = DateTime.Now;
                korisnik3.Ime = "Korisnik 3";
                korisnik3.Prezime = "Prezime 3";
                korisnik3.PhoneNumber = "062114412";
                korisnik3.Spol = 'Z';
                korisnik3.SjedisteId = _context.Drzava.Find(drzava.Id).Id;

                IdentityResult chkUser = await userMgr.CreateAsync(user, "$123abCdE!");
                IdentityResult chkUser1 = await userMgr.CreateAsync(korisnik1, "Pa$$w0rD=");
                IdentityResult chkUser2 = await userMgr.CreateAsync(korisnik2, "Ta$$t4turA=");
                IdentityResult chkUser3 = await userMgr.CreateAsync(korisnik3, "T3$tuS3r=");

                // Dodaje se rola "Administrator" na prethodno dodani nalog
                if (chkUser.Succeeded)
                {
                    var result1 = await userMgr.AddToRoleAsync(user, "Administrator");
                    var result2 = await userMgr.AddToRoleAsync(korisnik1, "Korisnik");
                    var result3 = await userMgr.AddToRoleAsync(korisnik2, "Korisnik");
                    var result4 = await userMgr.AddToRoleAsync(korisnik3, "Korisnik");
                }
            }

            return Content("Administrator dodan. Korisnička rola dodana. Ne pozivati ovu funkciju ponovo ako baza postoji.");
        }

    }
}