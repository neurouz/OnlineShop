using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OnlineShop.ViewModels;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace OnlineShop.Areas.Korisnik.Controllers
{
    [Area("Korisnik")]
    public class CartController : Controller
    {
        private Context _context;
        private UserManager<AppUser> _userManager;
        public CartController(Context context, UserManager<AppUser> usermgr)
        {
            _context = context;
            _userManager = usermgr;
        }

        [Authorize(Roles = "Korisnik")]
        [HttpPost]
        public IActionResult Index(string data)
        {
            var items = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            List<CartViewModel> model = new List<CartViewModel>();
            foreach(var item in items)
            {
                model.Add(new CartViewModel()
                {
                    Proizvod = _context.Proizvod.Include(x => x.kategorija).Where(x => x.ProizvodID == int.Parse(item.Key)).FirstOrDefault(),
                    Kolicina = int.Parse(item.Value)
                });
            }
            double ukupnaCijena = 0;

            foreach(var product in model)
                ukupnaCijena += product.Proizvod.Cijena * product.Kolicina;
            
            ViewBag.Ukupno = ukupnaCijena;

            return View(model);
        }

        [Authorize(Roles = "Korisnik")]
        [HttpPost]
        public async Task<IActionResult> Confirm(string data, string userId)
        {
            var items = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
            if (items.Count == 0)
                return Content("Korpa je prazna");

            var user = await _userManager.FindByIdAsync(userId);
            var order = new Narudzba()
            {
                Aktivna = true,
                DatumKreiranjaNarudzbe = DateTime.Now,
                NaruciocId = user.Id,
                Potvrdjena = false,
                DostavljacId = 1
            };

            var orderResult = await _context.Narudzba.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                for (int i = 0; i < int.Parse(item.Value); i++)
                {
                    var stavka = new NarudzbaStavka()
                    {
                        NarudzbaId = orderResult.Entity.Id,
                        ProizvodId = int.Parse(item.Key)
                    };
                    await _context.NarudzbaStavka.AddAsync(stavka);
                }
                await _context.SaveChangesAsync();
            }

            await _context.SaveChangesAsync();
            return Content("");
        }
    }
}
