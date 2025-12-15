using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicksellAPI.Data;
using QuicksellAPI.Models;

namespace QuicksellAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SolicitudesDeCompraController : ControllerBase
{
    private readonly QuicksellDbContext _context;

    public SolicitudesDeCompraController(QuicksellDbContext context)
    {
        _context = context;
    }

    // GET: api/SolicitudesDeCompra
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SolicitudDeCompra>>> GetSolicitudesDeCompra()
    {
        return await _context.SOLICITUDES_DE_COMPRA
            .Include(s => s.Usuario)
            .Include(s => s.Proveedor)
            .ToListAsync();
    }

    // GET: api/SolicitudesDeCompra/5
    [HttpGet("{id}")]
    public async Task<ActionResult<SolicitudDeCompra>> GetSolicitudDeCompra(int id)
    {
        var solicitud = await _context.SOLICITUDES_DE_COMPRA
            .Include(s => s.Usuario)
            .Include(s => s.Proveedor)
            .Include(s => s.DetallesSolicitud)
            .Include(s => s.Pagos)
            .Include(s => s.Envios)
            .FirstOrDefaultAsync(s => s.id_solicitud == id);

        if (solicitud == null)
        {
            return NotFound();
        }

        return solicitud;
    }

    // POST: api/SolicitudesDeCompra
    [HttpPost]
    public async Task<ActionResult<SolicitudDeCompra>> PostSolicitudDeCompra(SolicitudDeCompra solicitud)
    {
        _context.SOLICITUDES_DE_COMPRA.Add(solicitud);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSolicitudDeCompra), new { id = solicitud.id_solicitud }, solicitud);
    }

    // PUT: api/SolicitudesDeCompra/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSolicitudDeCompra(int id, SolicitudDeCompra solicitud)
    {
        if (id != solicitud.id_solicitud)
        {
            return BadRequest();
        }

        _context.Entry(solicitud).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!SolicitudDeCompraExists(id))
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

    // DELETE: api/SolicitudesDeCompra/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSolicitudDeCompra(int id)
    {
        var solicitud = await _context.SOLICITUDES_DE_COMPRA.FindAsync(id);
        if (solicitud == null)
        {
            return NotFound();
        }

        _context.SOLICITUDES_DE_COMPRA.Remove(solicitud);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SolicitudDeCompraExists(int id)
    {
        return _context.SOLICITUDES_DE_COMPRA.Any(e => e.id_solicitud == id);
    }
}