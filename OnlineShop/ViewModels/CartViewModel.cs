using ClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.ViewModels
{
    public class CartViewModel
    {
        public Proizvod Proizvod { get; set; }
        public int Kolicina { get; set; }
    }
}
