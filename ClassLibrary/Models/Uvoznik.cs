
using ServiceStack.DataAnnotations;

namespace ClassLibrary.Models
{
    public class Uvoznik
    {
        [PrimaryKey]
        public int UvoznikID { get; set; }
        public string NazivUvoznika { get; set; }
        public string AdresaUvoznika { get; set; }
        public string BrojTelefona { get; set; }
    }
}
