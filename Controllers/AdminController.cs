using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.DTOs;

namespace Clinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly ClinicaDbContext _context;

        public AdminController(ClinicaDbContext context)
        {
            _context = context;
        }

        // GET: api/Admin/estadisticas
        [HttpGet("estadisticas")]
        public async Task<ActionResult<DashboardDTO>> GetEstadisticas()
        {
            var zonaHoraria = TimeZoneInfo.FindSystemTimeZoneById("America/Hermosillo");
            var hoyLocal = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, zonaHoraria).Date;
            var inicioHoy = TimeZoneInfo.ConvertTimeToUtc(hoyLocal, zonaHoraria);
            var finHoy = TimeZoneInfo.ConvertTimeToUtc(hoyLocal.AddDays(1), zonaHoraria);

            var totalCitasHoy = await _context.Citas
                .CountAsync(c => c.FechaHora >= inicioHoy && c.FechaHora < finHoy);

            var citasPendientes = await _context.Citas
                .CountAsync(c => c.Estado == "Agendada" || c.Estado == "Pendiente" || c.Estado == "En Espera");

            var totalPacientes = await _context.Pacientes.CountAsync();

            return Ok(new DashboardDTO
            {
                TotalCitasHoy = totalCitasHoy,
                CitasPendientes = citasPendientes,
                TotalPacientes = totalPacientes
            });
        }
    }
}