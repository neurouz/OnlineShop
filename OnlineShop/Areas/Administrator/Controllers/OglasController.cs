using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassLibrary.Models;

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

        // GET: Administrator/Oglas
        public async Task<IActionResult> Index(bool? act)
        {
            return act == null ? View(await _context.oglasi.ToListAsync()) :
            View(await _context.oglasi.Where(o => o.Aktivan == act).ToListAsync());
        }

        // GET: Administrator/Oglas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oglas = await _context.oglasi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oglas == null)
            {
                return NotFound();
            }

            return View(oglas);
        }

        // GET: Administrator/Oglas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Oglas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: Administrator/Oglas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oglas = await _context.oglasi.FindAsync(id);
            if (oglas == null)
            {
                return NotFound();
            }
            oglas.IzracunajDatumIsteka();
            return View(oglas);
        }

        // POST: Administrator/Oglas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: Administrator/Oglas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var oglas = await _context.oglasi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (oglas == null)
            {
                return NotFound();
            }

            return View(oglas);
        }

        // POST: Administrator/Oglas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var oglas = await _context.oglasi.FindAsync(id);
            _context.oglasi.Remove(oglas);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OglasExists(int id)
        {
            return _context.oglasi.Any(e => e.Id == id);
        }
    }
}
