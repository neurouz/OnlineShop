﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Model.Models
{
    public class NarudzbaStavka
    {
        public int Id { get; set; }
        public Proizvod Proizvod { get; set; }
        public int ProizvodId { get; set; }
        public Narudzba Narudzba { get; set; }
        public int NarudzbaId { get; set; }
    }
}
