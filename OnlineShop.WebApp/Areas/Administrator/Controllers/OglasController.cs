using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Model.Models;
using Microsoft.AspNetCore.Authorization;
using ReflectionIT.Mvc.Paging;
using OnlineShop.WebApp.ViewModels;

namespace OnlineShop.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class OglasController : Controller
    {
        private readonly Context _context;
        
        public OglasController(Context context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index(bool? act, int page = 1)
        {
            var query = act == null
                ?  _context.Oglas.AsNoTracking().OrderBy(o => o.Id)
                :  _context.Oglas.Where(o => o.Aktivan == act).AsNoTracking().OrderBy(o => o.Id);

            var model = await PagingList.CreateAsync(query, 10, page);
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oglas = await _context.Oglas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oglas == null)
            {
                return NotFound();
            }

            return View(oglas);
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Oglas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Create([Bind("Id,Naslov,Sadrzaj,BrojPozicija,Lokacija,DatumObjave,Trajanje,DatumIsteka,Aktivan")] Oglas oglas)
        {
            if (ModelState.IsValid)
            {
                oglas.IzracunajDatumIsteka();
                _context.Add(oglas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(oglas);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oglas = await _context.Oglas.FindAsync(id);
            if (oglas == null)
            {
                return NotFound();
            }
            oglas.IzracunajDatumIsteka();
            return View(oglas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naslov,Sadrzaj,BrojPozicija,Lokacija,DatumObjave,Trajanje,DatumIsteka,Aktivan")] Oglas oglas)
        {
            if (id != oglas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    oglas.IzracunajDatumIsteka();
                    _context.Update(oglas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OglasExists(oglas.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(oglas);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oglas = await _context.Oglas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oglas == null)
            {
                return NotFound();
            }

            return View(oglas);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oglas = await _context.Oglas.FindAsync(id);

            var x = _context.KorisnikOglas.Where(x => x.OglasId == id).ToList();
            _context.RemoveRange(x);

            _context.Oglas.Remove(oglas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Administrator")]
        private bool OglasExists(int id)
        {
            return _context.Oglas.Any(e => e.Id == id);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetPrijave(int oglasId)
        {
            List<KorisnikOglas> registrovani = await _context.KorisnikOglas
                .Where(x => x.OglasId == oglasId)
                .Include(x => x.Korisnik)
                .Include(x => x.Oglas)
                .ToListAsync();

            List<KorisnikOglasAuth> neregistrovani = await _context.KorisnikOglasAuth
                .Where(x => x.OglasId == oglasId)
                .Include(x => x.Oglas)
                .ToListAsync();

            List<KorisnikOglasModel> model = new List<KorisnikOglasModel>();

            foreach(var user in registrovani)
            {
                model.Add(new KorisnikOglasModel()
                {
                    BrojTelefona = user.Korisnik.PhoneNumber,
                    CVPath = user.PathCV,
                    DatumSlanja = user.DatumPrijave,
                    DatumIsteka = user.Oglas.DatumIsteka,
                    Email = user.Korisnik.Email,
                    Ime = user.Korisnik.Ime,
                    Prezime = user.Korisnik.Prezime,
                    Oglas = user.Oglas,
                    Registrovan = true
                });
            }

            foreach (var user in neregistrovani)
            {
                model.Add(new KorisnikOglasModel()
                {
                    BrojTelefona = user.BrojTelefona,
                    CVPath = user.CV,
                    DatumSlanja = user.DatumSlanja,
                    DatumIsteka = user.Oglas.DatumIsteka,
                    Email = user.Email,
                    Ime = user.Ime,
                    Prezime = user.Prezime,
                    Oglas = user.Oglas,
                    Registrovan = false
                });
            }

            model = model.OrderBy(x => x.Ime).ToList();

            return PartialView(model);
        }
    }
}