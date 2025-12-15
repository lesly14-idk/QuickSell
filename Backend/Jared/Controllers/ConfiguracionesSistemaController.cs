using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicksellAPI.Data;
using QuicksellAPI.Models;

namespace QuicksellAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConfiguracionesSistemaController : ControllerBase
{
    private readonly QuicksellDbContext _context;

    public ConfiguracionesSistemaController(QuicksellDbContext context)
    {
        _context = context;
    }

    // GET: api/ConfiguracionesSistema
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ConfiguracionesSistema>>> GetConfiguraciones()
    {
        return await _context.CONFIGURACIONES_SISTEMA
            .Include(c => c.Usuario)
            .ToListAsync();
    }

    // GET: api/ConfiguracionesSistema/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ConfiguracionesSistema>> GetConfiguracion(int id)
    {
        var config = await _context.CONFIGURACIONES_SISTEMA
            .Include(c => c.Usuario)
            .FirstOrDefaultAsync(c => c.id_config == id);

        if (config == null)
        {
            return NotFound();
        }

        return config;
    }

    // POST: api/ConfiguracionesSistema
    [HttpPost]
    public async Task<ActionResult<ConfiguracionesSistema>> PostConfiguracion(ConfiguracionesSistema config)
    {
        _context.CONFIGURACIONES_SISTEMA.Add(config);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetConfiguracion), new { id = config.id_config }, config);
    }

    // PUT: api/ConfiguracionesSistema/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutConfiguracion(int id, ConfiguracionesSistema config)
    {
        if (id != config.id_config)
        {
            return BadRequest();
        }

        _context.Entry(config).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ConfiguracionExists(id))
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

    // DELETE: api/ConfiguracionesSistema/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteConfiguracion(int id)
    {
        var config = await _context.CONFIGURACIONES_SISTEMA.FindAsync(id);
        if (config == null)
        {
            return NotFound();
        }

        _context.CONFIGURACIONES_SISTEMA.Remove(config);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ConfiguracionExists(int id)
    {
        return _context.CONFIGURACIONES_SISTEMA.Any(e => e.id_config == id);
    }
}