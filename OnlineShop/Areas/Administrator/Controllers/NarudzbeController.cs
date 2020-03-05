using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ViewModels;

namespace OnlineShop.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class NarudzbeController : Controller
    {
        private UserManager<AppUser> userMgr { get; }
        private Context _context { get; }
        public NarudzbeController(UserManager<AppUser> mgr, Context context)
        {
            userMgr = mgr;
            _context = context;
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            NarudzbaIndexVM model = new NarudzbaIndexVM()
            {
                narudzbe = _context.Narudzba.Include(n => n.Narucioc)
                    .Include(n => n.Dostavljac).ToList(),
                narudzbeStavke = _context.NarudzbaStavka.ToList(),
                proizvodi = _context.Proizvod.Include(p => p.uvoznik).ToList()
            };

            var kolekcija = new List<NarudzbaCollection>();
            foreach (var narudzba in model.narudzbe)
            {
                kolekcija.Add(new NarudzbaCollection()
                {
                    narudzba = narudzba, 
                    proizvodi = new List<Proizvod>(),
                    kolicine = new List<int>()
                });

                foreach (var stavka in model.narudzbeStavke)
                {
                    if (narudzba.Id == stavka.NarudzbaId)
                    {
                        kolekcija.Where(x => x.narudzba == narudzba).SingleOrDefault().proizvodi
                            .Add(model.proizvodi.Where(p => p.ProizvodID == stavka.ProizvodId).SingleOrDefault());
                    }
                }
            }

            foreach(var x in kolekcija)
                x.GenerisiCijenu();

            return View(kolekcija);
        }
    }
}