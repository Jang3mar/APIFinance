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
    public class ExpenditureEmployeesController : ControllerBase
    {
        private readonly FinanceDBContext _context;

        public ExpenditureEmployeesController(FinanceDBContext context)
        {
            _context = context;
        }

        // GET: api/ExpenditureEmployees
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ExpenditureEmployee>>> GetExpenditureEmployees()
        {
          if (_context.ExpenditureEmployees == null)
          {
              return NotFound();
          }
            return await _context.ExpenditureEmployees.ToListAsync();
        }

        // GET: api/ExpenditureEmployees/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ExpenditureEmployee>> GetExpenditureEmployee(int id)
        {
          if (_context.ExpenditureEmployees == null)
          {
              return NotFound();
          }
            var expenditureEmployee = await _context.ExpenditureEmployees.FindAsync(id);

            if (expenditureEmployee == null)
            {
                return NotFound();
            }

            return expenditureEmployee;
        }

        // PUT: api/ExpenditureEmployees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutExpenditureEmployee(int id, ExpenditureEmployee expenditureEmployee)
        {
            if (id != expenditureEmployee.IdExpenditureEmployee)
            {
                return BadRequest();
            }

            _context.Entry(expenditureEmployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenditureEmployeeExists(id))
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

        // POST: api/ExpenditureEmployees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ExpenditureEmployee>> PostExpenditureEmployee(ExpenditureEmployee expenditureEmployee)
        {
          if (_context.ExpenditureEmployees == null)
          {
              return Problem("Entity set 'FinanceDBContext.ExpenditureEmployees'  is null.");
          }
            _context.ExpenditureEmployees.Add(expenditureEmployee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExpenditureEmployee", new { id = expenditureEmployee.IdExpenditureEmployee }, expenditureEmployee);
        }

        // DELETE: api/ExpenditureEmployees/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteExpenditureEmployee(int id)
        {
            if (_context.ExpenditureEmployees == null)
            {
                return NotFound();
            }
            var expenditureEmployee = await _context.ExpenditureEmployees.FindAsync(id);
            if (expenditureEmployee == null)
            {
                return NotFound();
            }

            _context.ExpenditureEmployees.Remove(expenditureEmployee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("mdel")]
        [Authorize]
        public async Task<IActionResult> DeleteExpEmployee([FromQuery] List<int> ids)
        {
            if (_context.ExpenditureEmployees == null)
            {
                return NotFound();
            }
            foreach (int item in ids)
            {
                var expEmp = await _context.ExpenditureEmployees.FindAsync(item);

                if (expEmp == null) continue;

                _context.ExpenditureEmployees.Remove(expEmp);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpenditureEmployeeExists(int id)
        {
            return (_context.ExpenditureEmployees?.Any(e => e.IdExpenditureEmployee == id)).GetValueOrDefault();
        }
    }
}
