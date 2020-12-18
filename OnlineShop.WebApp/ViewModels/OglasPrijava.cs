using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.WebApp.ViewModels
{
    public class OglasPrijava
    {
        public int OglasId { get; set; }
        public string Username { get; set; }
        public string Naslov { get; set; }
        public IFormFile CV { get; set; }
    }
}
