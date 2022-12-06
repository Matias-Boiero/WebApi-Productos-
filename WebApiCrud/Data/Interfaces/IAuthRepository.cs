using WebApiCrud.Models;

namespace WebApiCrud.Data.Interfaces
{
    public interface IAuthRepository
    {
        Task<Usuario> Registrar(Usuario usuario, string password);
        Task<Usuario> Login(string email, string password);
        Task<bool> ExisteUsuario(string email);
    }
}
