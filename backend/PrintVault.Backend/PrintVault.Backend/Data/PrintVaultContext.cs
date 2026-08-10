using Microsoft.EntityFrameworkCore;
using PrintVault.Backend.Models;

namespace PrintVault.Backend.Data;

public class PrintVaultContext : DbContext
{
    public PrintVaultContext(DbContextOptions<PrintVaultContext> options) : base(options)
    {
    }

    public DbSet<PrintModel> PrintModels { get; set; }
    public DbSet<Plate> Plates { get; set; }
    public DbSet<ModelImage> ModelImages { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<ModelTag> ModelTags { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ModelTag>()
            .HasKey(mt => new { mt.PrintModelId, mt.TagId });

        modelBuilder.Entity<ModelTag>()
            .HasOne(mt => mt.PrintModel)
            .WithMany(m => m.ModelTags)
            .HasForeignKey(mt => mt.PrintModelId);

        modelBuilder.Entity<ModelTag>()
            .HasOne(mt => mt.Tag)
            .WithMany(t => t.ModelTags)
            .HasForeignKey(mt => mt.TagId);
    }
}