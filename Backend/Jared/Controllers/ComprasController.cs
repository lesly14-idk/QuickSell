using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuicksellAPI.Data;
using QuicksellAPI.Models;

namespace QuicksellAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComprasController : ControllerBase
{
    private readonly QuicksellDbContext _context;
    private readonly ILogger<ComprasController> _logger;

    public ComprasController(QuicksellDbContext context, ILogger<ComprasController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // POST: api/Compras
    [HttpPost]
    public async Task<ActionResult<SolicitudDeCompra>> CrearCompra(SolicitudCompraRequest request)
    {
        // Iniciar transacción
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // 1. Validar que el usuario existe
            var usuarioExists = await _context.USUARIO.AnyAsync(u => u.id_usuario == request.id_usuario);
            if (!usuarioExists)
            {
                return BadRequest($"El usuario con ID {request.id_usuario} no existe");
            }

            // 2. Validar que el proveedor existe
            var proveedorExists = await _context.PROVEEDORES.AnyAsync(p => p.id_proveedor == request.id_proveedor);
            if (!proveedorExists)
            {
                return BadRequest($"El proveedor con ID {request.id_proveedor} no existe");
            }

            // 3. Validar que todos los productos existen y calcular el total
            decimal total = 0;
            var detallesParaGuardar = new List<DetalleSolicitud>();

            foreach (var detalle in request.detalles)
            {
                var producto = await _context.PRODUCTOS.FindAsync(detalle.id_producto);
                
                if (producto == null)
                {
                    return BadRequest($"El producto con ID {detalle.id_producto} no existe");
                }

                if (producto.stock_producto < detalle.cantidad)
                {
                    return BadRequest($"Stock insuficiente para el producto '{producto.nombre_producto}'. Stock disponible: {producto.stock_producto}");
                }

                // Calcular subtotal
                var subtotal = producto.precio_producto * detalle.cantidad;
                total += subtotal;

                // Crear detalle (aún no se guarda)
                var detalleItem = new DetalleSolicitud
                {
                    id_producto = detalle.id_producto,
                    cantidad = detalle.cantidad,
                    subtotal = subtotal
                };

                detallesParaGuardar.Add(detalleItem);
            }

            // 4. Crear la solicitud de compra
            var solicitud = new SolicitudDeCompra
            {
                id_usuario = request.id_usuario,
                id_proveedor = request.id_proveedor,
                fecha_solicitud = DateTime.Now,
                total = total,
                estado = "Pendiente",
                gestionado_por_admin = "FALSE",
                nota_seguimiento = request.nota_seguimiento
            };

            // Guardar solicitud
            _context.SOLICITUDES_DE_COMPRA.Add(solicitud);
            await _context.SaveChangesAsync();

            // 5. Agregar los detalles con el ID de la solicitud
            foreach (var detalle in detallesParaGuardar)
            {
                detalle.id_solicitud = solicitud.id_solicitud;
                _context.DETALLE_SOLICITUD.Add(detalle);
            }

            await _context.SaveChangesAsync();

            // 6. (Opcional) Actualizar el stock de los productos
            foreach (var detalle in request.detalles)
            {
                var producto = await _context.PRODUCTOS.FindAsync(detalle.id_producto);
                if (producto != null)
                {
                    producto.stock_producto -= detalle.cantidad;
                    
                    // Si el stock llega a 0, cambiar estado
                    if (producto.stock_producto == 0)
                    {
                        producto.estado_creacion = "Agotado";
                    }
                }
            }

            await _context.SaveChangesAsync();

            // 7. Confirmar transacción
            await transaction.CommitAsync();

            _logger.LogInformation("Compra creada exitosamente. Solicitud ID: {Id}", solicitud.id_solicitud);

            // 8. Retornar la solicitud creada con sus detalles
            var solicitudCompleta = await _context.SOLICITUDES_DE_COMPRA
                .Include(s => s.DetallesSolicitud)
                .FirstOrDefaultAsync(s => s.id_solicitud == solicitud.id_solicitud);

            return CreatedAtAction(
                nameof(ObtenerCompra), 
                new { id = solicitud.id_solicitud }, 
                solicitudCompleta
            );
        }
        catch (Exception ex)
        {
            // Si hay error, revertir todos los cambios
            await transaction.RollbackAsync();
            _logger.LogError(ex, "Error al crear la compra");
            return StatusCode(500, "Error al procesar la compra: " + ex.Message);
        }
    }

    // GET: api/Compras/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<SolicitudDeCompra>> ObtenerCompra(int id)
    {
        var solicitud = await _context.SOLICITUDES_DE_COMPRA
            .Include(s => s.Usuario)
            .Include(s => s.Proveedor)
            .Include(s => s.DetallesSolicitud)
            .FirstOrDefaultAsync(s => s.id_solicitud == id);

        if (solicitud == null)
        {
            return NotFound($"Solicitud con ID {id} no encontrada");
        }

        return Ok(solicitud);
    }

    // GET: api/Compras/usuario/{idUsuario}
    [HttpGet("usuario/{idUsuario}")]
    public async Task<ActionResult<IEnumerable<SolicitudDeCompra>>> ObtenerComprasPorUsuario(int idUsuario)
    {
        var solicitudes = await _context.SOLICITUDES_DE_COMPRA
            .Where(s => s.id_usuario == idUsuario)
            .Include(s => s.DetallesSolicitud)
            .OrderByDescending(s => s.fecha_solicitud)
            .ToListAsync();

        return Ok(solicitudes);
    }
}