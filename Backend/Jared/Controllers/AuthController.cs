using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicksellAPI.Data;
using QuicksellAPI.Models;
using QuicksellAPI.Services;

namespace QuicksellAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly QuicksellDbContext _context;
    private readonly JwtService _jwtService;

    public AuthController(QuicksellDbContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        // Buscar usuario con su tipo
        var usuario = await _context.USUARIO
            .Include(u => u.TipoUsuario)  // ← IMPORTANTE: incluir tipo
            .FirstOrDefaultAsync(u => 
                u.email_usuario == request.email && 
                u.contraseña_usuario == request.contraseña);

        if (usuario == null)
        {
            return Unauthorized(new { message = "Credenciales inválidas" });
        }

        if (usuario.estado_usuario != "Activo")
        {
            return Unauthorized(new { message = "Usuario inactivo o suspendido" });
        }

        var token = _jwtService.GenerateToken(usuario);

        var response = new LoginResponse
        {
            token = token,
            email = usuario.email_usuario,
            nombre = usuario.nombre_usuario,
            tipo_usuario = usuario.TipoUsuario?.tipo_nombreUsuario ?? "Cliente",  // ← CAMBIO
            id_usuario = usuario.id_usuario
        };

        return Ok(response);
    }
}