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
    public class CurrenciesController : ControllerBase
    {
        private readonly FinanceDBContext _context;

        public CurrenciesController(FinanceDBContext context)
        {
            _context = context;
        }

        // GET: api/Currencies
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<Currency>>> GetCurrencies()
        {
          if (_context.Currencies == null)
          {
              return NotFound();
          }
            return await _context.Currencies.ToListAsync();
        }

        // GET: api/Currencies/5
        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<Currency>> GetCurrency(int id)
        {
          if (_context.Currencies == null)
          {
              return NotFound();
          }
            var currency = await _context.Currencies.FindAsync(id);

            if (currency == null)
            {
                return NotFound();
            }

            return currency;
        }

        // PUT: api/Currencies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCurrency(int id, Currency currency)
        {
            if (id != currency.IdCurrency)
            {
                return BadRequest();
            }

            _context.Entry(currency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrencyExists(id))
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

        // POST: api/Currencies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Currency>> PostCurrency(Currency currency)
        {
          if (_context.Currencies == null)
          {
              return Problem("Entity set 'FinanceDBContext.Currencies'  is null.");
          }
            _context.Currencies.Add(currency);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurrency", new { id = currency.IdCurrency }, currency);
        }

        // DELETE: api/Currencies/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            if (_context.Currencies == null)
            {
                return NotFound();
            }
            var currency = await _context.Currencies.FindAsync(id);
            if (currency == null)
            {
                return NotFound();
            }

            _context.Currencies.Remove(currency);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("mdel")]
        [Authorize]
        public async Task<IActionResult> DeleteCurrencies([FromQuery] List<int> ids)
        {
            if (_context.Currencies == null)
            {
                return NotFound();
            }
            foreach (int item in ids)
            {
                var currency = await _context.Currencies.FindAsync(item);

                if (currency == null) continue;

                _context.Currencies.Remove(currency);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CurrencyExists(int id)
        {
            return (_context.Currencies?.Any(e => e.IdCurrency == id)).GetValueOrDefault();
        }
    }
}
