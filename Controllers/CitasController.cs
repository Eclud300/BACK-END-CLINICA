using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;
using Clinica.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Clinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CitasController : ControllerBase
    {
        private readonly ClinicaDbContext _context;

        public CitasController(ClinicaDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/Citas (Devuelve el DTO limpio y aplanado)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitaRespuestaDTO>>> GetCitas()
        {
            var citas = await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .Select(c => new CitaRespuestaDTO
                {
                    Id = c.Id,
                    PacienteId = c.PacienteId,
                    NombrePaciente = c.Paciente!.Nombre + " " + c.Paciente.Apellidos,
                    CurpPaciente = c.Paciente.CURP,
                    DoctorId = c.DoctorId,
                    NombreDoctor = c.Doctor!.Nombre,
                    RolDoctor = c.Doctor.Rol,
                    FechaHora = c.FechaHora,
                    MotivoConsulta = c.MotivoConsulta,
                    Estado = c.Estado,
                    NotasRecepcion = c.NotasRecepcion
                })
                .ToListAsync();

            return Ok(citas);
        }

        // 2. GET: api/Citas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CitaRespuestaDTO>> GetCita(int id)
        {
            var cita = await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .Where(c => c.Id == id)
                .Select(c => new CitaRespuestaDTO
                {
                    Id = c.Id,
                    PacienteId = c.PacienteId,
                    NombrePaciente = c.Paciente!.Nombre + " " + c.Paciente.Apellidos,
                    CurpPaciente = c.Paciente.CURP,
                    DoctorId = c.DoctorId,
                    NombreDoctor = c.Doctor!.Nombre,
                    RolDoctor = c.Doctor.Rol,
                    FechaHora = c.FechaHora,
                    MotivoConsulta = c.MotivoConsulta,
                    Estado = c.Estado,
                    NotasRecepcion = c.NotasRecepcion
                })
                .FirstOrDefaultAsync();

            if (cita == null) return NotFound("Cita no encontrada.");
            return Ok(cita);
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