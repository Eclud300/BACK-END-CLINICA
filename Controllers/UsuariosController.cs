using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Clinica.Data;
using Clinica.Models;
using Clinica.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;

namespace Clinica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ClinicaDbContext _context;
        private readonly IConfiguration _config;

        private static Expression<Func<Usuario, UsuarioRespuestaDTO>> ProyeccionDto() => u => new UsuarioRespuestaDTO
        {
            Id = u.Id,
            Nombre = u.Nombre,
            Correo = u.Correo,
            Rol = u.Rol,
            Estado = u.Estado,
            Telefono = u.Telefono,
            FechaNacimiento = u.FechaNacimiento,
            Estudios = u.Estudios,
            UniversidadEgreso = u.UniversidadEgreso,
            CedulaProfesional = u.CedulaProfesional,
            CedulaEspecialidad = u.CedulaEspecialidad,
            FirmaDigital = u.FirmaDigital,
            PorcentajeComision = u.PorcentajeComision,
            TarifaConsulta = u.TarifaConsulta,
            FechaCreacion = u.FechaCreacion
        };

        private static readonly Func<Usuario, UsuarioRespuestaDTO> MapearDto = ProyeccionDto().Compile();

        public UsuariosController(ClinicaDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Usuarios
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioRespuestaDTO>>> GetUsuarios()
        {
            var usuarios = await _context.Usuarios
                .Select(ProyeccionDto())
                .ToListAsync();

            return Ok(usuarios);
        }

        // GET: api/Usuarios/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioRespuestaDTO>> GetUsuario(int id)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.Id == id)
                .Select(ProyeccionDto())
                .FirstOrDefaultAsync();

            if (usuario == null) return NotFound("Usuario no encontrado.");
            return Ok(usuario);
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<UsuarioRespuestaDTO>> PostUsuario(Usuario usuario)
        {
            var correoNormalizado = usuario.Correo.Trim().ToLower();
            bool correoExiste = await _context.Usuarios
                .AnyAsync(u => u.Correo.ToLower() == correoNormalizado);

            if (correoExiste)
            {
                return Conflict("Ya existe un usuario registrado con ese correo.");
            }

            usuario.Correo = correoNormalizado;
            usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(usuario.PasswordHash);
            if (string.IsNullOrWhiteSpace(usuario.Estado))
            {
                usuario.Estado = "Activo";
            }

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, MapearDto(usuario));
        }

        // PUT: api/Usuarios/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioActualizarDTO dto)
        {
            if (id != dto.Id) return BadRequest("Los IDs no coinciden.");

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound("Usuario no encontrado.");

            var correoNormalizado = dto.Correo.Trim().ToLower();
            bool correoExiste = await _context.Usuarios
                .AnyAsync(u => u.Correo.ToLower() == correoNormalizado && u.Id != id);

            if (correoExiste)
            {
                return Conflict("Ya existe otro usuario registrado con ese correo.");
            }

            usuario.Nombre = dto.Nombre;
            usuario.Correo = correoNormalizado;
            usuario.Rol = dto.Rol;
            if (!string.IsNullOrWhiteSpace(dto.Estado)) usuario.Estado = dto.Estado;
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                usuario.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            }

            usuario.Telefono = dto.Telefono;
            usuario.FechaNacimiento = dto.FechaNacimiento;
            usuario.Estudios = dto.Estudios;
            usuario.UniversidadEgreso = dto.UniversidadEgreso;
            usuario.CedulaProfesional = dto.CedulaProfesional;
            usuario.CedulaEspecialidad = dto.CedulaEspecialidad;
            usuario.FirmaDigital = dto.FirmaDigital;
            usuario.PorcentajeComision = dto.PorcentajeComision;
            usuario.TarifaConsulta = dto.TarifaConsulta;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // PUT: api/Usuarios/5/suspender
        [Authorize]
        [HttpPut("{id}/suspender")]
        public async Task<IActionResult> SuspenderUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound("Usuario no encontrado.");

            usuario.Estado = "Suspendido";
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = $"El acceso para {usuario.Nombre} ha sido suspendido." });
        }

        // PUT: api/Usuarios/5/activar
        [Authorize]
        [HttpPut("{id}/activar")]
        public async Task<IActionResult> ActivarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound("Usuario no encontrado.");

            usuario.Estado = "Activo";
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = $"El acceso para {usuario.Nombre} ha sido reactivado." });
        }

        // POST: api/Usuarios/login
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDTO loginDto)
        {
            var correoNormalizado = loginDto.Correo.Trim().ToLower();
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo.ToLower() == correoNormalizado);

            if (usuario == null) return Unauthorized("Correo o contraseña incorrectos.");

            if (usuario.Estado == "Suspendido")
            {
                return Unauthorized("Tu cuenta ha sido suspendida. Contacta al administrador.");
            }

            bool passwordValida = BCrypt.Net.BCrypt.Verify(loginDto.Password, usuario.PasswordHash);
            if (!passwordValida) return Unauthorized("Correo o contraseña incorrectos.");

            usuario.UltimoLogin = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim(ClaimTypes.Role, usuario.Rol)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}