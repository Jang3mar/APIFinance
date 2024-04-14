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
    public class BudgetsController : ControllerBase
    {
        private readonly FinanceDBContext _context;

        public BudgetsController(FinanceDBContext context)
        {
            _context = context;
        }

        // GET: api/Budgets
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Budget>>> GetBudgets()
        {
          if (_context.Budgets == null)
          {
              return NotFound();
          }
            return await _context.Budgets.ToListAsync();
        }

        // GET: api/Budgets/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Budget>> GetBudget(int id)
        {
          if (_context.Budgets == null)
          {
              return NotFound();
          }
            var budget = await _context.Budgets.FindAsync(id);

            if (budget == null)
            {
                return NotFound();
            }

            return budget;
        }

        // PUT: api/Budgets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutBudget(int id, Budget budget)
        {
            if (id != budget.IdBudget)
            {
                return BadRequest();
            }

            _context.Entry(budget).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BudgetExists(id))
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

        // POST: api/Budgets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Budget>> PostBudget(Budget budget)
        {
          if (_context.Budgets == null)
          {
              return Problem("Entity set 'FinanceDBContext.Budgets'  is null.");
          }
            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBudget", new { id = budget.IdBudget }, budget);
        }

        // DELETE: api/Budgets/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBudget(int id)
        {
            if (_context.Budgets == null)
            {
                return NotFound();
            }
            var budget = await _context.Budgets.FindAsync(id);
            if (budget == null)
            {
                return NotFound();
            }

            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("mdel")]
        [Authorize]
        public async Task<IActionResult> DeleteBudgets([FromQuery] List<int> ids)
        {
            if (_context.Budgets == null)
            {
                return NotFound();
            }
            foreach (int item in ids)
            {
                var budget = await _context.Budgets.FindAsync(item);

                if (budget == null) continue;

                List<Operation> operations = _context.Operations.Where(x => x.BudgetId == budget.IdBudget).ToList();
                foreach (var operation in operations)
                {
                    _context.Operations.Remove(operation);
                }

                _context.Budgets.Remove(budget);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BudgetExists(int id)
        {
            return (_context.Budgets?.Any(e => e.IdBudget == id)).GetValueOrDefault();
        }
    }
}
