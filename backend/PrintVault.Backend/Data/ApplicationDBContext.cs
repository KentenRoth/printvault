using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PrintVault.Backend.Models;

namespace PrintVault.Backend.Data
{
    public class ApplicationDBContext : DbContext
    {
public ApplicationDBContext(DbContextOptions dbContextOptions)
        : base(dbContextOptions)
    {

    }

        public DbSet<PrintFile> PrintFiles { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<PrintFileTag> PrintFileTags { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PrintFileTag>()
                .HasKey(pft => new { pft.PrintFileId, pft.TagId });

            modelBuilder.Entity<PrintFileTag>()
                .HasOne(pft => pft.PrintFile)
                .WithMany(pf => pf.PrintFileTags)
                .HasForeignKey(pft => pft.PrintFileId);

            modelBuilder.Entity<PrintFileTag>()
                .HasOne(pft => pft.Tag)
                .WithMany(t => t.PrintFileTags)
                .HasForeignKey(pft => pft.TagId);
        }
    }
}