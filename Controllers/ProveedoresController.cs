using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicksellAPI.Data;
using QuicksellAPI.Models;

namespace QuicksellAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProveedoresController : ControllerBase
{
    private readonly QuicksellDbContext _context;

    public ProveedoresController(QuicksellDbContext context)
    {
        _context = context;
    }

    // GET: api/Proveedores
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedores()
    {
        return await _context.PROVEEDORES.Include(p => p.Usuario).ToListAsync();
    }

    // GET: api/Proveedores/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Proveedor>> GetProveedor(int id)
    {
        var proveedor = await _context.PROVEEDORES
            .Include(p => p.Usuario)
            .FirstOrDefaultAsync(p => p.id_proveedor == id);

        if (proveedor == null)
        {
            return NotFound();
        }

        return proveedor;
    }

    // POST: api/Proveedores
    [HttpPost]
    public async Task<ActionResult<Proveedor>> PostProveedor(Proveedor proveedor)
    {
        _context.PROVEEDORES.Add(proveedor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProveedor), new { id = proveedor.id_proveedor }, proveedor);
    }

    // PUT: api/Proveedores/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProveedor(int id, Proveedor proveedor)
    {
        if (id != proveedor.id_proveedor)
        {
            return BadRequest();
        }

        _context.Entry(proveedor).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProveedorExists(id))
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

    // DELETE: api/Proveedores/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProveedor(int id)
    {
        var proveedor = await _context.PROVEEDORES.FindAsync(id);
        if (proveedor == null)
        {
            return NotFound();
        }

        _context.PROVEEDORES.Remove(proveedor);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProveedorExists(int id)
    {
        return _context.PROVEEDORES.Any(e => e.id_proveedor == id);
    }
}