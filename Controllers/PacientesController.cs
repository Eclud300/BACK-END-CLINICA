using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;

[Route("api/[controller]")]
[ApiController]
public class PacientesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public PacientesController(ApplicationDbContext context) => _context = context;

    [HttpGet("clinica/{clinicaId}")]
    public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes(int clinicaId)
    {
        // Solo devuelve los pacientes de la clínica actual
        return await _context.Pacientes.Where(p => p.ClinicaId == clinicaId).ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> AgregarPaciente([FromBody] Paciente paciente)
    {
        _context.Pacientes.Add(paciente);
        await _context.SaveChangesAsync();
        return Ok(paciente);
    }
    [HttpPut("{id}")]
public async Task<IActionResult> PutPaciente(int id, Paciente paciente) {
    if (id != paciente.Id) return BadRequest("IDs no coinciden");
    _context.Entry(paciente).State = EntityState.Modified;
    await _context.SaveChangesAsync();
    return Ok(paciente);
}

[HttpDelete("{id}")]
public async Task<IActionResult> DeletePaciente(int id) {
    var paciente = await _context.Pacientes.FindAsync(id);
    if (paciente == null) return NotFound();
    _context.Pacientes.Remove(paciente);
    await _context.SaveChangesAsync();
    return Ok();
}


}