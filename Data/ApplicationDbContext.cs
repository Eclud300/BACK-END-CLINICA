using Microsoft.EntityFrameworkCore;
using Clinica.Models;

namespace Clinica.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // ESTO ES LO QUE LE FALTA A TU ARCHIVO (Las colecciones de tablas)
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Inventario> Inventarios { get; set; }
        public DbSet<Clinica.Models.Clinica> Clinicas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeo en minúsculas para que coincida con tu base de datos PostgreSQL
            modelBuilder.Entity<Cita>().ToTable("citas");
            modelBuilder.Entity<Empleado>().ToTable("empleados");
            modelBuilder.Entity<Paciente>().ToTable("pacientes");
            modelBuilder.Entity<Consulta>().ToTable("consultas");
            modelBuilder.Entity<Inventario>().ToTable("inventario"); 
            
        }
    }
}