using OnlineShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.WebApp.ViewModels
{
    public class KorisnikOglasModel
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumSlanja { get; set; }
        public DateTime DatumIsteka { get; set; }
        public string BrojTelefona { get; set; }
        public string Email { get; set; }
        public string CVPath { get; set; }
        public Oglas Oglas { get; set; }
        public bool Registrovan { get; set; }
    }
}
