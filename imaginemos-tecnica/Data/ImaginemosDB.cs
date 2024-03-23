using Microsoft.EntityFrameworkCore;
using imaginemos_tecnica.Models;

namespace imaginemos_tecnica.Data
{
    public class ImaginamosDb : DbContext
    {
        public ImaginamosDb(DbContextOptions<ImaginamosDb> options) : base(options)
        {
        }

        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Venta> Ventas => Set<Venta>();
        public DbSet<DetalleVenta> DetallesVentas => Set<DetalleVenta>();
        public DbSet<Usuario> Usuarios => Set<Usuario>();
    }
}
