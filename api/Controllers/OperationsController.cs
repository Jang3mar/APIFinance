using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly FinanceDBContext _context;
        private readonly ILogger<UsersController> _logger;

        public OperationsController(FinanceDBContext context, ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger;
        }
        private DateTime getTime(string date)
        {
            string[] strings = date.Split('.');
            List<int> ints = new List<int>();
            for (int i = 0; i < strings.Length; i++)
            {
                int a;
                if (Int32.TryParse(strings[i].Replace("'", ""), out a)) ints.Add(a);
                _logger.LogInformation($"{a}, из строки {strings[i]}");
            }
            DateTime date_converted = new DateTime(ints[2], ints[1], ints[0]);
            return date_converted;
        }
        // GET: api/Operations
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Operation>>> GetOperations([FromQuery] string? start_date,
            [FromQuery] string? end_date)
        {
          if (_context.Operations == null)
          {
              return NotFound();
            }
            List<Operation> operations = await _context.Operations.ToListAsync();
            if (start_date != null)
            {
                operations = operations.Where(x => x.DateOperation >= getTime(start_date)).ToList();
                
            }
            if (end_date != null)
            {
                operations = operations.Where(x => x.DateOperation <= getTime(end_date)).ToList();
            }
            return operations;
        }

        // GET: api/Operations/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Operation>> GetOperation(int id)
        {
          if (_context.Operations == null)
          {
              return NotFound();
          }
            var operation = await _context.Operations.FindAsync(id);

            if (operation == null)
            {
                return NotFound();
            }

            return operation;
        }

        // PUT: api/Operations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutOperation(int id, Operation operation)
        {
            if (id != operation.IdOperation)
            {
                return BadRequest();
            }

            _context.Entry(operation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperationExists(id))
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

        // POST: api/Operations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Operation>> PostOperation(Operation operation)
        {
          if (_context.Operations == null)
          {
              return Problem("Entity set 'FinanceDBContext.Operations'  is null.");
          }
            _context.Operations.Add(operation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOperation", new { id = operation.IdOperation }, operation);
        }

        // DELETE: api/Operations/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteOperation(int id)
        {
            if (_context.Operations == null)
            {
                return NotFound();
            }
            var operation = await _context.Operations.FindAsync(id);
            if (operation == null)
            {
                return NotFound();
            }

            _context.Operations.Remove(operation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("mdel")]
        [Authorize]
        public async Task<IActionResult> DeleteOperations([FromQuery] List<int> ids)
        {
            if (_context.Operations == null)
            {
                return NotFound();
            }
            foreach (int item in ids)
            {
                var operation = await _context.Operations.FindAsync(item);

                if (operation == null) continue;

                _context.Operations.Remove(operation);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OperationExists(int id)
        {
            return (_context.Operations?.Any(e => e.IdOperation == id)).GetValueOrDefault();
        }
    }
}
