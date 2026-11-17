using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;

namespace Clinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CitasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. LEER TODAS LAS CITAS (GET: api/citas/clinica/1)
        [HttpGet("clinica/{clinicaId}")]
        public async Task<ActionResult<IEnumerable<Cita>>> GetCitas(int clinicaId)
        {
            var citas = await _context.Citas
                .Where(c => c.ClinicaId == clinicaId)
                .OrderBy(c => c.FechaHora)
                .ToListAsync();

            return Ok(citas);
        }

        // 2. LEER UNA SOLA CITA (GET: api/citas/5)
        [HttpGet("{id}")]
        public async Task<ActionResult<Cita>> GetCita(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null) return NotFound("Cita no encontrada.");
            return Ok(cita);
        }

        // 3. CREAR CITA (POST: api/citas)
        [HttpPost]
        public async Task<IActionResult> CrearCita([FromBody] Cita nuevaCita)
        {
            // Se asume Pendiente por defecto
            nuevaCita.Estado = "Pendiente";
            
            _context.Citas.Add(nuevaCita);
            await _context.SaveChangesAsync();

            return Ok(nuevaCita);
        }

        // 4. EDITAR CITA (PUT: api/citas/5)
        [HttpPut("{id}")]
        public async Task<IActionResult> EditarCita(int id, [FromBody] Cita citaActualizada)
        {
            if (id != citaActualizada.Id) return BadRequest("Los IDs no coinciden.");

            _context.Entry(citaActualizada).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaExists(id)) return NotFound("La cita ya no existe.");
                else throw;
            }

            return Ok(citaActualizada);
        }

        // 5. ELIMINAR CITA (DELETE: api/citas/5)
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCita(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null) return NotFound();

            _context.Citas.Remove(cita);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Cita eliminada correctamente" });
        }

        private bool CitaExists(int id)
        {
            return _context.Citas.Any(e => e.Id == id);
        }
    }
}