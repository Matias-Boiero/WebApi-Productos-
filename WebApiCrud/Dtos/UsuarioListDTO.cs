using System.ComponentModel.DataAnnotations;

namespace WebApiCrud.Dtos
{
    public class UsuarioListDTO
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaAlta { get; set; }
        public bool Activo { get; set; }
    }
}
