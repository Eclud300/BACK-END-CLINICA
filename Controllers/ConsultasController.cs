using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;

[Route("api/[controller]")]
[ApiController]
public class ConsultasController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public ConsultasController(ApplicationDbContext context) => _context = context;

    [HttpGet("historial/{pacienteId}")]
    public async Task<ActionResult<IEnumerable<Consulta>>> GetHistorial(int pacienteId)
    {
        // Devuelve todo el historial ordenado por fecha
        return await _context.Consultas
            .Where(c => c.PacienteId == pacienteId)
            .OrderByDescending(c => c.FechaConsulta)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> GuardarConsulta([FromBody] Consulta consulta)
    {
        _context.Consultas.Add(consulta);
        await _context.SaveChangesAsync();
        return Ok(consulta);
    }
    // GET: api/Consultas (Traer absolutamente todo para el Historial)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Consulta>>> GetConsultas()
        {
            return await _context.Consultas.ToListAsync();
        }
}