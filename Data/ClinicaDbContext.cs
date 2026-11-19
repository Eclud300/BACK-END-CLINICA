using Microsoft.EntityFrameworkCore;
using Clinica.Models;

namespace Clinica.Data
{
    public class ClinicaDbContext : DbContext
    {
        public ClinicaDbContext(DbContextOptions<ClinicaDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<NotaMedica> NotasMedicas { get; set; }
        public DbSet<Insumo> Insumos { get; set; }
        public DbSet<ClinicaInfo> Clinicas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Índices únicos para evitar registros duplicados en PostgreSQL
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Correo)
                .IsUnique();

            modelBuilder.Entity<Paciente>()
                .HasIndex(p => p.CURP)
                .IsUnique();

            modelBuilder.Entity<Cita>()
                .HasIndex(c => c.Folio)
                .IsUnique();

            // Valores por defecto en BD para columnas nuevas en tablas existentes
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Estado)
                .HasDefaultValue("Activo");

            modelBuilder.Entity<Insumo>()
                .Property(i => i.Estado)
                .HasDefaultValue("Óptimo");

            modelBuilder.Entity<NotaMedica>()
    .HasIndex(n => n.CitaId)
    .IsUnique();


        }
    }
}