using DemoTienda.Domain.Entities;
using DemoTienda.Domain.Views;
using DemoTienda.Infraestructure.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoTienda.Infraestructure.Context;

public partial class DemoTiendaContext : IdentityDbContext<AppUser>
{
    private readonly bool IsUnitTestContext;
    public DemoTiendaContext()
    {
        IsUnitTestContext = false;
    }

    public DemoTiendaContext(DbContextOptions<DemoTiendaContext> options, bool unitTestContext = false)
        : base(options)
    {
        IsUnitTestContext = unitTestContext;
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<ProductosCategoria> ViewProductosCategoria { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Categoria>(entity =>
        {
            if (IsUnitTestContext)
                entity.ToTable("Categoria");
            else
                entity.ToTable("Categoria", t => t.ExcludeFromMigrations());

            entity.HasIndex(e => e.Nombre, "UX_Categoria_Nombre").IsUnique();

            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.EsActiva).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            if (IsUnitTestContext)
                entity.ToTable("Producto");
            else
                entity.ToTable("Producto", t => t.ExcludeFromMigrations());

            entity.HasIndex(e => e.EsActivo, "IX_Producto_EsActivo");

            entity.HasIndex(e => e.IdCategoria, "IX_Producto_IdCategoria");

            entity.HasIndex(e => new { e.IdCategoria, e.Nombre }, "UX_Producto_Nombre_Categoria").IsUnique();

            entity.Property(e => e.Descripcion).HasMaxLength(1000);
            entity.Property(e => e.EsActivo).HasDefaultValue(true);
            entity.Property(e => e.FechaCreacion)
                .HasPrecision(0)
                .HasDefaultValueSql("(sysutcdatetime())");
            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Producto_Categoria");
        });

        modelBuilder.Entity<ProductosCategoria>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vProductosConCategoria");

            entity.Property(e => e.FechaCreacion).HasPrecision(0);
            entity.Property(e => e.NombreCategoria).HasMaxLength(100);
            entity.Property(e => e.NombreProducto).HasMaxLength(150);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
