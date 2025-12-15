using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicksellAPI.Data;
using QuicksellAPI.Models;

namespace QuicksellAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistorialOperacionesController : ControllerBase
{
    private readonly QuicksellDbContext _context;

    public HistorialOperacionesController(QuicksellDbContext context)
    {
        _context = context;
    }

    // GET: api/HistorialOperaciones
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HistorialDeOperaciones>>> GetHistorialOperaciones()
    {
        return await _context.HISTORIAL_DE_OPERACIONES
            .Include(h => h.Usuario)
            .ToListAsync();
    }

    // GET: api/HistorialOperaciones/5
    [HttpGet("{id}")]
    public async Task<ActionResult<HistorialDeOperaciones>> GetHistorialOperacion(int id)
    {
        var historial = await _context.HISTORIAL_DE_OPERACIONES
            .Include(h => h.Usuario)
            .FirstOrDefaultAsync(h => h.id_operaciones == id);

        if (historial == null)
        {
            return NotFound();
        }

        return historial;
    }

    // POST: api/HistorialOperaciones
    [HttpPost]
    public async Task<ActionResult<HistorialDeOperaciones>> PostHistorialOperacion(HistorialDeOperaciones historial)
    {
        _context.HISTORIAL_DE_OPERACIONES.Add(historial);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHistorialOperacion), new { id = historial.id_operaciones }, historial);
    }

    // PUT: api/HistorialOperaciones/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHistorialOperacion(int id, HistorialDeOperaciones historial)
    {
        if (id != historial.id_operaciones)
        {
            return BadRequest();
        }

        _context.Entry(historial).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!HistorialOperacionExists(id))
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

    // DELETE: api/HistorialOperaciones/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHistorialOperacion(int id)
    {
        var historial = await _context.HISTORIAL_DE_OPERACIONES.FindAsync(id);
        if (historial == null)
        {
            return NotFound();
        }

        _context.HISTORIAL_DE_OPERACIONES.Remove(historial);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool HistorialOperacionExists(int id)
    {
        return _context.HISTORIAL_DE_OPERACIONES.Any(e => e.id_operaciones == id);
    }
}