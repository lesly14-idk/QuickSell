using Microsoft.EntityFrameworkCore;
using QuicksellAPI.Models;

namespace QuicksellAPI.Data;

public class QuicksellDbContext : DbContext
{
    public QuicksellDbContext(DbContextOptions<QuicksellDbContext> options) : base(options)
    {
    }

    // DbSets para todas las tablas
    public DbSet<Usuario> USUARIO { get; set; }
    public DbSet<Proveedor> PROVEEDORES { get; set; }
    public DbSet<Categoria> CATEGORIA { get; set; }
    public DbSet<Producto> PRODUCTOS { get; set; }
    public DbSet<SolicitudDeCompra> SOLICITUDES_DE_COMPRA { get; set; }
    public DbSet<DetalleSolicitud> DETALLE_SOLICITUD { get; set; }
    public DbSet<Pago> PAGOS { get; set; }
    public DbSet<Envio> ENVIAR { get; set; }
    public DbSet<HistorialDeOperaciones> HISTORIAL_DE_OPERACIONES { get; set; }
    public DbSet<ConfiguracionesSistema> CONFIGURACIONES_SISTEMA { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración de Usuario
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("USUARIO");
            entity.HasKey(e => e.id_usuario);
            entity.Property(e => e.id_usuario).ValueGeneratedOnAdd();
        });

        // Configuración de Proveedor
        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.ToTable("PROVEEDORES");
            entity.HasKey(e => e.id_proveedor);
            entity.Property(e => e.id_proveedor).ValueGeneratedOnAdd();
            entity.HasOne(e => e.Usuario)
                .WithMany(u => u.Proveedores)
                .HasForeignKey(e => e.id_usuario);
        });

        // Configuración de Categoria
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.ToTable("CATEGORIA");
            entity.HasKey(e => e.id_categoria);
            entity.Property(e => e.id_categoria).ValueGeneratedOnAdd();
        });

        // Configuración de Producto
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.ToTable("PRODUCTOS");
            entity.HasKey(e => e.id_producto);
            entity.Property(e => e.id_producto).ValueGeneratedOnAdd();
            entity.HasOne(e => e.Proveedor)
                .WithMany(p => p.Productos)
                .HasForeignKey(e => e.id_proveedor);
            entity.HasOne(e => e.Categoria)
                .WithMany(c => c.Productos)
                .HasForeignKey(e => e.id_categoria);
        });

        // Configuración de SolicitudDeCompra
        modelBuilder.Entity<SolicitudDeCompra>(entity =>
        {
            entity.ToTable("SOLICITUDES_DE_COMPRA");
            entity.HasKey(e => e.id_solicitud);
            entity.Property(e => e.id_solicitud).ValueGeneratedOnAdd();
            entity.HasOne(e => e.Usuario)
                .WithMany(u => u.SolicitudesDeCompra)
                .HasForeignKey(e => e.id_usuario);
            entity.HasOne(e => e.Proveedor)
                .WithMany(p => p.SolicitudesDeCompra)
                .HasForeignKey(e => e.id_proveedor);
        });

        // Configuración de DetalleSolicitud
        modelBuilder.Entity<DetalleSolicitud>(entity =>
        {
            entity.ToTable("DETALLE_SOLICITUD");
            entity.HasKey(e => e.id_detalle);
            entity.Property(e => e.id_detalle).ValueGeneratedOnAdd();
            entity.HasOne(e => e.SolicitudDeCompra)
                .WithMany(s => s.DetallesSolicitud)
                .HasForeignKey(e => e.id_solicitud);
            entity.HasOne(e => e.Producto)
                .WithMany(p => p.DetallesSolicitud)
                .HasForeignKey(e => e.id_producto);
        });

        // Configuración de Pago
        modelBuilder.Entity<Pago>(entity =>
        {
            entity.ToTable("PAGOS");
            entity.HasKey(e => e.id_pagos);
            entity.Property(e => e.id_pagos).ValueGeneratedOnAdd();
            entity.HasOne(e => e.SolicitudDeCompra)
                .WithMany(s => s.Pagos)
                .HasForeignKey(e => e.id_solicitud);
        });

        // Configuración de Envio
        modelBuilder.Entity<Envio>(entity =>
        {
            entity.ToTable("ENVIAR");
            entity.HasKey(e => e.id_envios);
            entity.Property(e => e.id_envios).ValueGeneratedOnAdd();
            entity.HasOne(e => e.SolicitudDeCompra)
                .WithMany(s => s.Envios)
                .HasForeignKey(e => e.id_solicitud);
        });

        // Configuración de HistorialDeOperaciones
        modelBuilder.Entity<HistorialDeOperaciones>(entity =>
        {
            entity.ToTable("HISTORIAL_DE_OPERACIONES");
            entity.HasKey(e => e.id_operaciones);
            entity.Property(e => e.id_operaciones).ValueGeneratedOnAdd();
            entity.HasOne(e => e.Usuario)
                .WithMany(u => u.HistorialOperaciones)
                .HasForeignKey(e => e.id_usuario);
        });

        // Configuración de ConfiguracionesSistema
        modelBuilder.Entity<ConfiguracionesSistema>(entity =>
        {
            entity.ToTable("CONFIGURACIONES_SISTEMA");
            entity.HasKey(e => e.id_config);
            entity.Property(e => e.id_config).ValueGeneratedOnAdd();
            entity.HasOne(e => e.Usuario)
                .WithMany(u => u.Configuraciones)
                .HasForeignKey(e => e.id_usuario);
        });
    }
}