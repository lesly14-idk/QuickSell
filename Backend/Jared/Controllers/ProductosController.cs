using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicksellAPI.Data;
using QuicksellAPI.Models;
using QuicksellAPI.DTOs;

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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
    {
        return await _context.PRODUCTOS.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Producto>> GetProducto(int id)
    {
        var producto = await _context.PRODUCTOS.FindAsync(id);
        if (producto == null) return NotFound();
        return producto;
    }

    [HttpPost]
    public async Task<ActionResult<Producto>> PostProducto(ProductoCreateDto dto)
    {
        // Validar proveedor
        var proveedorExists = await _context.PROVEEDORES.AnyAsync(p => p.id_proveedor == dto.id_proveedor);
        if (!proveedorExists)
        {
            return BadRequest($"El proveedor con ID {dto.id_proveedor} no existe");
        }

        // Validar categoría
        var categoriaExists = await _context.CATEGORIA.AnyAsync(c => c.id_categoria == dto.id_categoria);
        if (!categoriaExists)
        {
            return BadRequest($"La categoría con ID {dto.id_categoria} no existe");
        }

        var producto = new Producto
        {
            id_proveedor = dto.id_proveedor,
            id_categoria = dto.id_categoria,
            nombre_producto = dto.nombre_producto,
            descripcion_producto = dto.descripcion_producto,
            precio_producto = dto.precio_producto,
            stock_producto = dto.stock_producto,
            estado_creacion = dto.estado_creacion,
            fecha_creacion = DateTime.Now,
            validado_por_admin = dto.validado_por_admin
        };

        _context.PRODUCTOS.Add(producto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProducto), new { id = producto.id_producto }, producto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProducto(int id, Producto producto)
    {
        if (id != producto.id_producto) return BadRequest();
        _context.Entry(producto).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductoExists(id)) return NotFound();
            throw;
        }
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProducto(int id)
    {
        var producto = await _context.PRODUCTOS.FindAsync(id);
        if (producto == null) return NotFound();
        
        _context.PRODUCTOS.Remove(producto);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool ProductoExists(int id)
    {
        return _context.PRODUCTOS.Any(e => e.id_producto == id);
    }
}