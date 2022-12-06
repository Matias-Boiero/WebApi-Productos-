using System.ComponentModel.DataAnnotations;

namespace WebApiCrud.Models
{
    public class Producto
    {
        public Producto()
        {
            FechaAlta = DateTime.Now;
            Activo = true;
        }
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaAlta { get; set; }

        [Required]
        public decimal Precio { get; set; }
        public bool Activo { get; set; }

    }
}
