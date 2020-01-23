using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ClassLibrary.Models
{
    public class DataFill
    {

        public static void EmptyDatabase(Context _context)
        {
            _context.kategorije.RemoveRange(_context.kategorije);
            _context.SaveChanges();

            _context.uvoznici.RemoveRange(_context.uvoznici);
            _context.SaveChanges();

            _context.proizvodi.RemoveRange(_context.proizvodi);
            _context.SaveChanges();

            _context.recenzije.RemoveRange(_context.recenzije);
            _context.SaveChanges();

        }
        public static bool IsEmptyDB(Context _context)
        {
            return _context.proizvodi.Count() == 0;
        }
        public static bool LoadData(Context _context)
        {

            int brojSlike = 1;

            try
            {

                _context.kategorije.Add(new Kategorija() { NazivKategorije = "Računari" });
                _context.SaveChanges();

                _context.kategorije.Add(new Kategorija() { NazivKategorije = "Laptopi" });
                _context.SaveChanges();

                _context.kategorije.Add(new Kategorija() { NazivKategorije = "Monitori" });
                _context.SaveChanges();

                _context.kategorije.Add(new Kategorija() { NazivKategorije = "Periferija" });
                _context.SaveChanges();

                _context.kategorije.Add(new Kategorija() { NazivKategorije = "Slušalice" });
                _context.SaveChanges();

                _context.kategorije.Add(new Kategorija() { NazivKategorije = "Procesori" });
                _context.SaveChanges();

                _context.kategorije.Add(new Kategorija() { NazivKategorije = "Grafičke kartice" });
                _context.SaveChanges();

                _context.kategorije.Add(new Kategorija() { NazivKategorije = "Hladnjaci" });
                _context.SaveChanges();

                _context.kategorije.Add(new Kategorija() { NazivKategorije = "Matične ploče" });
                _context.SaveChanges();

                _context.kategorije.Add(new Kategorija() { NazivKategorije = "Kućišta" });
                _context.SaveChanges();

                _context.kategorije.Add(new Kategorija() { NazivKategorije = "Mobilni uređaji" });
                _context.SaveChanges();

                _context.uvoznici.Add(new Uvoznik() { AdresaUvoznika = "Adresa-1", BrojTelefona = "061554777", NazivUvoznika = "Doper Tech" });
                _context.SaveChanges();

                _context.uvoznici.Add(new Uvoznik() { AdresaUvoznika = "Adresa-2", BrojTelefona = "033666584", NazivUvoznika = "Univerzalno" });
                _context.SaveChanges();

                _context.uvoznici.Add(new Uvoznik() { AdresaUvoznika = "Adresa-3", BrojTelefona = "064112027", NazivUvoznika = "UEFI Tech" });
                _context.SaveChanges();

                _context.uvoznici.Add(new Uvoznik() { AdresaUvoznika = "Adresa-4", BrojTelefona = "061258741", NazivUvoznika = "BIOS Tech" });
                _context.SaveChanges();

                _context.uvoznici.Add(new Uvoznik() { AdresaUvoznika = "Adresa-5", BrojTelefona = "036587412", NazivUvoznika = "Domod" });
                _context.SaveChanges();

                _context.uvoznici.Add(new Uvoznik() { AdresaUvoznika = "Adresa-6", BrojTelefona = "063335874", NazivUvoznika = "Target PC" });
                _context.SaveChanges();

                _context.uvoznici.Add(new Uvoznik() { AdresaUvoznika = "Adresa-7", BrojTelefona = "0603336647", NazivUvoznika = "PC Shop" });
                _context.SaveChanges();

                _context.uvoznici.Add(new Uvoznik() { AdresaUvoznika = "Adresa-8", BrojTelefona = "065551550", NazivUvoznika = "Garmin" });
                _context.SaveChanges();

                _context.uvoznici.Add(new Uvoznik() { AdresaUvoznika = "Adresa-9", BrojTelefona = "062258733", NazivUvoznika = "Pro Comp" });
                _context.SaveChanges();

                _context.uvoznici.Add(new Uvoznik() { AdresaUvoznika = "Adresa-10", BrojTelefona = "064744110", NazivUvoznika = "Intelcom" });
                _context.SaveChanges();

                _context.uvoznici.Add(new Uvoznik() { AdresaUvoznika = "Adresa-11", BrojTelefona = "063312211", NazivUvoznika = "Media Market" });
                _context.SaveChanges();

                _context.proizvodi.Add(new Proizvod()
                {
                    NazivProizvoda = "HP Laptop",
                    Cijena = 859.99,
                    Kolicina = 8,
                    uvoznikId = 1,
                    kategorijaId = 3,
                    OpisProizvoda = "Mnogo dobar laptop, nemam rijeci zaista.",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg"
                });
                _context.SaveChanges();

                _context.proizvodi.Add(new Proizvod()
                {
                    NazivProizvoda = "DELL Laptop",
                    Cijena = 954.99,
                    Kolicina = 12,
                    uvoznikId = 3,
                    kategorijaId = 2,
                    OpisProizvoda = "Mnogo dobar laptop, nemam rijeci zaista.",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg"
                });
                _context.SaveChanges();

                _context.proizvodi.Add(new Proizvod()
                {
                    NazivProizvoda = "Canyon Slušalice",
                    Cijena = 59.99,
                    Kolicina = 25,
                    uvoznikId = 4,
                    kategorijaId = 5,
                    OpisProizvoda = "Slusalice Headhunterz hi koristio.",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg"
                });
                _context.SaveChanges();

                _context.proizvodi.Add(new Proizvod()
                {
                    NazivProizvoda = "DELL Monitor",
                    Cijena = 319.90,
                    Kolicina = 25,
                    uvoznikId = 6,
                    kategorijaId = 7,
                    OpisProizvoda = "60 Herca drama dobar",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg"
                });
                _context.SaveChanges();

                _context.proizvodi.Add(new Proizvod()
                {
                    NazivProizvoda = "iPhone Slušalice",
                    Cijena = 112.49,
                    Kolicina = 60,
                    uvoznikId = 1,
                    kategorijaId = 7,
                    OpisProizvoda = "Slusalice za ajfon",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg"
                });
                _context.SaveChanges();

                _context.proizvodi.Add(new Proizvod()
                {
                    NazivProizvoda = "RX590 8GB GDDR6",
                    Cijena = 420,
                    Kolicina = 150,
                    uvoznikId = 8,
                    kategorijaId = 9,
                    OpisProizvoda = "Graficka za najnovije igre!",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg"
                });
                _context.SaveChanges();

                _context.proizvodi.Add(new Proizvod()
                {
                    NazivProizvoda = "DDR4 Viper 3200MHz 16GB (8x8)",
                    Cijena = 159.99,
                    Kolicina = 5,
                    uvoznikId = 5,
                    kategorijaId = 9,
                    OpisProizvoda = "Brrrrzzzzzzina.",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg"
                });
                _context.SaveChanges();

                _context.proizvodi.Add(new Proizvod()
                {
                    NazivProizvoda = "AMD Ryzen 2700x 3.6GHz",
                    Cijena = 374.99,
                    Kolicina = 50,
                    uvoznikId = 3,
                    kategorijaId = 3,
                    OpisProizvoda = "Up to 4.4GHz",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg"
                });
                _context.SaveChanges();

                _context.proizvodi.Add(new Proizvod()
                {
                    NazivProizvoda = "RIOTORO Napojna 600W",
                    Cijena = 100,
                    Kolicina = 9,
                    uvoznikId = 4,
                    kategorijaId = 1,
                    OpisProizvoda = "Napaja sve moguce i nemoguce.",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg"
                });
                _context.SaveChanges();

                _context.proizvodi.Add(new Proizvod()
                {
                    NazivProizvoda = "PC LED Cooler",
                    Cijena = 44.90,
                    Kolicina = 20,
                    uvoznikId = 4,
                    kategorijaId = 8,
                    OpisProizvoda = "RGB LED Cooler za procesor.",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg"
                });
                _context.SaveChanges();

                _context.proizvodi.Add(new Proizvod()
                {
                    NazivProizvoda = "Bežična tastatura",
                    Cijena = 34.90,
                    Kolicina = 14,
                    uvoznikId = 2,
                    kategorijaId = 9,
                    OpisProizvoda = "Bežična tastatura sa žicama.",
                    imageLocation = "/pictures/slika-" + brojSlike++ + ".jpg"
                });
                _context.SaveChanges();

                _context.recenzije.Add(new Recenzija()
                {
                    Ocjena = 5,
                    Komentar = "Jako dobar proizvod.",
                    ProizvodId = 4
                });
                _context.SaveChanges();

                _context.recenzije.Add(new Recenzija()
                {
                    Ocjena = 4,
                    Komentar = "Zadovoljan/na sam.",
                    ProizvodId = 10
                });
                _context.SaveChanges();

                _context.recenzije.Add(new Recenzija()
                {
                    Ocjena = 3,
                    Komentar = "Za ovu cijenu može i bolji kvalitet!",
                    ProizvodId = 6
                });
                _context.SaveChanges();

                _context.recenzije.Add(new Recenzija()
                {
                    Ocjena = 2,
                    Komentar = "Kvalitet veoma loš, sreća pa je jeftino..",
                    ProizvodId = 2
                });
                _context.SaveChanges();

                _context.recenzije.Add(new Recenzija()
                {
                    Ocjena = 1,
                    Komentar = "Želim svoj novac nazad!",
                    ProizvodId = 9
                });
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public static bool LoadOglasi(Context _context)
        {
            try
            {
                _context.oglasi.Add(new Oglas()
                {
                    Naslov = "Tehničar",
                    Lokacija = "Sarajevo",
                    Sadrzaj = "Potrebno više tehničara za rad sa informatičkom opremom. Radno iskustvo nije neophodno, ali je poželjno. Kandidati se mogu prijaviti putem opcije 'Prijavi se na oglas'",
                    DatumObjave = DateTime.Now,
                    Trajanje = 10,
                    Aktivan = true,
                    PrijavljeniKorisnici = new List<AppUser>(),
                    BrojPozicija = 3
                }.IzracunajDatumIsteka());

                _context.SaveChanges();

                _context.oglasi.Add(new Oglas()
                {
                    Naslov = "Programer",
                    Lokacija = "Tuzla",
                    Sadrzaj = "Java programer sa najmanje 2 godine iskustva. Potrebno odlično poznavanje Java Spring i MVC. Poželjno je poznavanje Linux OS. Kandidati se mogu prijaviti putem opcije 'Prijavi se na oglas'",
                    DatumObjave = DateTime.Now,

                    Trajanje = 5,
                    Aktivan = true,
                    PrijavljeniKorisnici = new List<AppUser>(),
                    BrojPozicija = 3
                }.IzracunajDatumIsteka());

                _context.SaveChanges();

                _context.oglasi.Add(new Oglas()
                {
                    Naslov = "Direktor",
                    Lokacija = "Sarajevo",
                    Sadrzaj = "Poslovnica u Sarajevu trenutno je u haosu jer nema direktora. Potrebna je odgovorna osoba koja će sjediti 8 sati dnevno u fotelji i pratiti radnike na kameri, te zvati ih u slučaju da odmaraju, jedu, pričaju na telefon i slično.",
                    DatumObjave = DateTime.Now,
                    Trajanje = 20,
                    Aktivan = true,
                    PrijavljeniKorisnici = new List<AppUser>(),
                    BrojPozicija = 1
                }.IzracunajDatumIsteka());

                _context.SaveChanges();

                _context.oglasi.Add(new Oglas()
                {
                    Naslov = "Direktor",
                    Lokacija = "Tuzla",
                    Sadrzaj = "Oglas je zatvoren.",
                    DatumObjave = DateTime.Now,
                    Trajanje = 10,
                    Aktivan = false,
                    PrijavljeniKorisnici = new List<AppUser>(),
                    BrojPozicija = 1
                }.IzracunajDatumIsteka());

                _context.SaveChanges();
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
