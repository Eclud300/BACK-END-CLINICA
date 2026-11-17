using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;
using Microsoft.AspNetCore.Authorization;

namespace Clinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // 🔒 Todo este controlador requiere Token JWT para poder usarse
    public class PacientesController : ControllerBase
    {
        private readonly ClinicaDbContext _context;

        public PacientesController(ClinicaDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/Pacientes (Obtener todos los pacientes)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
        {
            return await _context.Pacientes.ToListAsync();
        }

        // 2. GET: api/Pacientes/5 (Buscar un paciente específico por su ID)
        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> GetPaciente(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null) return NotFound("Paciente no encontrado.");
            return paciente;
        }

        // 3. POST: api/Pacientes (Registrar paciente nuevo)
        [HttpPost]
        public async Task<ActionResult<Paciente>> PostPaciente(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPaciente", new { id = paciente.Id }, paciente);
        }

        // 4. PUT: api/Pacientes/5 (Actualizar datos del paciente)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaciente(int id, Paciente pacienteActualizado)
        {
            if (id != pacienteActualizado.Id) return BadRequest("El ID no coincide.");

            _context.Entry(pacienteActualizado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PacienteExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // 5. DELETE: api/Pacientes/5 (Eliminar un paciente)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null) return NotFound();

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PacienteExists(int id)
        {
            return _context.Pacientes.Any(e => e.Id == id);
        }
    }
}