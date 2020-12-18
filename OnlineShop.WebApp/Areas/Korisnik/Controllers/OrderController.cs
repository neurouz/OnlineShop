using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ViewModels;

namespace OnlineShop.Areas.Korisnik.Controllers
{
    [Area("Korisnik")]
    public class OrderController : Controller
    {
        private readonly Context _context;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(Context context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Korisnik")]
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var model = new List<NarudzbaCollection>();
            var orders = _context.Narudzba.Include(x => x.Dostavljac)
                .Include(x => x.Narucioc)
                .Where(x => x.NaruciocId == currentUser.Id)
                .OrderByDescending(x => x.DatumKreiranjaNarudzbe);
            foreach(var order in orders)
            {
                var stavke = _context.NarudzbaStavka.Include(x => x.Proizvod)
                    .Where(x => x.NarudzbaId == order.Id);
                List<Proizvod> proizvodi = new List<Proizvod>();

                foreach(var stavka in stavke)
                {
                    proizvodi.Add(_context.Proizvod.Include(x => x.uvoznik).Where(x => x.ProizvodID == stavka.ProizvodId).FirstOrDefault());
                }

                var kolekcija = new NarudzbaCollection()
                {
                    narudzba = order,
                    proizvodi = proizvodi
                };

                kolekcija.PopuniContainer();
                kolekcija.GenerisiCijenu();

                model.Add(kolekcija);
            }

            return View(model);
        }
    }
}
