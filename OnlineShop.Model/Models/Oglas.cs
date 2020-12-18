using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace OnlineShop.Model.Models
{
    public class Oglas
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public int BrojPozicija { get; set; }
        public string Lokacija { get; set; }
        public DateTime DatumObjave { get; set; } = DateTime.Now;
        public int Trajanje { get; set; } = 0;
        public DateTime DatumIsteka { get; set; }
        public AppUser Autor { get; set; }
        public int AutorId { get; set; }
        public bool Aktivan { get; set; } = true;
        public Oglas IzracunajDatumIsteka()
        {
            DatumIsteka = DatumObjave.AddDays(Trajanje);
            return this;
        }
        public void SetNeaktivan()
        {
            this.Aktivan = false;
        }

        public bool IsAktivan()
        {
            return DateTime.Compare(DateTime.Now, DatumIsteka) < 0 ? true : false;
        }
        public string GetExpirationDays()
        {
            if (IsAktivan())
                return "Oglas ističe za " + ((DatumIsteka - DateTime.Now).Days).ToString() + " dana";
            return "Oglas nije aktivan";
        }
    }
}