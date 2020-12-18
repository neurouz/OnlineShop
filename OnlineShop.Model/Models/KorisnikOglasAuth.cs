using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Model.Models
{
    public class KorisnikOglasAuth
    {
        [PrimaryKey]
        public int Id { get; set; }
        public Oglas Oglas { get; set; }
        public int OglasId { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumSlanja { get; set; }
        public string BrojTelefona { get; set; }
        public string Email { get; set; }
        public string CV { get; set; }
    }
}
