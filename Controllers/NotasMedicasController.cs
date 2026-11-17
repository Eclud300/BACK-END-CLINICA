using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;
using Microsoft.AspNetCore.Authorization;
using Clinica.DTOs;

namespace Clinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // 🔒 Bóveda de seguridad activa con JWT
    public class NotasMedicasController : ControllerBase
    {
        private readonly ClinicaDbContext _context;

        public NotasMedicasController(ClinicaDbContext context)
        {
            _context = context;
        }

        // 1. GET: api/NotasMedicas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotaMedicaRespuestaDTO>>> GetNotasMedicas()
        {
            var notas = await _context.NotasMedicas
                .Include(n => n.Paciente)
                .Include(n => n.Doctor)
                .OrderByDescending(n => n.FechaRegistro)
                .Select(n => new NotaMedicaRespuestaDTO
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
                })
                .ToListAsync();

            return Ok(notas);
        }
        // 2. GET: api/NotasMedicas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotaMedicaRespuestaDTO>> GetNotaMedica(int id)
        {
            var nota = await _context.NotasMedicas
                .Include(n => n.Paciente)
                .Include(n => n.Doctor)
                .Where(n => n.Id == id)
                .Select(n => new NotaMedicaRespuestaDTO
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
                })
                .FirstOrDefaultAsync();

            if (nota == null) return NotFound("Nota médica no encontrada.");
            return Ok(nota);
        }

        // 3. POST: api/NotasMedicas (El médico guarda la consulta y se autocompleta la cita)
        [HttpPost]
        public async Task<ActionResult<NotaMedica>> PostNotaMedica(NotaMedica notaMedica)
        {
            // A) Preparamos la nota médica para guardarla
            _context.NotasMedicas.Add(notaMedica);

            // B) Buscamos la cita que corresponde a esta nota
            var cita = await _context.Citas.FindAsync(notaMedica.CitaId);

            // C) Si la cita existe, le cambiamos el estado automáticamente
            if (cita != null)
            {
                cita.Estado = "Completada";
            }

            // D) Ejecutamos los cambios en PostgreSQL (Guarda la nota Y actualiza la cita al mismo tiempo)
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotaMedica", new { id = notaMedica.Id }, notaMedica);
        }

        // 4. PUT: api/NotasMedicas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotaMedica(int id, NotaMedica notaMedicaActualizada)
        {
            if (id != notaMedicaActualizada.Id) return BadRequest("El ID no coincide.");

            _context.Entry(notaMedicaActualizada).State = EntityState.Modified;

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

        // 5. DELETE: api/NotasMedicas/5
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