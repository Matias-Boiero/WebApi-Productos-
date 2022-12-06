using AutoMapper;
using WebApiCrud.Dtos;
using WebApiCrud.Models;

namespace WebApiCrud.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //el primer parametro dentro del <> es el origen y el segundo es el destino del mapeado
            //Post o create
            CreateMap<ProductoCreateDTO, Producto>();
            //Update o put
            CreateMap<ProductoUpdateDTO, Producto>();
            //Get o list
            CreateMap<Producto, ProductoGetListDTO>();

            //Usuarios
            CreateMap<UsuarioRegisterDTO, Usuario>();
            CreateMap<UsuarioLoginDTO, Usuario>();
            CreateMap<Usuario, UsuarioListDTO>();
            CreateMap<UsuarioUpdateDTO, Usuario>();

        }
    }
}
