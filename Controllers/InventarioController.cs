using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;

[Route("api/[controller]")]
[ApiController]
public class InventarioController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public InventarioController(ApplicationDbContext context) => _context = context;

    [HttpGet("clinica/{clinicaId}")]
    public async Task<ActionResult<IEnumerable<Inventario>>> GetInventario(int clinicaId)
    {
        return await _context.Inventarios.Where(i => i.ClinicaId == clinicaId).ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> AgregarProducto([FromBody] Inventario producto)
    {
        _context.Inventarios.Add(producto);
        await _context.SaveChangesAsync();
        return Ok(producto);
    }
    
    // Método para EDITAR un producto
    [HttpPut("{id}")]
    public async Task<IActionResult> PutInventario(int id, Inventario inventario)
    {
        if (id != inventario.Id)
        {
            return BadRequest("El ID no coincide");
        }

        _context.Entry(inventario).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // CORRECCIÓN: Agregamos la 's' a Inventarios
            if (!_context.Inventarios.Any(e => e.Id == id))
            {
                return NotFound("El producto no existe en la base de datos");
            }
            else
            {
                throw;
            }
        }

        return Ok(inventario);
    }

    // Método para ELIMINAR un producto
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInventario(int id)
    {
        // CORRECCIÓN: Agregamos la 's' a Inventarios
        var inventario = await _context.Inventarios.FindAsync(id);
        if (inventario == null)
        {
            return NotFound("El producto no existe");
        }

        // CORRECCIÓN: Agregamos la 's' a Inventarios
        _context.Inventarios.Remove(inventario);
        await _context.SaveChangesAsync();

        return Ok();
    }
}