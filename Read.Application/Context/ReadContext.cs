using Microsoft.EntityFrameworkCore;
using Read.Application.Models;

namespace Read.Application.Context;

public class ReadContext : DbContext
{
    public ReadContext(DbContextOptions<ReadContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Author)
                .IsRequired()
                .HasMaxLength(200);
            entity.Property(u => u.Title).HasMaxLength(200);
        });

        modelBuilder.Entity<User>(entity => { entity.HasKey(u => u.Id); });
    }
}