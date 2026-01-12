using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MMAppApi.Models;

public partial class MmappContext : DbContext
{
    public MmappContext()
    {
    }

    public MmappContext(DbContextOptions<MmappContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Instrument> Instruments { get; set; }

    public virtual DbSet<Label> Labels { get; set; }

    public virtual DbSet<Occupation> Occupations { get; set; }

    public virtual DbSet<Website> Websites { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=STEPALT;Database=MMApp; Integrated Security=True; Encrypt=True; TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("PK_Artist");

            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasMany(d => d.Genres).WithMany(p => p.Artists)
                .UsingEntity<Dictionary<string, object>>(
                    "ArtistGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("GenreId")
                        .HasConstraintName("FK_ArtistGenres_Genres"),
                    l => l.HasOne<Artist>().WithMany()
                        .HasForeignKey("ArtistId")
                        .HasConstraintName("FK_ArtistGenres_Artist"),
                    j =>
                    {
                        j.HasKey("ArtistId", "GenreId");
                        j.ToTable("ArtistGenres");
                    });

            entity.HasMany(d => d.Instruments).WithMany(p => p.Artists)
                .UsingEntity<Dictionary<string, object>>(
                    "ArtistInstrument",
                    r => r.HasOne<Instrument>().WithMany()
                        .HasForeignKey("InstrumentId")
                        .HasConstraintName("FK_ArtistInstruments_Instruments"),
                    l => l.HasOne<Artist>().WithMany()
                        .HasForeignKey("ArtistId")
                        .HasConstraintName("FK_ArtistInstruments_Artist"),
                    j =>
                    {
                        j.HasKey("ArtistId", "InstrumentId");
                        j.ToTable("ArtistInstruments");
                    });
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK_Genre");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Instrument>(entity =>
        {
            entity.HasKey(e => e.InstrumentId).HasName("PK_Instrument");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Label>(entity =>
        {
            entity.HasKey(e => e.LabelId).HasName("PK_Label");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Occupation>(entity =>
        {
            entity.HasKey(e => e.OccupationId).HasName("PK_Occupation");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Website>(entity =>
        {
            entity.HasKey(e => e.WebsiteId).HasName("PK_Website");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
