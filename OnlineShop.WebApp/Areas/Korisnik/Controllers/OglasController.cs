using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Controllers;
using OnlineShop.Model.Models;
using OnlineShop.WebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace OnlineShop.WebApp.Areas.Korisnik.Controllers
{
    [Area("Korisnik")]
    public class OglasController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInMgr { get; }
        private RoleManager<AppRole> _roleMgr { get; }
        private readonly IWebHostEnvironment _hostingEnvironment;

        public OglasController(Context context, UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInMgr, RoleManager<AppRole> roleMgr, IWebHostEnvironment environment)
        {
            _context = context;
            _userManager = userManager;
            _signInMgr = signInMgr;
            _roleMgr = roleMgr;
            _hostingEnvironment = environment;
        }
        public IActionResult Index(string note = null)
        {
            ViewData["note"] = note;

            var model = _context.Oglas
                .Include(x => x.Autor)
                .Where(x => x.Aktivan).ToList();

            return View(model);
        }

        public IActionResult PrijavaGuest(int oglasId)
        {
            var naziv = _context.Oglas.FirstOrDefault(x => x.Id == oglasId).Naslov;
            var model = new OglasPrijavaGuest()
            {
                OglasId = oglasId,
                Naslov = naziv
            };
            return View(model);
        }

        public IActionResult Prijava(int oglasId)
        {
            var naziv = _context.Oglas.FirstOrDefault(x => x.Id == oglasId).Naslov;
            var model = new OglasPrijava()
            {
                OglasId = oglasId,
                Naslov = naziv
            };
            return View(model);
        }

        public async Task<IActionResult> PosaljiPrijavuGuest(OglasPrijavaGuest prijava, IFormFile CV)
        {
            var korisnik = new KorisnikOglasAuth()
            {
                BrojTelefona = prijava.BrojTelefona,
                Prezime = prijava.Prezime,
                DatumSlanja = DateTime.Now,
                Email = prijava.Email,
                Ime = prijava.Ime,
                OglasId = prijava.OglasId
            };

            _context.KorisnikOglasAuth.Add(korisnik);
            await _context.SaveChangesAsync();

            var entity = await _context.KorisnikOglasAuth.FirstOrDefaultAsync(x => x.Ime == korisnik.Ime && x.Prezime == korisnik.Prezime);

            if (CV != null)
            {
                if (CV.Length > 0)
                {
                    // wwwroot/CV/UserID/
                    var path = Path.Combine(_hostingEnvironment.WebRootPath, "CV", entity.Id.ToString());
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    // wwwroot/CV/UserID/OglasID.pdf
                    var filename = prijava.OglasId.ToString();
                    var extension = Path.GetExtension(CV.FileName);

                    var fullPath = Path.Combine("CV", entity.Id.ToString(), filename + extension).Replace("\\", "_");

                    var bytes = Encoding.UTF8.GetBytes(fullPath);
                    var encodedString = Convert.ToBase64String(bytes);
                    
                    entity.CV = encodedString;

                    using (var stream = new FileStream(Path.Combine(path, filename + extension), FileMode.Create))
                    {
                        await CV.CopyToAsync(stream);
                    }
                }
            }
            else
            {
                entity.CV = null;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Oglas", new { note = "Prijava poslana!" });
        }

        public async Task<IActionResult> PosaljiPrijavu(OglasPrijava prijava, IFormFile CV)
        {
            var user = await _userManager.FindByNameAsync(prijava.Username);
            var korisnik = new KorisnikOglas()
            {
                DatumPrijave = DateTime.Now,
                KorisnikId = user.Id,
                OglasId = prijava.OglasId
            };

            _context.KorisnikOglas.Add(korisnik);
            await _context.SaveChangesAsync();

            var entity = await _context.KorisnikOglas.FirstOrDefaultAsync(x => x.KorisnikId == user.Id);

            if (CV != null)
            {
                if (CV.Length > 0)
                {
                    // wwwroot/CV/UserID/
                    var path = Path.Combine(_hostingEnvironment.WebRootPath, "CV-R", entity.Id.ToString());
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    // wwwroot/CV/UserID/OglasID.pdf
                    var filename = prijava.OglasId.ToString();
                    var extension = Path.GetExtension(CV.FileName);

                    var fullPath = Path.Combine("CV-R", entity.Id.ToString(), filename + extension).Replace("\\", "_");

                    var bytes = Encoding.UTF8.GetBytes(fullPath);
                    var encodedString = Convert.ToBase64String(bytes);
                    
                    entity.PathCV = encodedString;

                    using (var stream = new FileStream(Path.Combine(path, filename + extension), FileMode.Create))
                    {
                        await CV.CopyToAsync(stream);
                    }
                }
            }
            else
            {
                entity.PathCV = null;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Oglas", new {note = "Prijava poslana!"});
        }
    }
}