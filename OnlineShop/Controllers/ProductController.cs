﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var model = new List<ClassLibrary.Models.Proizvod>();
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

        public void AppendToCart(int productId)
        {
            CookieOptions options = new CookieOptions(){ Expires = DateTime.Now.AddMinutes(30) };
            var cookies = Request.Cookies["product"];

            if (cookies != null)
            {
                cookies += "_" + productId;
                Response.Cookies.Append("product", cookies);
            }
            else
                Response.Cookies.Append("product", productId.ToString());
        }
    }
}
