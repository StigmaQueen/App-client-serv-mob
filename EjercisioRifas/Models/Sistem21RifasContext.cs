using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EjercisioRifas.Models;

public partial class Sistem21RifasContext : DbContext
{
    public Sistem21RifasContext()
    {
    }

    public Sistem21RifasContext(DbContextOptions<Sistem21RifasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Boletos> Boletos { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Boletos>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("boletos");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Eliminado).HasColumnType("bit(1)");
            entity.Property(e => e.FechaModificacion).HasColumnType("datetime");
            entity.Property(e => e.NombrePersona).HasMaxLength(255);
            entity.Property(e => e.NumeroBoleto).HasColumnType("int(10) unsigned");
            entity.Property(e => e.NumeroTelefono).HasMaxLength(10);
            entity.Property(e => e.Pagado).HasColumnType("bit(1)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
