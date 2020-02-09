using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Naslov { get; set; }
        public string Sadrzaj { get; set; }
        public AppUser Autor { get; set; }
        public int AutorId { get; set; }
        public string ImageLocation { get; set; }
    }
}
