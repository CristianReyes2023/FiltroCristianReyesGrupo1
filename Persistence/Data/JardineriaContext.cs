using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Data;

public partial class JardineriaContext : DbContext
{
    public JardineriaContext()
    {
    }

    public JardineriaContext(DbContextOptions<JardineriaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetallePedido> DetallePedidos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<GamaProducto> GamaProductos { get; set; }

    public virtual DbSet<Oficina> Oficinas { get; set; }

    public virtual DbSet<Pago> Pagos { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;password=123456;database=jardineriadbcristian", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.34-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cliente");

            entity.HasIndex(e => e.IdEmpleadoRepresentanteVentasFk, "idEmpleadoRepresentanteVentasFk");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ApellidoContacto)
                .HasMaxLength(30)
                .HasColumnName("apellido_contacto");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(50)
                .HasColumnName("ciudad");
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(10)
                .HasColumnName("codigo_postal");
            entity.Property(e => e.Fax)
                .HasMaxLength(15)
                .HasColumnName("fax");
            entity.Property(e => e.IdEmpleadoRepresentanteVentasFk).HasColumnName("idEmpleadoRepresentanteVentasFk");
            entity.Property(e => e.LimiteCredito)
                .HasPrecision(15, 2)
                .HasColumnName("limite_credito");
            entity.Property(e => e.LineaDireccion1)
                .HasMaxLength(50)
                .HasColumnName("linea_direccion1");
            entity.Property(e => e.LineaDireccion2)
                .HasMaxLength(50)
                .HasColumnName("linea_direccion2");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(50)
                .HasColumnName("nombre_cliente");
            entity.Property(e => e.NombreContacto)
                .HasMaxLength(30)
                .HasColumnName("nombre_contacto");
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .HasColumnName("pais");
            entity.Property(e => e.Region)
                .HasMaxLength(50)
                .HasColumnName("region");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdEmpleadoRepresentanteVentasFkNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdEmpleadoRepresentanteVentasFk)
                .HasConstraintName("idEmpleadoRepresentanteVentasFk");
        });

        modelBuilder.Entity<DetallePedido>(entity =>
        {
            entity.HasKey(e => new { e.IdPedidoFk, e.IdProductoFk })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("detalle_pedido");

            entity.HasIndex(e => e.IdProductoFk, "idProductoFk");

            entity.Property(e => e.IdPedidoFk).HasColumnName("idPedidoFk");
            entity.Property(e => e.IdProductoFk)
                .HasMaxLength(15)
                .HasColumnName("idProductoFk");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.NumeroLinea).HasColumnName("numero_linea");
            entity.Property(e => e.PrecioUnidad)
                .HasPrecision(15, 2)
                .HasColumnName("precio_unidad");

            entity.HasOne(d => d.IdPedidoFkNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdPedidoFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idPedidoFk");

            entity.HasOne(d => d.IdProductoFkNavigation).WithMany(p => p.DetallePedidos)
                .HasForeignKey(d => d.IdProductoFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idProductoFk");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("empleado");

            entity.HasIndex(e => e.IdEmpleadoJefeFk, "idEmpleadoJefeFk");

            entity.HasIndex(e => e.IdOficinaFk, "idOficinaFk");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Apellido1)
                .HasMaxLength(50)
                .HasColumnName("apellido1");
            entity.Property(e => e.Apellido2)
                .HasMaxLength(50)
                .HasColumnName("apellido2");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Extension)
                .HasMaxLength(10)
                .HasColumnName("extension");
            entity.Property(e => e.IdEmpleadoJefeFk).HasColumnName("idEmpleadoJefeFk");
            entity.Property(e => e.IdOficinaFk)
                .HasMaxLength(10)
                .HasColumnName("idOficinaFk");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Puesto)
                .HasMaxLength(50)
                .HasColumnName("puesto");

            entity.HasOne(d => d.IdEmpleadoJefeFkNavigation).WithMany(p => p.InverseIdEmpleadoJefeFkNavigation)
                .HasForeignKey(d => d.IdEmpleadoJefeFk)
                .HasConstraintName("idEmpleadoJefeFk");

            entity.HasOne(d => d.IdOficinaFkNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdOficinaFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idOficinaFk");
        });

        modelBuilder.Entity<GamaProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("gama_producto");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.DescripcionHtml)
                .HasColumnType("text")
                .HasColumnName("descripcion_html");
            entity.Property(e => e.DescripcionTexto)
                .HasColumnType("text")
                .HasColumnName("descripcion_texto");
            entity.Property(e => e.Imagen)
                .HasMaxLength(256)
                .HasColumnName("imagen");
        });

        modelBuilder.Entity<Oficina>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("oficina");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .HasColumnName("id");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(30)
                .HasColumnName("ciudad");
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(10)
                .HasColumnName("codigo_postal");
            entity.Property(e => e.LineaDireccion1)
                .HasMaxLength(50)
                .HasColumnName("linea_direccion1");
            entity.Property(e => e.LineaDireccion2)
                .HasMaxLength(50)
                .HasColumnName("linea_direccion2");
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .HasColumnName("pais");
            entity.Property(e => e.Region)
                .HasMaxLength(50)
                .HasColumnName("region");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pago");

            entity.HasIndex(e => e.IdClienteFk, "idClienteFkk");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");
            entity.Property(e => e.FormaPago)
                .HasMaxLength(40)
                .HasColumnName("forma_pago");
            entity.Property(e => e.IdClienteFk).HasColumnName("idClienteFk");
            entity.Property(e => e.Total)
                .HasPrecision(15, 2)
                .HasColumnName("total");

            entity.HasOne(d => d.IdClienteFkNavigation).WithMany(p => p.Pagos)
                .HasForeignKey(d => d.IdClienteFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idClienteFkk");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pedido");

            entity.HasIndex(e => e.IdClienteFk, "idClienteFk");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Comentarios)
                .HasColumnType("text")
                .HasColumnName("comentarios");
            entity.Property(e => e.Estado)
                .HasMaxLength(15)
                .HasColumnName("estado");
            entity.Property(e => e.FechaEntrega).HasColumnName("fecha_entrega");
            entity.Property(e => e.FechaEsperada).HasColumnName("fecha_esperada");
            entity.Property(e => e.FechaPedido).HasColumnName("fecha_pedido");
            entity.Property(e => e.IdClienteFk).HasColumnName("idClienteFk");

            entity.HasOne(d => d.IdClienteFkNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.IdClienteFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idClienteFk");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("producto");

            entity.HasIndex(e => e.IdGamaFk, "idGamaFk");

            entity.Property(e => e.Id)
                .HasMaxLength(15)
                .HasColumnName("id");
            entity.Property(e => e.CantidadEnStock).HasColumnName("cantidad_en_stock");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Dimensiones)
                .HasMaxLength(25)
                .HasColumnName("dimensiones");
            entity.Property(e => e.IdGamaFk)
                .HasMaxLength(50)
                .HasColumnName("idGamaFk");
            entity.Property(e => e.Nombre)
                .HasMaxLength(70)
                .HasColumnName("nombre");
            entity.Property(e => e.PrecioProveedor)
                .HasPrecision(15, 2)
                .HasColumnName("precio_proveedor");
            entity.Property(e => e.PrecioVenta)
                .HasPrecision(15, 2)
                .HasColumnName("precio_venta");
            entity.Property(e => e.Proveedor)
                .HasMaxLength(50)
                .HasColumnName("proveedor");

            entity.HasOne(d => d.IdGamaFkNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdGamaFk)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("idGamaFk");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
