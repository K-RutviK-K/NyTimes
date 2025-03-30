using Microsoft.EntityFrameworkCore;
using NyTimes.Domain.Models;

namespace NyTimes.Infrastructure.DatabaseContext;

public partial class NyTimesDbContext : DbContext
{
    public NyTimesDbContext()
    {
    }

    public NyTimesDbContext(DbContextOptions<NyTimesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Articles> Articles { get; set; }

    public virtual DbSet<Facets> Facets { get; set; }

    public virtual DbSet<Multimedia> Multimedia { get; set; }

//   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=NITRO-5-FROM-RK\\SQLEXPRESS;Database=RutvikNyTimes;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Articles>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Articles__3214EC074D8806E5");

            entity.Property(e => e.Byline).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ItemType).HasMaxLength(100);
            entity.Property(e => e.Kicker).HasMaxLength(255);
            entity.Property(e => e.MaterialTypeFacet).HasMaxLength(255);
            entity.Property(e => e.PublishedDate).HasColumnType("datetime");
            entity.Property(e => e.Section).HasMaxLength(255);
            entity.Property(e => e.Subsection).HasMaxLength(255);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Facets>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Facets__3214EC0705311C56");

            entity.Property(e => e.FacetType).HasMaxLength(50);

            entity.HasOne(d => d.Article).WithMany(p => p.Facets)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Facets__ArticleI__3B75D760");
        });

        modelBuilder.Entity<Multimedia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Multimed__3214EC07DFF21755");

            entity.Property(e => e.Copyright).HasMaxLength(255);
            entity.Property(e => e.Format).HasMaxLength(255);
            entity.Property(e => e.Subtype).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.Article).WithMany(p => p.Multimedia)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Multimedi__Artic__38996AB5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
