using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;
using Microsoft.AspNetCore.Authorization;

namespace Clinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // 🔒 Protegido
    public class InsumosController : ControllerBase
    {
        private readonly ClinicaDbContext _context;

        public InsumosController(ClinicaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Insumo>>> GetInsumos()
        {
            return await _context.Insumos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Insumo>> PostInsumo(Insumo insumo)
        {
            _context.Insumos.Add(insumo);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetInsumos", new { id = insumo.Id }, insumo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsumo(int id, Insumo insumo)
        {
            if (id != insumo.Id) return BadRequest();
            _context.Entry(insumo).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

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