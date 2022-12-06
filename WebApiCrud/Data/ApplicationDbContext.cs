using Microsoft.EntityFrameworkCore;
using WebApiCrud.Models;

namespace WebApiCrud.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {

        }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //creo las dos keys (llave compuesta) de la tabla ArticuloEtiqueta 

            modelBuilder.Entity<Producto>().Property(p => p.Precio).HasPrecision(9, 4); // or whatever your schema specifies


        }

    }
}
