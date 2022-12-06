using System.ComponentModel.DataAnnotations;

namespace WebApiCrud.Dtos
{
    public class UsuarioRegisterDTO
    {
        public UsuarioRegisterDTO()
        {
            FechaAlta = DateTime.Now;
            Activo = true;
        }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Contraseña { get; set; }


        [DataType(DataType.Date)]
        public DateTime FechaAlta { get; set; }
        public bool Activo { get; set; }


    }
}
