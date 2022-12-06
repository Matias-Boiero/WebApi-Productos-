using WebApiCrud.Models;

namespace WebApiCrud.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(Usuario usuario);
    }
}
