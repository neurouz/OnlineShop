using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Models
{
    public class KorisnikProizvod
    {
        public int Id { get; set; }
        public AppUser Korisnik { get; set; }
        public int KorisnikId { get; set; }

        public Proizvod Proizvod { get; set; }
        public int ProizvodId { get; set; }

    }
}
