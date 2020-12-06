using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Model.Models
{
    public class Proizvodjac
    {
        public int Id { get; set; }
        public string NazivProizvodjaca { get; set; }
        public Drzava Sjediste { get; set; }
        public int SjedisteId { get; set; }
    }
}
