using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using Microsoft.AspNetCore.Authorization;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxesController : ControllerBase
    {
        private readonly FinanceDBContext _context;

        public TaxesController(FinanceDBContext context)
        {
            _context = context;
        }

        // GET: api/Taxes
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Tax>>> GetTaxes()
        {
          if (_context.Taxes == null)
          {
              return NotFound();
          }
            return await _context.Taxes.ToListAsync();
        }

        // GET: api/Taxes/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Tax>> GetTax(int id)
        {
          if (_context.Taxes == null)
          {
              return NotFound();
          }
            var tax = await _context.Taxes.FindAsync(id);

            if (tax == null)
            {
                return NotFound();
            }

            return tax;
        }

        // PUT: api/Taxes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutTax(int id, Tax tax)
        {
            if (id != tax.IdTax)
            {
                return BadRequest();
            }

            _context.Entry(tax).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaxExists(id))
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

        // POST: api/Taxes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Tax>> PostTax(Tax tax)
        {
          if (_context.Taxes == null)
          {
              return Problem("Entity set 'FinanceDBContext.Taxes'  is null.");
          }
            _context.Taxes.Add(tax);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTax", new { id = tax.IdTax }, tax);
        }

        // DELETE: api/Taxes/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteTax(int id)
        {
            if (_context.Taxes == null)
            {
                return NotFound();
            }
            var tax = await _context.Taxes.FindAsync(id);
            if (tax == null)
            {
                return NotFound();
            }

            _context.Taxes.Remove(tax);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("mdel")]
        [Authorize]
        public async Task<IActionResult> DeleteTaxes([FromQuery] List<int> ids)
        {
            if (_context.Taxes == null)
            {
                return NotFound();
            }
            foreach (int item in ids)
            {
                var tax = await _context.Taxes.FindAsync(item);

                if (tax == null) continue;

                _context.Taxes.Remove(tax);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaxExists(int id)
        {
            return (_context.Taxes?.Any(e => e.IdTax == id)).GetValueOrDefault();
        }
    }
}
