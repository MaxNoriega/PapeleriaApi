using ApiPaplria.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPaplria.Context
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        // DbSets para las entidades
        public DbSet<Producto> Productos { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Puntos> Puntos { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Venta> Ventas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de relaciones
            modelBuilder.Entity<DetalleVenta>()
                .HasOne(d => d.Venta)
                .WithMany(v => v.Detalles)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DetalleVenta>()
                .HasOne(d => d.Producto)
                .WithMany()
                .HasForeignKey(d => d.IdPro)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Puntos>()
                .HasOne<Alumno>()
                .WithOne()
                .HasForeignKey<Puntos>(p => p.NumCtrl)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Venta>()
                .HasOne<Alumno>()
                .WithMany()
                .HasForeignKey(v => v.IdCliente)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Pago>()
                .HasOne<Venta>()
                .WithMany()
                .HasForeignKey(p => p.IdVenta)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
