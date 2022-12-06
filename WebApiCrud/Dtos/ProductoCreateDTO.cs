using System.ComponentModel.DataAnnotations;

namespace WebApiCrud.Dtos
{
    public class ProductoCreateDTO
    {
        [Required]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        [Required]
        public decimal Precio { get; set; }
    }
}
