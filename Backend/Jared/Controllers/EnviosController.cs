using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicksellAPI.Data;
using QuicksellAPI.Models;

namespace QuicksellAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnviosController : ControllerBase
{
    private readonly QuicksellDbContext _context;

    public EnviosController(QuicksellDbContext context)
    {
        _context = context;
    }

    // GET: api/Envios
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Envio>>> GetEnvios()
    {
        return await _context.ENVIAR
            .Include(e => e.SolicitudDeCompra)
            .ToListAsync();
    }

    // GET: api/Envios/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Envio>> GetEnvio(int id)
    {
        var envio = await _context.ENVIAR
            .Include(e => e.SolicitudDeCompra)
            .FirstOrDefaultAsync(e => e.id_envios == id);

        if (envio == null)
        {
            return NotFound();
        }

        return envio;
    }

    // POST: api/Envios
    [HttpPost]
    public async Task<ActionResult<Envio>> PostEnvio(Envio envio)
    {
        _context.ENVIAR.Add(envio);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetEnvio), new { id = envio.id_envios }, envio);
    }

    // PUT: api/Envios/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEnvio(int id, Envio envio)
    {
        if (id != envio.id_envios)
        {
            return BadRequest();
        }

        _context.Entry(envio).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!EnvioExists(id))
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

    // DELETE: api/Envios/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEnvio(int id)
    {
        var envio = await _context.ENVIAR.FindAsync(id);
        if (envio == null)
        {
            return NotFound();
        }

        _context.ENVIAR.Remove(envio);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool EnvioExists(int id)
    {
        return _context.ENVIAR.Any(e => e.id_envios == id);
    }
}