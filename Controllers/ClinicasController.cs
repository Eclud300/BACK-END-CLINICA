using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;

namespace Clinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClinicasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtener datos de la clinica
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Clinica>> GetClinica(int id)
        {
            var clinica = await _context.Clinicas.FindAsync(id);
            if (clinica == null) return NotFound();
            return clinica;
        }

        // Actualizar datos de la clinica
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClinica(int id, Models.Clinica clinica)
        {
            if (id != clinica.Id) return BadRequest();
            _context.Entry(clinica).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(clinica);
        }
    }
}