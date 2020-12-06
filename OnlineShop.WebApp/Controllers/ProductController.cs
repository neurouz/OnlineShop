using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Model.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Models;
using Org.BouncyCastle.Ocsp;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly Context _context;
        public ProductController(Context context)
        {
            _context = context;
        }
        public IActionResult Index(int? categoryId)
        {
            var query = _context.Proizvod.Include(x => x.kategorija).AsQueryable();
            if (categoryId != null)
                query = query.Where(x => x.kategorijaId == categoryId);
            var products = query.ToList();
            var model = new List<OnlineShop.Model.Models.Proizvod>();
            foreach(var product in products)
            {
                model.Add(new Proizvod()
                {
                    ProizvodID = product.ProizvodID,
                    kategorija = product.kategorija,
                    ProizvodjacId = product.ProizvodjacId,
                    NazivProizvoda = product.NazivProizvoda,
                    OpisProizvoda = product.OpisProizvoda,
                    Cijena = product.Cijena,
                    imageLocation = product.imageLocation,
                    Kolicina = product.Kolicina,
                    kategorijaId = product.kategorijaId,
                    snizen = product.snizen,
                    uvoznikId = product.uvoznikId
                });
            }

            return View(model);
        }

        public async Task<IActionResult> Details(int productId)
        {
            Proizvod product = await _context.Proizvod.FindAsync(productId);
            return product != null ? 
                View(product) : 
                View("Error", new ErrorViewModel() { RequestId = "Proizvod ne postoji u bazi" });
        }
    }
}