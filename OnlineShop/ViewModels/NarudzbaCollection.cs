using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClassLibrary.Models;

namespace OnlineShop.ViewModels
{
    public class NarudzbaCollection
    {
        public Narudzba narudzba { get; set; }
        public List<Proizvod> proizvodi { get; set; }
        public List<int> kolicine { get; set; }
        public double UkupnaCijena { get; set; }

        public void GenerisiCijenu()
        {
            double temp = 0;
            foreach (var proizvod in proizvodi)
                temp += proizvod.Cijena;

            UkupnaCijena = Math.Round(temp, 2);
        }
    }
}
