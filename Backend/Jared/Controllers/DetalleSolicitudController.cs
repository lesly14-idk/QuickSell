using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicksellAPI.Data;
using QuicksellAPI.Models;

namespace QuicksellAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DetalleSolicitudController : ControllerBase
{
    private readonly QuicksellDbContext _context;

    public DetalleSolicitudController(QuicksellDbContext context)
    {
        _context = context;
    }

    // GET: api/DetalleSolicitud
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetalleSolicitud>>> GetDetallesSolicitud()
    {
        return await _context.DETALLE_SOLICITUD
            .Include(d => d.SolicitudDeCompra)
            .Include(d => d.Producto)
            .ToListAsync();
    }

    // GET: api/DetalleSolicitud/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DetalleSolicitud>> GetDetalleSolicitud(int id)
    {
        var detalle = await _context.DETALLE_SOLICITUD
            .Include(d => d.SolicitudDeCompra)
            .Include(d => d.Producto)
            .FirstOrDefaultAsync(d => d.id_detalle == id);

        if (detalle == null)
        {
            return NotFound();
        }

        return detalle;
    }

    // POST: api/DetalleSolicitud
    [HttpPost]
    public async Task<ActionResult<DetalleSolicitud>> PostDetalleSolicitud(DetalleSolicitud detalle)
    {
        _context.DETALLE_SOLICITUD.Add(detalle);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetDetalleSolicitud), new { id = detalle.id_detalle }, detalle);
    }

    // PUT: api/DetalleSolicitud/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDetalleSolicitud(int id, DetalleSolicitud detalle)
    {
        if (id != detalle.id_detalle)
        {
            return BadRequest();
        }

        _context.Entry(detalle).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!DetalleSolicitudExists(id))
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

    // DELETE: api/DetalleSolicitud/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDetalleSolicitud(int id)
    {
        var detalle = await _context.DETALLE_SOLICITUD.FindAsync(id);
        if (detalle == null)
        {
            return NotFound();
        }

        _context.DETALLE_SOLICITUD.Remove(detalle);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool DetalleSolicitudExists(int id)
    {
        return _context.DETALLE_SOLICITUD.Any(e => e.id_detalle == id);
    }
}