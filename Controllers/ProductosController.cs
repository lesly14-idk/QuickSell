using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicksellAPI.Data;
using QuicksellAPI.Models;

namespace QuicksellAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly QuicksellDbContext _context;

    public ProductosController(QuicksellDbContext context)
    {
        _context = context;
    }

    // GET: api/Productos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
    {
        return await _context.PRODUCTOS
            .Include(p => p.Proveedor)
            .Include(p => p.Categoria)
            .ToListAsync();
    }

    // GET: api/Productos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Producto>> GetProducto(int id)
    {
        var producto = await _context.PRODUCTOS
            .Include(p => p.Proveedor)
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.id_producto == id);

        if (producto == null)
        {
            return NotFound();
        }

        return producto;
    }

    // POST: api/Productos
    [HttpPost]
    public async Task<ActionResult<Producto>> PostProducto(Producto producto)
    {
        _context.PRODUCTOS.Add(producto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProducto), new { id = producto.id_producto }, producto);
    }

    // PUT: api/Productos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProducto(int id, Producto producto)
    {
        if (id != producto.id_producto)
        {
            return BadRequest();
        }

        _context.Entry(producto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductoExists(id))
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

    // DELETE: api/Productos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProducto(int id)
    {
        var producto = await _context.PRODUCTOS.FindAsync(id);
        if (producto == null)
        {
            return NotFound();
        }

        _context.PRODUCTOS.Remove(producto);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool ProductoExists(int id)
    {
        return _context.PRODUCTOS.Any(e => e.id_producto == id);
    }
}