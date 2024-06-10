using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemaVenta.Model.Models;

public partial class DbempanadasContext : DbContext
{
    public DbempanadasContext()
    {
    }

    public DbempanadasContext(DbContextOptions<DbempanadasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Detalleventa> Detalleventa { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Menurol> Menurols { get; set; }

    public virtual DbSet<Numerodocumento> Numerodocumentos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=DBEMPANADAS;Username=postgres;Password=123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Idcategoria).HasName("categoria_pkey");

            entity.ToTable("categoria");

            entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");
            entity.Property(e => e.Esactivo)
                .HasDefaultValueSql("true")
                .HasColumnName("esactivo");
            entity.Property(e => e.Fecharegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecharegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Detalleventa>(entity =>
        {
            entity.HasKey(e => e.Iddetalleventa).HasName("detalleventa_pkey");

            entity.ToTable("detalleventa");

            entity.Property(e => e.Iddetalleventa).HasColumnName("iddetalleventa");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.Idventa).HasColumnName("idventa");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasColumnName("total");

            entity.HasOne(d => d.IdproductoNavigation).WithMany(p => p.Detalleventa)
                .HasForeignKey(d => d.Idproducto)
                .HasConstraintName("detalleventa_idproducto_fkey");

            entity.HasOne(d => d.IdventaNavigation).WithMany(p => p.Detalleventa)
                .HasForeignKey(d => d.Idventa)
                .HasConstraintName("detalleventa_idventa_fkey");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Idmenu).HasName("menu_pkey");

            entity.ToTable("menu");

            entity.Property(e => e.Idmenu).HasColumnName("idmenu");
            entity.Property(e => e.Icono)
                .HasMaxLength(50)
                .HasColumnName("icono");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Url)
                .HasMaxLength(50)
                .HasColumnName("url");
        });

        modelBuilder.Entity<Menurol>(entity =>
        {
            entity.HasKey(e => e.Idmenurol).HasName("menurol_pkey");

            entity.ToTable("menurol");

            entity.Property(e => e.Idmenurol).HasColumnName("idmenurol");
            entity.Property(e => e.Idmenu).HasColumnName("idmenu");
            entity.Property(e => e.Idrol).HasColumnName("idrol");

            entity.HasOne(d => d.IdmenuNavigation).WithMany(p => p.Menurols)
                .HasForeignKey(d => d.Idmenu)
                .HasConstraintName("menurol_idmenu_fkey");

            entity.HasOne(d => d.IdrolNavigation).WithMany(p => p.Menurols)
                .HasForeignKey(d => d.Idrol)
                .HasConstraintName("menurol_idrol_fkey");
        });

        modelBuilder.Entity<Numerodocumento>(entity =>
        {
            entity.HasKey(e => e.Idnumerodocumento).HasName("numerodocumento_pkey");

            entity.ToTable("numerodocumento");

            entity.Property(e => e.Idnumerodocumento).HasColumnName("idnumerodocumento");
            entity.Property(e => e.Fecharegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecharegistro");
            entity.Property(e => e.UltimoNumero).HasColumnName("ultimo_numero");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Idproducto).HasName("producto_pkey");

            entity.ToTable("producto");

            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.Esactivo)
                .HasDefaultValueSql("true")
                .HasColumnName("esactivo");
            entity.Property(e => e.Fecharegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecharegistro");
            entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasPrecision(10, 2)
                .HasColumnName("precio");
            entity.Property(e => e.Stock).HasColumnName("stock");

            entity.HasOne(d => d.IdcategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.Idcategoria)
                .HasConstraintName("producto_idcategoria_fkey");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Idrol).HasName("rol_pkey");

            entity.ToTable("rol");

            entity.Property(e => e.Idrol).HasColumnName("idrol");
            entity.Property(e => e.Fecharegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecharegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("usuario_pkey");

            entity.ToTable("usuario");

            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Clave)
                .HasMaxLength(40)
                .HasColumnName("clave");
            entity.Property(e => e.Correo)
                .HasMaxLength(40)
                .HasColumnName("correo");
            entity.Property(e => e.Esactivo)
                .HasDefaultValueSql("true")
                .HasColumnName("esactivo");
            entity.Property(e => e.Fecharegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecharegistro");
            entity.Property(e => e.Idrol).HasColumnName("idrol");
            entity.Property(e => e.Nombrecompleto)
                .HasMaxLength(100)
                .HasColumnName("nombrecompleto");

            entity.HasOne(d => d.IdrolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Idrol)
                .HasConstraintName("usuario_idrol_fkey");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.Idventa).HasName("venta_pkey");

            entity.ToTable("venta");

            entity.Property(e => e.Idventa).HasColumnName("idventa");
            entity.Property(e => e.Fecharegistro)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecharegistro");
            entity.Property(e => e.Numerodocumento)
                .HasMaxLength(40)
                .HasColumnName("numerodocumento");
            entity.Property(e => e.Tipopago)
                .HasMaxLength(50)
                .HasColumnName("tipopago");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasColumnName("total");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
