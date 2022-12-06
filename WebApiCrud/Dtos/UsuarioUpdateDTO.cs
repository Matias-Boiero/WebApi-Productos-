using System.ComponentModel.DataAnnotations;

namespace WebApiCrud.Dtos
{
    public class UsuarioUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
