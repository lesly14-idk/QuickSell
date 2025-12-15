using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicksellAPI.Data;
using QuicksellAPI.Models;
using QuicksellAPI.DTOs;

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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedores()
    {
        return await _context.PROVEEDORES.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Proveedor>> GetProveedor(int id)
    {
        var proveedor = await _context.PROVEEDORES.FindAsync(id);
        if (proveedor == null) return NotFound();
        return proveedor;
    }

    [HttpPost]
    public async Task<ActionResult<Proveedor>> PostProveedor(ProveedorCreateDto dto)
    {
        // Validar que el usuario existe
        var usuarioExists = await _context.USUARIO.AnyAsync(u => u.id_usuario == dto.id_usuario);
        if (!usuarioExists)
        {
            return BadRequest($"El usuario con ID {dto.id_usuario} no existe");
        }

        var proveedor = new Proveedor
        {
            id_usuario = dto.id_usuario,
            nombre_empresa = dto.nombre_empresa,
            telefono = dto.telefono,
            direccion = dto.direccion,
            catalogo_activo = dto.catalogo_activo,
            validado_por_proveedor = dto.validado_por_proveedor,
            validado_por_admin = dto.validado_por_admin
        };

        _context.PROVEEDORES.Add(proveedor);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProveedor), new { id = proveedor.id_proveedor }, proveedor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProveedor(int id, Proveedor proveedor)
    {
        if (id != proveedor.id_proveedor) return BadRequest();
        _context.Entry(proveedor).State = EntityState.Modified;
        
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProveedorExists(id)) return NotFound();
            throw;
        }
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProveedor(int id)
    {
        var proveedor = await _context.PROVEEDORES.FindAsync(id);
        if (proveedor == null) return NotFound();
        
        _context.PROVEEDORES.Remove(proveedor);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool ProveedorExists(int id)
    {
        return _context.PROVEEDORES.Any(e => e.id_proveedor == id);
    }
}