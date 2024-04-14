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
    public class IncomesController : ControllerBase
    {
        private readonly FinanceDBContext _context;

        public IncomesController(FinanceDBContext context)
        {
            _context = context;
        }

        // GET: api/Incomes
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Income>>> GetIncomes()
        {
          if (_context.Incomes == null)
          {
              return NotFound();
          }
            return await _context.Incomes.ToListAsync();
        }

        // GET: api/Incomes/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Income>> GetIncome(int id)
        {
          if (_context.Incomes == null)
          {
              return NotFound();
          }
            var income = await _context.Incomes.FindAsync(id);

            if (income == null)
            {
                return NotFound();
            }

            return income;
        }

        // PUT: api/Incomes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutIncome(int id, Income income)
        {
            if (id != income.IdIncome)
            {
                return BadRequest();
            }

            _context.Entry(income).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncomeExists(id))
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

        // POST: api/Incomes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Income>> PostIncome(Income income)
        {
          if (_context.Incomes == null)
          {
              return Problem("Entity set 'FinanceDBContext.Incomes'  is null.");
          }
            _context.Incomes.Add(income);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIncome", new { id = income.IdIncome }, income);
        }

        // DELETE: api/Incomes/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            if (_context.Incomes == null)
            {
                return NotFound();
            }
            var income = await _context.Incomes.FindAsync(id);
            if (income == null)
            {
                return NotFound();
            }

            _context.Incomes.Remove(income);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("mdel")]
        [Authorize]
        public async Task<IActionResult> DeleteIncomes([FromQuery] List<int> ids)
        {
            if (_context.Incomes == null)
            {
                return NotFound();
            }
            foreach (int item in ids)
            {
                var income = await _context.Incomes.FindAsync(item);

                if (income == null) continue;

                _context.Incomes.Remove(income);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IncomeExists(int id)
        {
            return (_context.Incomes?.Any(e => e.IdIncome == id)).GetValueOrDefault();
        }
    }
}
