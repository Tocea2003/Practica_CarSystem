using Microsoft.EntityFrameworkCore;
using MasiniAPI.Models;

namespace MasiniAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Masina> Masini { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurări specifice pentru Masina
            modelBuilder.Entity<Masina>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Marca).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Model).IsRequired().HasMaxLength(100);
                entity.Property(e => e.An).IsRequired();
                entity.Property(e => e.Motor).IsRequired().HasMaxLength(20);
            });
        }
    }
}
