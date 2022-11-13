using Microsoft.EntityFrameworkCore;

namespace Ajmera.Models;

public partial class AjmeraContext : DbContext
{
    public AjmeraContext()
    {
    }

    public AjmeraContext(DbContextOptions<AjmeraContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = Environment.GetEnvironmentVariable("PostGreSqlConnection");
            optionsBuilder.UseNpgsql(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("pk_BooksB");

            entity.ToTable("books");

            entity.Property(e => e.BookId)
                .ValueGeneratedNever()
                .HasColumnName("book_id");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(50)
                .HasColumnName("author_name");
            entity.Property(e => e.BookName)
                .HasMaxLength(50)
                .HasColumnName("book_name");
            entity.Property(e => e.CreateTs)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("create_ts");
            entity.Property(e => e.UpdateTs)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("update_ts");
        });
    }
}