using Microsoft.EntityFrameworkCore;
using WebApiCrud.Data.Interfaces;
using WebApiCrud.Models;

namespace WebApiCrud.Data
{
    public class ApiRepository : IApiRepository
    {
        private readonly ApplicationDbContext _context;

        public ApiRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(p => p.Id == id);
            return producto;
        }

        public async Task<IEnumerable<Producto>> GetProductosAsync()
        {
            var listaProductos = await _context.Productos.ToListAsync();
            return listaProductos;
        }

        public async Task<Producto> GetUProductoByNombreAsync(string producto)
        {
            var nombreProducto = await _context.Productos.FirstOrDefaultAsync(p => p.Nombre == producto);
            return nombreProducto;
        }

        public async Task<Usuario> GetUsuarioByIdAsync(int id)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
            return usuario;
        }

        public async Task<Usuario> GetUsuarioByNombreAsync(string nombre)
        {
            var nombreUsuario = await _context.Usuarios.FirstOrDefaultAsync(p => p.Nombre == nombre);
            return nombreUsuario;
        }

        public async Task<IEnumerable<Usuario>> GetUsuariosAsync()
        {
            var listaUsuarios = await _context.Usuarios.ToListAsync();
            return listaUsuarios;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
