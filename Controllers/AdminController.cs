using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;
using Clinica.DTOs;

namespace Clinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==========================================
        // 1. MÓDULO DE RESUMEN (DASHBOARD)
        // ==========================================
        
       [HttpGet("estadisticas/{clinicaId}")]
        public async Task<ActionResult<DashboardDto>> GetEstadisticas(int clinicaId)
        {
            var hoy = DateTime.UtcNow.Date;

            // C# cuenta los datos directamente desde las tablas
            var totalCitasHoy = await _context.Citas
                .Where(c => c.ClinicaId == clinicaId && c.FechaHora.Date == hoy)
                .CountAsync();

            var citasPendientes = await _context.Citas
                .Where(c => c.ClinicaId == clinicaId && c.Estado == "Pendiente")
                .CountAsync();

            var totalPacientes = await _context.Pacientes
                .Where(p => p.ClinicaId == clinicaId)
                .CountAsync();

            var stats = new DashboardDto
            {
                TotalCitasHoy = totalCitasHoy,
                CitasPendientes = citasPendientes,
                TotalPacientes = totalPacientes
            };

            return Ok(stats);
        }

        // ==========================================
        // 2. MÓDULO DE PERSONAL (EMPLEADOS Y ROLES)
        // ==========================================

        // VER LISTA DE PERSONAL
        [HttpGet("personal/{clinicaId}")]
        public async Task<ActionResult<IEnumerable<Empleado>>> GetPersonal(int clinicaId)
        {
            var personal = await _context.Empleados
                .Where(e => e.ClinicaId == clinicaId)
                .ToListAsync();

            return Ok(personal);
        }

        // AGREGAR NUEVO EMPLEADO (Ej. Nuevo Doctor o Recepcionista)
        [HttpPost("personal")]
        public async Task<IActionResult> AgregarPersonal([FromBody] Empleado nuevoEmpleado)
        {
            // Por defecto, un empleado nuevo entra como Activo
            nuevoEmpleado.Estado = "Activo"; 
            
            _context.Empleados.Add(nuevoEmpleado);
            await _context.SaveChangesAsync();

            return Ok(nuevoEmpleado);
        }

        // EDITAR DATOS DEL PERSONAL (Ej. Cambiar de rol o actualizar teléfono)
        [HttpPut("personal/{id}")]
        public async Task<IActionResult> EditarPersonal(int id, [FromBody] Empleado empleadoEditado)
        {
            if (id != empleadoEditado.Id) return BadRequest("Los IDs no coinciden.");

            _context.Entry(empleadoEditado).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(empleadoEditado);
        }

        // SUSPENDER PERSONAL (En lugar de borrarlo, le quitamos el acceso)
        [HttpPut("personal/{id}/suspender")]
        public async Task<IActionResult> SuspenderPersonal(int id)
        {
            var empleado = await _context.Empleados.FindAsync(id);
            if (empleado == null) return NotFound("Empleado no encontrado.");

            empleado.Estado = "Suspendido";
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = $"El acceso para {empleado.Nombre} ha sido suspendido." });
        }
    }
}