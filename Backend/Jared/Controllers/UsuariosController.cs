using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicksellAPI.Data;
using QuicksellAPI.Models;
using QuicksellAPI.DTOs;

namespace QuicksellAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly QuicksellDbContext _context;

    public UsuariosController(QuicksellDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
    {
        return await _context.USUARIO.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetUsuario(int id)
    {
        var usuario = await _context.USUARIO.FindAsync(id);
        if (usuario == null) return NotFound();
        return usuario;
    }

    // POST con DTO limpio
    [HttpPost]
    public async Task<ActionResult<Usuario>> PostUsuario(UsuarioCreateDto dto)
    {
        var usuario = new Usuario
        {
            nombre_usuario = dto.nombre_usuario,
            telefono_usuario = dto.telefono_usuario,
            direccion_usuario = dto.direccion_usuario,
            email_usuario = dto.email_usuario,
            contraseña_usuario = dto.contraseña_usuario,
            tipo_usuario = dto.tipo_usuario,
            estado_usuario = dto.estado_usuario,
            fecha_registro = DateTime.Now
        };

        _context.USUARIO.Add(usuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUsuario), new { id = usuario.id_usuario }, usuario);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
    {
        if (id != usuario.id_usuario) return BadRequest();
        _context.Entry(usuario).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UsuarioExists(id)) return NotFound();
            throw;
        }
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var usuario = await _context.USUARIO.FindAsync(id);
        if (usuario == null) return NotFound();
        
        _context.USUARIO.Remove(usuario);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool UsuarioExists(int id)
    {
        return _context.USUARIO.Any(e => e.id_usuario == id);
    }
}