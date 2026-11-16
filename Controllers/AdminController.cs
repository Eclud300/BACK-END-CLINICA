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

        // GET: api/admin/estadisticas/1
        [HttpGet("estadisticas/{clinicaId}")]
        public async Task<ActionResult<DashboardDto>> GetEstadisticas(int clinicaId)
        {
            // Ejecutamos la función de Postgres que creaste en Neon
            var stats = await _context.Database
                .SqlQueryRaw<DashboardDto>($"SELECT * FROM fn_GetEstadisticasDashboard({clinicaId})")
                .ToListAsync();

            return Ok(stats.FirstOrDefault());
        }

        // GET: api/admin/citas/pendientes/1
        [HttpGet("citas/pendientes/{clinicaId}")]
        public async Task<ActionResult<IEnumerable<Cita>>> GetCitasPendientes(int clinicaId)
        {
            var citas = await _context.Citas
                .Where(c => c.ClinicaId == clinicaId && c.Estado == "Pendiente")
                .ToListAsync();

            return Ok(citas);
        }

        // PUT: api/admin/citas/estado/5
        [HttpPut("citas/estado/{citaId}")]
        public async Task<IActionResult> ActualizarEstadoCita(int citaId, [FromBody] string nuevoEstado)
        {
            var cita = await _context.Citas.FindAsync(citaId);
            if (cita == null) return NotFound();

            cita.Estado = nuevoEstado;
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Estado actualizado correctamente" });
        }
    }
}