using ApiPaplria.Context;
using ApiPaplria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPaplria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetalleVentaController : ControllerBase
    {
        private readonly AppDBContext _context;

        public DetalleVentaController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/DetalleVenta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleVenta>>> GetDetallesVenta()
        {
            return await _context.DetallesVenta
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                .ToListAsync();
        }

        // GET: api/DetalleVenta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleVenta>> GetDetalleVenta(int id)
        {
            var detalleVenta = await _context.DetallesVenta
                .Include(d => d.Producto)
                .Include(d => d.Venta)
                .FirstOrDefaultAsync(d => d.IdDetalle == id);

            if (detalleVenta == null)
            {
                return NotFound();
            }

            return detalleVenta;
        }

        // POST: api/DetalleVenta
        [HttpPost]
        public async Task<ActionResult<DetalleVenta>> PostDetalleVenta(DetalleVenta detalleVenta)
        {
            // Validar que el producto existe y tiene suficiente stock
            var producto = await _context.Productos.FindAsync(detalleVenta.IdPro);
            if (producto == null)
            {
                return BadRequest("El producto no existe.");
            }

            if (producto.Stock < detalleVenta.CantidadProd)
            {
                return BadRequest("Stock insuficiente para realizar la venta.");
            }

            // Restar stock
            producto.Stock -= detalleVenta.CantidadProd;

            // Guardar el detalle de venta
            _context.DetallesVenta.Add(detalleVenta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDetalleVenta), new { id = detalleVenta.IdDetalle }, detalleVenta);
        }

        // PUT: api/DetalleVenta/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDetalleVenta(int id, DetalleVenta detalleVenta)
        {
            if (id != detalleVenta.IdDetalle)
            {
                return BadRequest();
            }

            _context.Entry(detalleVenta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetalleVentaExists(id))
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

        // DELETE: api/DetalleVenta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetalleVenta(int id)
        {
            var detalleVenta = await _context.DetallesVenta.FindAsync(id);
            if (detalleVenta == null)
            {
                return NotFound();
            }

            // Devolver stock al producto
            var producto = await _context.Productos.FindAsync(detalleVenta.IdPro);
            if (producto != null)
            {
                producto.Stock += detalleVenta.CantidadProd;
            }

            _context.DetallesVenta.Remove(detalleVenta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetalleVentaExists(int id)
        {
            return _context.DetallesVenta.Any(e => e.IdDetalle == id);
        }
    }

}
