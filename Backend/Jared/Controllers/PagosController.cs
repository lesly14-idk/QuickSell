using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicksellAPI.Data;
using QuicksellAPI.Models;

namespace QuicksellAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagosController : ControllerBase
{
    private readonly QuicksellDbContext _context;

    public PagosController(QuicksellDbContext context)
    {
        _context = context;
    }

    // GET: api/Pagos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pago>>> GetPagos()
    {
        return await _context.PAGOS
            .Include(p => p.SolicitudDeCompra)
            .ToListAsync();
    }

    // GET: api/Pagos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Pago>> GetPago(int id)
    {
        var pago = await _context.PAGOS
            .Include(p => p.SolicitudDeCompra)
            .FirstOrDefaultAsync(p => p.id_pagos == id);

        if (pago == null)
        {
            return NotFound();
        }

        return pago;
    }

    // POST: api/Pagos
    [HttpPost]
    public async Task<ActionResult<Pago>> PostPago(Pago pago)
    {
        _context.PAGOS.Add(pago);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPago), new { id = pago.id_pagos }, pago);
    }

    // PUT: api/Pagos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPago(int id, Pago pago)
    {
        if (id != pago.id_pagos)
        {
            return BadRequest();
        }

        _context.Entry(pago).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PagoExists(id))
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

    // DELETE: api/Pagos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePago(int id)
    {
        var pago = await _context.PAGOS.FindAsync(id);
        if (pago == null)
        {
            return NotFound();
        }

        _context.PAGOS.Remove(pago);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PagoExists(int id)
    {
        return _context.PAGOS.Any(e => e.id_pagos == id);
    }
}