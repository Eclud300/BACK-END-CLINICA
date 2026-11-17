using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;
using Microsoft.AspNetCore.Authorization;

namespace Clinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // 🔒 Protección JWT activada
    public class CitasController : ControllerBase
    {
        private readonly ClinicaDbContext _context;

        public CitasController(ClinicaDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/Citas (Obtener todas las citas de la clínica)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cita>>> GetCitas()
        {
            // Más adelante podemos usar .Include() para traer los nombres del doctor y paciente
            return await _context.Citas.ToListAsync();
        }

        // 2. GET: api/Citas/5 (Buscar una cita por ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<Cita>> GetCita(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null) return NotFound("Cita no encontrada.");
            return cita;
        }

        // 3. POST: api/Citas (Agendar una nueva cita)
        [HttpPost]
        public async Task<ActionResult<Cita>> PostCita(Cita cita)
        {
            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCita", new { id = cita.Id }, cita);
        }

        // 4. PUT: api/Citas/5 (Actualizar cita: ej. cambiar Estado a "Completada" o "Cancelada")
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCita(int id, Cita citaActualizada)
        {
            if (id != citaActualizada.Id) return BadRequest("El ID de la cita no coincide.");

            _context.Entry(citaActualizada).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitaExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // 5. DELETE: api/Citas/5 (Borrar cita del registro)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCita(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null) return NotFound();

            _context.Citas.Remove(cita);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CitaExists(int id)
        {
            return _context.Citas.Any(e => e.Id == id);
        }
    }
}