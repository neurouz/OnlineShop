using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.WebApp.ViewModels
{
    public class OglasPrijavaGuest
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string BrojTelefona { get; set; }
        public string Email { get; set; }
        public IFormFile CV { get; set; }
        public int OglasId { get; set; }
        public string Naslov { get; set; }
    }
}
