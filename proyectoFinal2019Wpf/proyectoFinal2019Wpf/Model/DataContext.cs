using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace proyectoFinal2019Wpf.Model
{
    public class DataContext: DbContext
    {

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<TipoEmpaque> TipoEmpaques { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<DetalleCompra> DetalleCompras { get; set; }
        public DbSet<DetalleFactura> DetalleFacturas { get; set; }
        public DbSet<EmailCliente> EmailClientes { get; set; }
        public DbSet<EmailProveedor> EmailProveedores { get; set; }
        public DbSet<Factura> Facturas { get; set; }
        public DbSet<TelefonoCliente> TelefonoClientes { get; set; }
        public DbSet<TelefonoProveedor> TelefonoProveedores { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //Categoria 
            modelBuilder.Entity<Categoria>()
                .ToTable("Categoria")
                .HasKey(c => new { c.CodigoCategoria });
            modelBuilder.Entity<Categoria>()
                .ToTable("Categoria")
                .Property(c => c.Descripcion)
                .IsRequired()
                .HasMaxLength(128);
            //TipoEmpaque
            modelBuilder.Entity<TipoEmpaque>()
                .ToTable("TipoEmpaque")
                .HasKey(t => new { t.CodigoEmpaque });
            modelBuilder.Entity<TipoEmpaque>()
                .ToTable("TipoEmpaque")
                .Property(t => t.Descripcion)
                .IsRequired()
                .HasMaxLength(128);
            //Inventario
            modelBuilder.Entity<Inventario>()
                .ToTable("Inventario")
                .HasKey(i => new { i.CodigoInventario });
            modelBuilder.Entity<Inventario>()
                .ToTable("Inventario")
                .Property(i => i.Fecha)
                .IsRequired();
            modelBuilder.Entity<Inventario>()
                .ToTable("Inventario")
                .Property(i => i.TipoRegristro)
                .IsRequired()
                .HasMaxLength(1);
            modelBuilder.Entity<Inventario>()
                .ToTable("Inventario")
                .Property(i => i.Precio)
                .IsRequired();
            modelBuilder.Entity<Inventario>()
                .ToTable("Inventario")
                .Property(i => i.Entradas)
                .IsRequired();
            modelBuilder.Entity<Inventario>()
                .ToTable("Inventario")
                .Property(i => i.Salidas)
                .IsRequired();
            //Producto
            modelBuilder.Entity<Producto>()
                .ToTable("Producto")
                .HasKey(p => new { p.CodigoProducto });
            modelBuilder.Entity<Producto>()
                .ToTable("Producto")
                .Property(p => p.Descripcion)
                .IsRequired()
                .HasMaxLength(128);
            modelBuilder.Entity<Producto>()
                .ToTable("Producto")
                .Property(p => p.PrecioUnitario)
                .IsRequired();
            modelBuilder.Entity<Producto>()
                .ToTable("Producto")
                .Property(p => p.PrecioPorDocena)
                .IsRequired();
            modelBuilder.Entity<Producto>()
                .ToTable("Producto")
                .Property(p => p.PrecioPorMayor)
                .IsRequired();
            modelBuilder.Entity<Producto>()
                .ToTable("Producto")
                .Property(p => p.Existencia)
                .IsRequired();
            modelBuilder.Entity<Producto>()
                .ToTable("Producto")
                .Property(p => p.Imagen)
                .IsOptional()
                .HasMaxLength(128);
            //DetalleCompra
            modelBuilder.Entity<DetalleCompra>()
                .ToTable("DetalleCompra")
                .HasKey(x => new { x.IdDetalle });
            modelBuilder.Entity<DetalleCompra>()
                .ToTable("DetalleCompra")
                .Property(x => x.Cantidad)
                .IsRequired();
            modelBuilder.Entity<DetalleCompra>()
                .ToTable("DetalleCompra")
                .Property(x => x.Precio)
                .IsRequired();
            //Compra
            modelBuilder.Entity<Compra>()
                .ToTable("Compra")
                .HasKey(l => new { l.IdCompra });
            modelBuilder.Entity<Compra>()
                .ToTable("Compra")
                .Property(l => l.NumeroDocumento)
                .IsRequired();
            modelBuilder.Entity<Compra>()
                .ToTable("Compra")
                .Property(l => l.Fecha)
                .IsRequired();
            modelBuilder.Entity<Compra>()
                .ToTable("Compra")
                .Property(l => l.Total)
                .IsRequired();
            //Proveedor
            modelBuilder.Entity<Proveedor>()
                .ToTable("Proveedor")
                .HasKey(s => new { s.CodigoProveedor });
            modelBuilder.Entity<Proveedor>()
                .ToTable("Proveedor")
                .Property(s => s.Nit)
                .IsRequired()
                .HasMaxLength(64);
            modelBuilder.Entity<Proveedor>()
                .ToTable("Proveedor")
                .Property(s => s.RazonSocial)
                .IsRequired()
                .HasMaxLength(128);
            modelBuilder.Entity<Proveedor>()
                .ToTable("Proveedor")
                .Property(s => s.Direccion)
                .IsRequired()
                .HasMaxLength(128);
            modelBuilder.Entity<Proveedor>()
                .ToTable("Proveedor")
                .Property(s => s.PaginaWeb)
                .IsOptional()
                .HasMaxLength(64);
            modelBuilder.Entity<Proveedor>()
                .ToTable("Proveedor")
                .Property(s => s.ContactoPrincipal)
                .IsRequired()
                .HasMaxLength(64);
            //TelefonoProveedor
            modelBuilder.Entity<TelefonoProveedor>()
                .ToTable("TelefonoPorveedor")
                .HasKey(t => new { t.CodigoTelefono });
            modelBuilder.Entity<TelefonoProveedor>()
                .ToTable("TelefonoProveedor")
                .Property(t => t.Numero)
                .IsRequired()
                .HasMaxLength(32);
            modelBuilder.Entity<TelefonoProveedor>()
                .ToTable("TelefonoProveedor")
                .Property(t => t.Descripcion)
                .IsRequired()
                .HasMaxLength(64);
            //EmailProveedor
            modelBuilder.Entity<EmailProveedor>()
            .ToTable("EmailProveedor")
            .HasKey(e => new {e.CodigoEmail });
            modelBuilder.Entity<EmailProveedor>()
                .ToTable("EmailProveedor")
                .Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(64);
            //DetalleFactura
            modelBuilder.Entity<DetalleFactura>()
                .ToTable("DetalleFactura")
                .HasKey(v => new { v.CodigoDetalle });
            modelBuilder.Entity<DetalleFactura>()
                .ToTable("DetalleFactura")
                .Property(v => v.Cantidad)
                .IsRequired();
            modelBuilder.Entity<DetalleFactura>()
                .ToTable("DetalleFactura")
                .Property(v => v.Precio)
                .IsRequired();
            modelBuilder.Entity<DetalleFactura>()
                .ToTable("DetalleFactura")
                .Property(v => v.Descuento)
                .IsRequired();
            //Factura
            modelBuilder.Entity<Factura>()
                .ToTable("Factura")
                .HasKey(f => new { f.NumeroFactura });
            modelBuilder.Entity<Factura>()
                .ToTable("Factura")
                .Property(f => f.Nit)
                .IsRequired()
                .HasMaxLength(64);
            modelBuilder.Entity<Factura>()
               .ToTable("Factura")
               .Property(f => f.Fecha)
               .IsRequired();
            modelBuilder.Entity<Factura>()
               .ToTable("Factura")
               .Property(f => f.Total)
               .IsRequired();
            //Cliente
            modelBuilder.Entity<Cliente>()
                .ToTable("Cliente")
                .HasKey(x => new { x.Nit });
            modelBuilder.Entity<Cliente>()
                .ToTable("Cliente")
                .Property(x => x.DPI)
                .IsRequired()
                .HasMaxLength(64);
            modelBuilder.Entity<Cliente>()
               .ToTable("Cliente")
               .Property(x => x.Nombre)
               .IsRequired()
               .HasMaxLength(128);
            modelBuilder.Entity<Cliente>()
               .ToTable("Cliente")
               .Property(x => x.Direccion)
               .IsRequired()
               .HasMaxLength(128);
            //EmailCliente
            modelBuilder.Entity<EmailCliente>()
                .ToTable("EmailCliente")
                .HasKey(m => new { m.CodigoEmail });
            modelBuilder.Entity<EmailCliente>()
                .ToTable("EmailCliente")
                .Property(z => z.Email)
                .IsRequired()
                .HasMaxLength(128);
            modelBuilder.Entity<EmailCliente>()
                .ToTable("EmailCliente")
                .Property(z => z.Nit)
                .IsRequired()
                .HasMaxLength(64);
            //TelefonoCliente
            modelBuilder.Entity<TelefonoCliente>()
                .ToTable("TelefonoCliente")
                .HasKey(n => new { n.CodigoTelefono });
            modelBuilder.Entity<TelefonoCliente>()
                .ToTable("TelefonoCliente")
                .Property(n => n.Numero)
                .IsRequired()
                .HasMaxLength(32);
            modelBuilder.Entity<TelefonoCliente>()
                .ToTable("TelefonoCliente")
                .Property(n => n.Descripcion)
                .IsRequired()
                .HasMaxLength(128);
            modelBuilder.Entity<TelefonoCliente>()
                .ToTable("TelefonoCliente")
                .Property(n => n.Nit)
                .IsRequired()
                .HasMaxLength(64);





        }



    }
}
