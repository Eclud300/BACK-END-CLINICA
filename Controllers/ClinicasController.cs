using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;

namespace Clinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClinicasController : ControllerBase
    {
        private readonly ClinicaDbContext _context;

        public ClinicasController(ClinicaDbContext context)
        {
            _context = context;
        }

        // GET: api/Clinicas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClinicaInfo>>> GetClinicas()
        {
            return await _context.Clinicas.ToListAsync();
        }

        // GET: api/Clinicas/1
        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicaInfo>> GetClinica(int id)
        {
            var clinica = await _context.Clinicas.FindAsync(id);
            if (clinica == null) return NotFound("Clínica no encontrada.");
            return Ok(clinica);
        }

        // POST: api/Clinicas
        [HttpPost]
        public async Task<ActionResult<ClinicaInfo>> PostClinica(ClinicaInfo clinica)
        {
            _context.Clinicas.Add(clinica);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetClinica), new { id = clinica.Id }, clinica);
        }

        // PUT: api/Clinicas/1
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClinica(int id, ClinicaInfo clinica)
        {
            if (id != clinica.Id) return BadRequest("El ID no coincide.");

            _context.Entry(clinica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Clinicas.Any(c => c.Id == id)) return NotFound();
                throw;
            }

            return Ok(clinica);
        }
    }
}