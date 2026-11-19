using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;
using Microsoft.AspNetCore.Authorization;
using Clinica.DTOs;
using System.Linq.Expressions;

namespace Clinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotasMedicasController : ControllerBase
    {
        private readonly ClinicaDbContext _context;

        private static Expression<Func<NotaMedica, NotaMedicaRespuestaDTO>> ProyeccionDto() => n => new NotaMedicaRespuestaDTO
        {
            Id = n.Id,
            PacienteId = n.PacienteId,
            CitaId = n.CitaId,
            DoctorId = n.DoctorId,
            NombrePaciente = n.Paciente!.Nombre + " " + n.Paciente.Apellidos,
            NombreDoctor = n.Doctor!.Nombre,
            MotivoConsulta = n.MotivoConsulta,
            FrecuenciaCardiaca = n.FrecuenciaCardiaca,
            FrecuenciaRespiratoria = n.FrecuenciaRespiratoria,
            Temperatura = n.Temperatura,
            SaturacionOxigeno = n.SaturacionOxigeno,
            Peso = n.Peso,
            Talla = n.Talla,
            IMC = n.IMC,
            ExploracionFisica = n.ExploracionFisica,
            Diagnostico = n.Diagnostico,
            CodigoCIE10 = n.CodigoCIE10,
            PlanTerapeutico = n.PlanTerapeutico,
            RecetaMedicamentos = n.RecetaMedicamentos,
            EstudiosAuxiliares = n.EstudiosAuxiliares,
            FechaRegistro = n.FechaRegistro
        };

        public NotasMedicasController(ClinicaDbContext context)
        {
            _context = context;
        }

        // GET: api/NotasMedicas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotaMedicaRespuestaDTO>>> GetNotasMedicas()
        {
            var notas = await _context.NotasMedicas
                .OrderByDescending(n => n.FechaRegistro)
                .Select(ProyeccionDto())
                .ToListAsync();

            return Ok(notas);
        }

        // GET: api/NotasMedicas/historial/5
        [HttpGet("historial/{pacienteId}")]
        public async Task<ActionResult<IEnumerable<NotaMedicaRespuestaDTO>>> GetHistorialPorPaciente(int pacienteId)
        {
            var notas = await _context.NotasMedicas
                .Where(n => n.PacienteId == pacienteId)
                .OrderByDescending(n => n.FechaRegistro)
                .Select(ProyeccionDto())
                .ToListAsync();

            return Ok(notas);
        }

        // GET: api/NotasMedicas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotaMedicaRespuestaDTO>> GetNotaMedica(int id)
        {
            var nota = await _context.NotasMedicas
                .Where(n => n.Id == id)
                .Select(ProyeccionDto())
                .FirstOrDefaultAsync();

            if (nota == null) return NotFound("Nota médica no encontrada.");
            return Ok(nota);
        }

        // POST: api/NotasMedicas
        [HttpPost]
        public async Task<ActionResult<NotaMedica>> PostNotaMedica(NotaMedica notaMedica)
        {
            bool notaExiste = await _context.NotasMedicas
                .AnyAsync(n => n.CitaId == notaMedica.CitaId);

            if (notaExiste)
            {
                return Conflict("Esta cita ya tiene una nota médica registrada.");
            }

            _context.NotasMedicas.Add(notaMedica);

            var cita = await _context.Citas.FindAsync(notaMedica.CitaId);
            if (cita != null)
            {
                cita.Estado = "Completada";
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNotaMedica), new { id = notaMedica.Id }, notaMedica);
        }

        // PUT: api/NotasMedicas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotaMedica(int id, NotaMedica notaMedicaActualizada)
        {
            if (id != notaMedicaActualizada.Id) return BadRequest("El ID no coincide.");

            bool citaYaTieneNota = await _context.NotasMedicas
    .AnyAsync(n => n.CitaId == notaMedicaActualizada.CitaId && n.Id != id);

            if (citaYaTieneNota)
            {
                return Conflict("Esa cita ya tiene otra nota médica registrada.");
            }

            _context.Entry(notaMedicaActualizada).State = EntityState.Modified;
            _context.Entry(notaMedicaActualizada).Property(n => n.FechaRegistro).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotaMedicaExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // DELETE: api/NotasMedicas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotaMedica(int id)
        {
            var nota = await _context.NotasMedicas.FindAsync(id);
            if (nota == null) return NotFound();

            _context.NotasMedicas.Remove(nota);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotaMedicaExists(int id)
        {
            return _context.NotasMedicas.Any(e => e.Id == id);
        }
    }
}