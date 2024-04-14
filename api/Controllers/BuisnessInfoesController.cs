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
    public class BuisnessInfoesController : ControllerBase
    {
        private readonly FinanceDBContext _context;

        public BuisnessInfoesController(FinanceDBContext context)
        {
            _context = context;
        }

        // GET: api/BuisnessInfoes
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<BuisnessInfo>>> GetBuisnessInfos()
        {
          if (_context.BuisnessInfos == null)
          {
              return NotFound();
          }
            return await _context.BuisnessInfos.ToListAsync();
        }

        // GET: api/BuisnessInfoes/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<BuisnessInfo>> GetBuisnessInfo(int id)
        {
          if (_context.BuisnessInfos == null)
          {
              return NotFound();
          }
            var buisnessInfo = await _context.BuisnessInfos.FindAsync(id);

            if (buisnessInfo == null)
            {
                return NotFound();
            }

            return buisnessInfo;
        }

        // PUT: api/BuisnessInfoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutBuisnessInfo(int id, BuisnessInfo buisnessInfo)
        {
            if (id != buisnessInfo.IdBuisness)
            {
                return BadRequest();
            }

            _context.Entry(buisnessInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuisnessInfoExists(id))
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

        // POST: api/BuisnessInfoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BuisnessInfo>> PostBuisnessInfo(BuisnessInfo buisnessInfo)
        {
          if (_context.BuisnessInfos == null)
          {
              return Problem("Entity set 'FinanceDBContext.BuisnessInfos'  is null.");
          }
            _context.BuisnessInfos.Add(buisnessInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuisnessInfo", new { id = buisnessInfo.IdBuisness }, buisnessInfo);
        }

        // DELETE: api/BuisnessInfoes/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBuisnessInfo(int id)
        {
            if (_context.BuisnessInfos == null)
            {
                return NotFound();
            }
            var buisnessInfo = await _context.BuisnessInfos.FindAsync(id);
            if (buisnessInfo == null)
            {
                return NotFound();
            }

            _context.BuisnessInfos.Remove(buisnessInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("mdel")]
        [Authorize]
        public async Task<IActionResult> DeleteBuisnessInfoes([FromQuery] List<int> ids)
        {
            if (_context.BuisnessInfos == null)
            {
                return NotFound();
            }
            foreach (int item in ids)
            {
                var buisInfo = await _context.BuisnessInfos.FindAsync(item);

                if (buisInfo == null) continue;

                _context.BuisnessInfos.Remove(buisInfo);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuisnessInfoExists(int id)
        {
            return (_context.BuisnessInfos?.Any(e => e.IdBuisness == id)).GetValueOrDefault();
        }
    }
}
