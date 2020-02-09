using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClassLibrary.Models
{
    public class AppUser : IdentityUser<int>
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public char Spol { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public Drzava Sjediste { get; set; }
        public int SjedisteId { get; set; }
    }
}
