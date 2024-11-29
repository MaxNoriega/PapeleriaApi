using ApiPaplria.Context;
using ApiPaplria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace ApiPaplria.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagoController : ControllerBase
    {
        private readonly AppDBContext _context;

        public PagoController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Pago
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pago>>> GetPagos()
        {
            return await _context.Pagos.ToListAsync();
        }

        // GET: api/Pago/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> GetPago(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);

            if (pago == null)
            {
                return NotFound();
            }

            return pago;
        }

        // POST: api/Pago
        [HttpPost]
        public async Task<ActionResult<Pago>> PostPago(Pago pago)
        {
            var venta = await _context.Ventas.FindAsync(pago.IdVenta);

            if (venta == null)
            {
                return BadRequest("La venta asociada no existe.");
            }

            pago.EstadoTrans = "Pendiente";

            if (pago.TipoPago == "Dinero")
            {
                // Simular comunicación con API del banco
                var resultadoBanco = ProcesarPagoConBanco(pago.CuentaOrigen, pago.CuentaDestino, pago.MontoTotal);

                if (resultadoBanco)
                {
                    pago.EstadoTrans = "Completado";
                }
                else
                {
                    pago.EstadoTrans = "Fallido";
                    return BadRequest("La transacción con el banco falló.");
                }
            }
            else if (pago.TipoPago == "Puntos")
            {
                var puntosAlumno = await _context.Puntos.FirstOrDefaultAsync(p => p.NumCtrl == venta.IdCliente);

                if (puntosAlumno == null || puntosAlumno.PuntosDisponibles < pago.MontoTotal)
                {
                    return BadRequest("El cliente no tiene suficientes puntos.");
                }

                // Deduce los puntos
                puntosAlumno.PuntosDisponibles -= pago.MontoTotal;
                pago.EstadoTrans = "Completado";
            }
            else
            {
                return BadRequest("Tipo de pago inválido. Debe ser 'Dinero' o 'Puntos'.");
            }

            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPago), new { id = pago.IdPago }, pago);
        }

        // DELETE: api/Pago/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePago(int id)
        {
            var pago = await _context.Pagos.FindAsync(id);
            if (pago == null)
            {
                return NotFound();
            }

            _context.Pagos.Remove(pago);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProcesarPagoConBanco(string cuentaOrigen, string cuentaDestino, decimal monto)
        {
            // Simulación de comunicación con una API bancaria
            // Aquí podrías implementar una llamada real a la API de un banco usando HttpClient.
            // Por ahora, asumimos que siempre es exitoso.
            return true;
        }

        private bool PagoExists(int id)
        {
            return _context.Pagos.Any(e => e.IdPago == id);
        }
    }
}
