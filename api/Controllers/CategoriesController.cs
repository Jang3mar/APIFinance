﻿using System;
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
    public class CategoriesController : ControllerBase
    {
        private readonly FinanceDBContext _context;

        public CategoriesController(FinanceDBContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
          if (_context.Categories == null)
          {
              return NotFound();
          }
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (id != category.IdCategory)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
          if (_context.Categories == null)
          {
              return Problem("Entity set 'FinanceDBContext.Categories'  is null.");
          }
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.IdCategory }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("mdel")]
        [Authorize]
        public async Task<IActionResult> DeleteCategories([FromQuery] List<int> ids)
        {
            if (_context.Categories == null)
            {
                return NotFound();
            }
            foreach (int item in ids)
            {
                var category = await _context.Categories.FindAsync(item);

                if (category == null) continue;

                _context.Categories.Remove(category);
            }
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return (_context.Categories?.Any(e => e.IdCategory == id)).GetValueOrDefault();
        }
    }
}
