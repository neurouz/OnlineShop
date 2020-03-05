﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore;
using ClassLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.ViewModels;
using ReflectionIT.Mvc.Paging;

namespace OnlineShop.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class PostController : Controller
    {

        private readonly Context _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public PostController(Context context, IWebHostEnvironment henv)
        {
            _context = context;
            _hostingEnvironment = henv;
        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index(int page = 1)
        {
            var query = _context.Post.Include(x => x.Autor).AsNoTracking().OrderBy(p => p.Id);
            var model = await PagingList.CreateAsync(query, 3, page);

            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult DodajPost()
        {
            Post model = new Post();
            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SnimiPost(Post post, IFormFile slika)
        {
            if (slika != null)
            {
                if (slika.Length > 0)
                {
                    var path = _hostingEnvironment.WebRootPath + "\\pictures\\oglasi";
                    await using (var stream = new FileStream(Path.Combine(path, slika.FileName), FileMode.Create))
                    {
                        await slika.CopyToAsync(stream);
                    }
                    post.ImageLocation = "/pictures/oglasi/" + slika.FileName;
                }
            }
            else
            {
                post.ImageLocation = "/pictures/oglasi/no_image_post.png";
            }

            post.DatumObjave = DateTime.Now;
            _context.Post.Add(post);
            _context.SaveChanges();

            return RedirectToAction("Index", "Post");
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> ObrisiPost(int? id)
        {
            if (id == null)
                return NotFound();
            var post = await _context.Post.FindAsync(id);

            if (post == null)
                return NotFound();

            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> EditPost(int? id)
        {
            var model = new EditPostVM()
            {
                postEdit = await _context.Post.FindAsync(id)
            };

            return View(model);
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SpasiPromjene(EditPostVM model, IFormFile slika)
        {

            Post p = await _context.Post.FindAsync(model.postEdit.Id);
            p.AutorId = model.postEdit.AutorId;
            p.DatumObjave = DateTime.Now;
            p.Naslov = model.postEdit.Naslov;
            p.Sadrzaj = model.postEdit.Sadrzaj;

            if (slika != null)
            {
                if (slika.Length > 0)
                {
                    var path = _hostingEnvironment.WebRootPath + "\\pictures\\oglasi";
                    await using (var stream = new FileStream(Path.Combine(path, slika.FileName), FileMode.Create))
                    {
                        await slika.CopyToAsync(stream);
                    }
                    p.ImageLocation = "/pictures/oglasi/" + slika.FileName;
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Post");
        }

    }
}