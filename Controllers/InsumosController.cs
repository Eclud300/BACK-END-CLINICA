using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;
using Microsoft.AspNetCore.Authorization;

namespace Clinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InsumosController : ControllerBase
    {
        private readonly ClinicaDbContext _context;

        public InsumosController(ClinicaDbContext context)
        {
            _context = context;
        }

        // GET: api/Insumos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Insumo>>> GetInsumos()
        {
            return await _context.Insumos.ToListAsync();
        }

        // GET: api/Insumos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Insumo>> GetInsumo(int id)
        {
            var insumo = await _context.Insumos.FindAsync(id);
            if (insumo == null) return NotFound("Insumo no encontrado.");
            return Ok(insumo);
        }

        // POST: api/Insumos
        [HttpPost]
        public async Task<ActionResult<Insumo>> PostInsumo(Insumo insumo)
        {
            var nombreNormalizado = insumo.Nombre.Trim().ToLower();
            bool insumoExiste = await _context.Insumos
                .AnyAsync(i => i.Nombre.ToLower() == nombreNormalizado);

            if (insumoExiste)
            {
                return Conflict("Ya existe un insumo registrado con ese nombre.");
            }

            insumo.Nombre = insumo.Nombre.Trim();
            _context.Insumos.Add(insumo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInsumo), new { id = insumo.Id }, insumo);
        }

        // PUT: api/Insumos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsumo(int id, Insumo insumo)
        {
            if (id != insumo.Id) return BadRequest("El ID no coincide.");

            var nombreNormalizado = insumo.Nombre.Trim().ToLower();
            bool insumoExiste = await _context.Insumos
                .AnyAsync(i => i.Nombre.ToLower() == nombreNormalizado && i.Id != id);

            if (insumoExiste)
            {
                return Conflict("Ya existe otro insumo registrado con ese nombre.");
            }

            insumo.Nombre = insumo.Nombre.Trim();
            _context.Entry(insumo).State = EntityState.Modified;
            _context.Entry(insumo).Property(i => i.FechaRegistro).IsModified = false;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Insumos.Any(e => e.Id == id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // DELETE: api/Insumos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsumo(int id)
        {
            var insumo = await _context.Insumos.FindAsync(id);
            if (insumo == null) return NotFound();

            _context.Insumos.Remove(insumo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}