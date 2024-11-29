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
    public class PuntosController : ControllerBase
    {
        private readonly AppDBContext _context;

        public PuntosController(AppDBContext context)
        {
            _context = context;
        }

        // GET: api/Puntos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Puntos>>> GetPuntos()
        {
            return await _context.Puntos.ToListAsync();
        }

        // GET: api/Puntos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Puntos>> GetPuntos(int id)
        {
            var puntos = await _context.Puntos.FindAsync(id);

            if (puntos == null)
            {
                return NotFound();
            }

            return puntos;
        }

        // POST: api/Puntos
        [HttpPost]
        public async Task<ActionResult<Puntos>> PostPuntos(Puntos puntos)
        {
            _context.Puntos.Add(puntos);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPuntos), new { id = puntos.NumCtrl }, puntos);
        }

        // PUT: api/Puntos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPuntos(int id, Puntos puntos)
        {
            if (id != puntos.NumCtrl)
            {
                return BadRequest();
            }

            _context.Entry(puntos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PuntosExists(id))
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

        // DELETE: api/Puntos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePuntos(int id)
        {
            var puntos = await _context.Puntos.FindAsync(id);
            if (puntos == null)
            {
                return NotFound();
            }

            _context.Puntos.Remove(puntos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PuntosExists(int id)
        {
            return _context.Puntos.Any(e => e.NumCtrl == id);
        }
    }
}
