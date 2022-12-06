using System.ComponentModel.DataAnnotations;

namespace WebApiCrud.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime FechaAlta { get; set; }
        public bool Activo { get; set; }

        //propiedades para la encriptación del password de autenticación
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

    }
}
