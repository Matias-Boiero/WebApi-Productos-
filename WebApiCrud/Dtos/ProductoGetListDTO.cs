using System.ComponentModel.DataAnnotations;

namespace WebApiCrud.Dtos
{
    public class ProductoGetListDTO
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

    }
}
