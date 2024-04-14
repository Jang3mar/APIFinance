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
    public class FinanceAccountsController : ControllerBase
    {
        private readonly FinanceDBContext _context;

        public FinanceAccountsController(FinanceDBContext context)
        {
            _context = context;
        }

        // GET: api/FinanceAccounts
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<FinanceAccount>>> GetFinanceAccounts()
        {
          if (_context.FinanceAccounts == null)
          {
              return NotFound();
          }
            return await _context.FinanceAccounts.ToListAsync();
        }

        // GET: api/FinanceAccounts/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<FinanceAccount>> GetFinanceAccount(int id)
        {
          if (_context.FinanceAccounts == null)
          {
              return NotFound();
          }
            var financeAccount = await _context.FinanceAccounts.FindAsync(id);

            if (financeAccount == null)
            {
                return NotFound();
            }

            return financeAccount;
        }

        // PUT: api/FinanceAccounts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutFinanceAccount(int id, FinanceAccount financeAccount)
        {
            if (id != financeAccount.IdFinanceAccount)
            {
                return BadRequest();
            }

            _context.Entry(financeAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinanceAccountExists(id))
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

        // POST: api/FinanceAccounts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<FinanceAccount>> PostFinanceAccount(FinanceAccount financeAccount)
        {
          if (_context.FinanceAccounts == null)
          {
              return Problem("Entity set 'FinanceDBContext.FinanceAccounts'  is null.");
          }
            _context.FinanceAccounts.Add(financeAccount);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFinanceAccount", new { id = financeAccount.IdFinanceAccount }, financeAccount);
        }

        // DELETE: api/FinanceAccounts/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteFinanceAccount(int id)
        {
            if (_context.FinanceAccounts == null)
            {
                return NotFound();
            }
            var financeAccount = await _context.FinanceAccounts.FindAsync(id);
            if (financeAccount == null)
            {
                return NotFound();
            }

            _context.FinanceAccounts.Remove(financeAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("mdel")]
        [Authorize]
        public async Task<IActionResult> DeleteFinAccounts([FromQuery] List<int> ids)
        {
            if (_context.FinanceAccounts == null)
            {
                return NotFound();
            }
            foreach (int item in ids)
            {
                var finAcc = await _context.FinanceAccounts.FindAsync(item);

                if (finAcc == null) continue;

                _context.FinanceAccounts.Remove(finAcc);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FinanceAccountExists(int id)
        {
            return (_context.FinanceAccounts?.Any(e => e.IdFinanceAccount == id)).GetValueOrDefault();
        }
    }
}
