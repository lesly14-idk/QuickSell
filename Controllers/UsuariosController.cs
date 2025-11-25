using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicksellAPI.Data;
using QuicksellAPI.Models;

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

    // GET: api/Usuarios
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
    {
        return await _context.USUARIO.ToListAsync();
    }

    // GET: api/Usuarios/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetUsuario(int id)
    {
        var usuario = await _context.USUARIO.FindAsync(id);

        if (usuario == null)
        {
            return NotFound();
        }

        return usuario;
    }

    // POST: api/Usuarios
    [HttpPost]
    public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
    {
        _context.USUARIO.Add(usuario);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUsuario), new { id = usuario.id_usuario }, usuario);
    }

    // PUT: api/Usuarios/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
    {
        if (id != usuario.id_usuario)
        {
            return BadRequest();
        }

        _context.Entry(usuario).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UsuarioExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/Usuarios/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var usuario = await _context.USUARIO.FindAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }

        _context.USUARIO.Remove(usuario);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UsuarioExists(int id)
    {
        return _context.USUARIO.Any(e => e.id_usuario == id);
    }
}