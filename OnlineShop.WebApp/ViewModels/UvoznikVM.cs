using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShop.Model.Models;

namespace OnlineShop.ViewModels
{
    public class UvoznikVM
    {
        public Uvoznik uvoznik { get; set; } = new Uvoznik();
        public List<Drzava> sjedista { get; set; }
    }
}
