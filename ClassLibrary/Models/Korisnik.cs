using ServiceStack.DataAnnotations;
using System;

namespace ClassLibrary.Models
{
    public class Korisnik
    {
        [PrimaryKey]
        public int KorisnikID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string BrojTelefona { get; set; }
        public string Username { get; set; }
        // Pomocu funkcija Encrypt i Decrypt u klasi StringOptions u bazu ce se pohranjivati enkriptovan password
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DatumRegistracije { get; set; }
    }
}
