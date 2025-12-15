using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicksellAPI.Data;
using QuicksellAPI.Models;

namespace QuicksellAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly QuicksellDbContext _context;

    public CategoriasController(QuicksellDbContext context)
    {
        _context = context;
    }

    // GET: api/Categorias
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategorias()
    {
        return await _context.CATEGORIA.ToListAsync();
    }

    // GET: api/Categorias/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Categoria>> GetCategoria(int id)
    {
        var categoria = await _context.CATEGORIA.FindAsync(id);

        if (categoria == null)
        {
            return NotFound();
        }

        return categoria;
    }

    // POST: api/Categorias
    [HttpPost]
    public async Task<ActionResult<Categoria>> PostCategoria(Categoria categoria)
    {
        _context.CATEGORIA.Add(categoria);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCategoria), new { id = categoria.id_categoria }, categoria);
    }

    // PUT: api/Categorias/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
    {
        if (id != categoria.id_categoria)
        {
            return BadRequest();
        }

        _context.Entry(categoria).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoriaExists(id))
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

    // DELETE: api/Categorias/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoria(int id)
    {
        var categoria = await _context.CATEGORIA.FindAsync(id);
        if (categoria == null)
        {
            return NotFound();
        }

        _context.CATEGORIA.Remove(categoria);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CategoriaExists(int id)
    {
        return _context.CATEGORIA.Any(e => e.id_categoria == id);
    }
}