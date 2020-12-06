using ServiceStack.DataAnnotations;

namespace OnlineShop.Model.Models
{
    public class Kategorija
    {
        [PrimaryKey]
        public int KategorijaID { get; set; }
        public string NazivKategorije { get; set; }
    }
}