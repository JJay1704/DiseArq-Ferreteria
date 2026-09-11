using Ferreteria.Models;
using Microsoft.EntityFrameworkCore;

namespace Ferreteria.Data
{
    public class AppData : DbContext

    {

        public AppData  (DbContextOptions<AppData> options) : base(options)
        {


        }
        public DbSet<TipoCliente> TipoClientes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<MetodoPago> MetodosPago { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<DetVenta> DetallesVenta { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

       
                base.OnModelCreating(modelBuilder);

                #region Mapeo de Tablas y Llaves Primarias
                modelBuilder.Entity<TipoCliente>().ToTable("TipoCliente").HasKey(tc => tc.IdTpeCliente);
                modelBuilder.Entity<Cliente>().ToTable("Cliente").HasKey(c => c.IdCliente);
                modelBuilder.Entity<Empleado>().ToTable("Empleado").HasKey(e => e.IdEmpleado);
                modelBuilder.Entity<MetodoPago>().ToTable("MetodoPago").HasKey(mp => mp.IdMetPago);
                modelBuilder.Entity<Categoria>().ToTable("Categoria").HasKey(ca => ca.IdCat);
                modelBuilder.Entity<Marca>().ToTable("Marca").HasKey(m => m.IdMarca);
                modelBuilder.Entity<Producto>().ToTable("Producto").HasKey(p => p.IdProducto);
                modelBuilder.Entity<Venta>().ToTable("Venta").HasKey(v => v.IdVenta);
                modelBuilder.Entity<DetVenta>().ToTable("DetVent").HasKey(dv => dv.IdDetVent);
                #endregion

                #region Relaciones de Cliente y TipoCliente
                modelBuilder.Entity<Cliente>()
                    .HasOne(c => c.TipoClienteNavegacion)
                    .WithMany(tc => tc.Clientes)
                    .HasForeignKey(c => c.TpeCliente)
                    .OnDelete(DeleteBehavior.Restrict); // Evita borrado en cascada

                modelBuilder.Entity<Cliente>()
                    .Property(c => c.Saldo)
                    .HasPrecision(10, 2);

                modelBuilder.Entity<Cliente>()
                    .Property(c => c.TotalConsum)
                    .HasPrecision(10, 2);
                #endregion

                #region Relaciones de Producto (Categoría y Marca)
                modelBuilder.Entity<Producto>()
                    .HasOne(p => p.Cat)
                    .WithMany(ca => ca.Productos)
                    .HasForeignKey(p => p.IdCat)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Producto>()
                    .HasOne(p => p.Marc)
                    .WithMany(m => m.Productos)
                    .HasForeignKey(p => p.IdMarca)
                    .OnDelete(DeleteBehavior.Restrict);

                // Mapeo de precisión decimal para inventarios ferreteros
                modelBuilder.Entity<Producto>().Property(p => p.PrecioCosto).HasPrecision(10, 2);
                modelBuilder.Entity<Producto>().Property(p => p.PrecioVenta).HasPrecision(10, 2);
                modelBuilder.Entity<Producto>().Property(p => p.StockActual).HasPrecision(10, 2);
                modelBuilder.Entity<Producto>().Property(p => p.StockMinimo).HasPrecision(10, 2);
                #endregion

                #region Relaciones de Venta (Cliente, Empleado, Método de Pago)
                modelBuilder.Entity<Venta>()
                    .HasOne(v => v.Cliente)
                    .WithMany(c => c.Ventas)
                    .HasForeignKey(v => v.IdCliente)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Venta>()
                    .HasOne(v => v.Empleado)
                    .WithMany(e => e.Ventas)
                    .HasForeignKey(v => v.IdEmpleado)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Venta>()
                    .HasOne(v => v.MetodoPago)
                    .WithMany(mp => mp.Ventas)
                    .HasForeignKey(v => v.IdMetodoPago)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Venta>()
                    .Property(v => v.Total)
                    .HasPrecision(10, 2);

                // Forzar a SQL Server que use GETDATE() si el objeto va sin fecha
                modelBuilder.Entity<Venta>()
                    .Property(v => v.Fecha)
                    .HasDefaultValueSql("GETDATE()");
                #endregion

                #region Relaciones de DetVent (Venta y Producto)
                modelBuilder.Entity<DetVenta>()
                    .HasOne(dv => dv.Venta)
                    .WithMany(v => v.Detalles)
                    .HasForeignKey(dv => dv.IdVenta)
                    .OnDelete(DeleteBehavior.Cascade); // Si se elimina una venta de raíz, se eliminan sus renglones de detalle

                modelBuilder.Entity<DetVenta>()
                    .HasOne(dv => dv.Producto)
                    .WithMany() // No creamos una lista de detalles dentro de Producto para no saturar memoria
                    .HasForeignKey(dv => dv.IdProducto)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<DetVenta>().Property(dv => dv.Cant).HasPrecision(10, 2);
                modelBuilder.Entity<DetVenta>().Property(dv => dv.PrecUnit).HasPrecision(10, 2);
                modelBuilder.Entity<DetVenta>().Property(dv => dv.Descuento).HasPrecision(10, 2);
                #endregion


            


        }


    }
}
