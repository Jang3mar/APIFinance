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
    public class ExpendituresController : ControllerBase
    {
        private readonly FinanceDBContext _context;

        public ExpendituresController(FinanceDBContext context)
        {
            _context = context;
        }

        // GET: api/Expenditures
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Expenditure>>> GetExpenditures()
        {
          if (_context.Expenditures == null)
          {
              return NotFound();
          }
            return await _context.Expenditures.ToListAsync();
        }

        // GET: api/Expenditures/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Expenditure>> GetExpenditure(int id)
        {
          if (_context.Expenditures == null)
          {
              return NotFound();
          }
            var expenditure = await _context.Expenditures.FindAsync(id);

            if (expenditure == null)
            {
                return NotFound();
            }

            return expenditure;
        }

        // PUT: api/Expenditures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutExpenditure(int id, Expenditure expenditure)
        {
            if (id != expenditure.IdExpenditure)
            {
                return BadRequest();
            }

            _context.Entry(expenditure).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenditureExists(id))
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

        // POST: api/Expenditures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Expenditure>> PostExpenditure(Expenditure expenditure)
        {
          if (_context.Expenditures == null)
          {
              return Problem("Entity set 'FinanceDBContext.Expenditures'  is null.");
          }
            _context.Expenditures.Add(expenditure);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpenditure", new { id = expenditure.IdExpenditure }, expenditure);
        }

        // DELETE: api/Expenditures/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteExpenditure(int id)
        {
            if (_context.Expenditures == null)
            {
                return NotFound();
            }
            var expenditure = await _context.Expenditures.FindAsync(id);
            if (expenditure == null)
            {
                return NotFound();
            }

            _context.Expenditures.Remove(expenditure);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("mdel")]
        [Authorize]
        public async Task<IActionResult> DeleteExpenditures([FromQuery] List<int> ids)
        {
            if (_context.Expenditures == null)
            {
                return NotFound();
            }
            foreach (int item in ids)
            {
                var exp = await _context.Expenditures.FindAsync(item);

                if (exp == null) continue;

                _context.Expenditures.Remove(exp);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpenditureExists(int id)
        {
            return (_context.Expenditures?.Any(e => e.IdExpenditure == id)).GetValueOrDefault();
        }
    }
}
