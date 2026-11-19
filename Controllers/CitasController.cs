using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;
using Clinica.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;

namespace Clinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CitasController : ControllerBase
    {
        private readonly ClinicaDbContext _context;

        private static Expression<Func<Cita, CitaRespuestaDTO>> ProyeccionDto() => c => new CitaRespuestaDTO
        {
            Id = c.Id,
            Folio = c.Folio,
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
        };

        public CitasController(ClinicaDbContext context)
        {
            _context = context;
        }

        // GET: api/Citas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CitaRespuestaDTO>>> GetCitas()
        {
            var citas = await _context.Citas
                .OrderBy(c => c.FechaHora)
                .Select(ProyeccionDto())
                .ToListAsync();

            return Ok(citas);
        }

        // GET: api/Citas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CitaRespuestaDTO>> GetCita(int id)
        {
            var cita = await _context.Citas
                .Where(c => c.Id == id)
                .Select(ProyeccionDto())
                .FirstOrDefaultAsync();

            if (cita == null) return NotFound("Cita no encontrada.");
            return Ok(cita);
        }

        // POST: api/Citas
        [HttpPost]
        public async Task<ActionResult<Cita>> PostCita(Cita cita)
        {
            bool empalme = await _context.Citas.AnyAsync(c =>
                c.DoctorId == cita.DoctorId &&
                c.FechaHora == cita.FechaHora &&
                c.Estado != "Cancelada");

            if (empalme)
            {
                return Conflict("El doctor ya tiene una cita agendada en esa fecha y hora.");
            }

            if (string.IsNullOrWhiteSpace(cita.Folio))
            {
                cita.Folio = $"CIT-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString("N")[..4].ToUpper()}";
            }
            else
            {
                cita.Folio = cita.Folio.Trim().ToUpper();
                bool folioExiste = await _context.Citas.AnyAsync(c => c.Folio == cita.Folio);
                if (folioExiste) return Conflict("Ya existe una cita registrada con ese folio.");
            }

            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCita), new { id = cita.Id }, cita);
        }

        // PUT: api/Citas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCita(int id, Cita citaActualizada)
        {
            if (id != citaActualizada.Id) return BadRequest("El ID de la cita no coincide.");

            if (citaActualizada.Estado != "Cancelada")
            {
                bool empalme = await _context.Citas.AnyAsync(c =>
                    c.Id != id &&
                    c.DoctorId == citaActualizada.DoctorId &&
                    c.FechaHora == citaActualizada.FechaHora &&
                    c.Estado != "Cancelada");

                if (empalme)
                {
                    return Conflict("El doctor ya tiene otra cita agendada en esa fecha y hora.");
                }
            }

            bool conservarFolioOriginal = string.IsNullOrWhiteSpace(citaActualizada.Folio);
            if (!conservarFolioOriginal)
            {
                citaActualizada.Folio = citaActualizada.Folio!.Trim().ToUpper();
                bool folioExiste = await _context.Citas.AnyAsync(c => c.Folio == citaActualizada.Folio && c.Id != id);
                if (folioExiste) return Conflict("Ya existe otra cita registrada con ese folio.");
            }

            _context.Entry(citaActualizada).State = EntityState.Modified;
            if (conservarFolioOriginal)
            {
                _context.Entry(citaActualizada).Property(c => c.Folio).IsModified = false;
            }

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

        // DELETE: api/Citas/5
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