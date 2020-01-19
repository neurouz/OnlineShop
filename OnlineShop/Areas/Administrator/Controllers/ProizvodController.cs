using ClassLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class ProizvodController : Controller
    {
        private readonly Context _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ProizvodController(Context _temp, IHostingEnvironment hostingEnvironment)
        {
            _context = _temp;
            _hostingEnvironment = hostingEnvironment;
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            ProizvodViewModel model = new ProizvodViewModel() { proizvodi = _context.proizvodi.Distinct().ToList() };
            _context.SaveChanges();
            return View(model);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult AddProizvod()
        {
            ProizvodViewModel model = new ProizvodViewModel()
            {
                uvoznici = _context.uvoznici.ToList(),
                kategorije = _context.kategorije.ToList()
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> DodajNoviProizvodv2(Proizvod proizvod, IFormFile slikaProizvoda)
        {
            if (slikaProizvoda != null)
            {
                if (slikaProizvoda.Length > 0)
                {
                    var path = _hostingEnvironment.WebRootPath + "\\pictures";
                    proizvod.imageLocation = "/pictures/" + slikaProizvoda.FileName;

                    using (var stream = new FileStream(Path.Combine(path, slikaProizvoda.FileName), FileMode.Create))
                    {
                        await slikaProizvoda.CopyToAsync(stream);
                    }
                }
            }
            else
            {
                proizvod.imageLocation = "/pictures/no_image.svg";
            }

            _context.proizvodi.Add(proizvod);
            _context.SaveChanges();

            return View("Redirector");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult UkloniProizvod(int id)
        {
            Proizvod p = new Proizvod() { ProizvodID = id };
            _context.proizvodi.Attach(p);
            _context.proizvodi.Remove(p);
            _context.SaveChanges();

            return Redirect("~/Administrator/Proizvod");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult EditRedirect(int id)
        {

            EditViewModel model = new EditViewModel()
            {
                uvoznici = _context.uvoznici.ToList(),
                kategorije = _context.kategorije.ToList(),
                proizvodEdit = _context.proizvodi.Find(id),
                staraCijena = _context.proizvodi.Find(id).Cijena
            };
            
            return View("EditProizvod", model);
        }

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> EditujProizvod(EditViewModel mdl)
        {
            Proizvod p = _context.proizvodi.Find(mdl.proizvodEdit.ProizvodID);

            p.NazivProizvoda = mdl.proizvodEdit.NazivProizvoda;
            p.Cijena = mdl.proizvodEdit.Cijena;
            p.Kolicina = mdl.proizvodEdit.Kolicina;
            p.kategorijaId = mdl.proizvodEdit.kategorijaId;
            p.uvoznikId = mdl.proizvodEdit.uvoznikId;
            p.OpisProizvoda = mdl.proizvodEdit.OpisProizvoda;

            p.snizen = mdl.proizvodEdit.Cijena < mdl.staraCijena ? true : false;

            if (mdl.slikaProizvoda != null)
            {
                if (mdl.slikaProizvoda.Length > 0)
                {
                    var path = _hostingEnvironment.WebRootPath + "\\pictures";
                    p.imageLocation = "/pictures/" + mdl.slikaProizvoda.FileName;

                    using (var stream = new FileStream(Path.Combine(path, mdl.slikaProizvoda.FileName), FileMode.Create))
                    {
                        await mdl.slikaProizvoda.CopyToAsync(stream);
                    }
                }
            }

            _context.SaveChanges();
            return Redirect("~/Administrator/Proizvod");
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Detaljno(int id)
        {
            var product = _context.proizvodi.Find(id);

            if (product == null)
                return Content("Nema");

            var model = _context.proizvodi
                .Include(p => p.kategorija)
                .Include(p => p.uvoznik).ToList().Find(p => p.ProizvodID.Equals(product.ProizvodID));

            return View(model);
            
        }
    }
}