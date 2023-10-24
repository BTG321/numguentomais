using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using beyondthegame.Controllers;
using beyondthegame.Models;

namespace numguentomais.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class curtidasController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public curtidasController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/curtidas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<curtida>>> Get()
        {
          if (_context.Curtidas == null)
          {
              return NotFound();
          }
            return await _context.Curtidas.ToListAsync();
        }

        // GET: api/curtidas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<curtida>> Get(int id)
        {
          if (_context.Curtidas == null)
          {
              return NotFound();
          }
            var curtida = await _context.Curtidas.FindAsync(id);

            if (curtida == null)
            {
                return NotFound();
            }

            return curtida;
        }

        // PUT: api/curtidas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, curtida curtida)
        {
            if (id != curtida.id)
            {
                return BadRequest();
            }

            _context.Entry(curtida).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!curtidaExists(id))
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

        // POST: api/curtidas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<curtida>> Postcurtida(curtida curtida)
        {
          if (_context.Curtidas == null)
          {
              return Problem("Entity set 'DatabaseContext.Curtidas'  is null.");
          }
            _context.Curtidas.Add(curtida);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getcurtida", new { id = curtida.id }, curtida);
        }

        // DELETE: api/curtidas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletecurtida(int id)
        {
            if (_context.Curtidas == null)
            {
                return NotFound();
            }
            var curtida = await _context.Curtidas.FindAsync(id);
            if (curtida == null)
            {
                return NotFound();
            }

            _context.Curtidas.Remove(curtida);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool curtidaExists(int id)
        {
            return (_context.Curtidas?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
