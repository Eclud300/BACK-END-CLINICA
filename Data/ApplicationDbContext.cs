using Microsoft.EntityFrameworkCore;
using Clinica.Models;

namespace Clinica.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Aquí mapeamos las tablas principales
        public DbSet<Cita> Citas { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Le decimos a EF Core que no pluralice los nombres si tus tablas en SQL están en español
            modelBuilder.Entity<Cita>().ToTable("citas");
        }
    }
}