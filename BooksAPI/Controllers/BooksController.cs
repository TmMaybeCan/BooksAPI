using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using BooksAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksContext _context;

        public BooksController(BooksContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Books>>> GetBooks() => 
            await _context.Books.Include(e => e.Author).ToListAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<Books>> GetBook(int id)
        {
            var book = await _context.Books.Include(e => e.Author).FirstOrDefaultAsync(e => e.BookId == id);
            if (book == null)
                return NotFound();
            return book;
        }

        [HttpPost]
        public async Task<ActionResult<Books>> PostBook(Books book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Books book)
        {
            if (id != book.BookId)
                return BadRequest();

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Books>> DeleteBook(int id)
        {
            var book = await _context.Books.Include(e => e.Author).FirstOrDefaultAsync(e => e.BookId == id);

            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return book;
        }

        private bool BookExists(int id) => _context.Books.Include(e => e.Author).Any(e => e.BookId == id);
    }
}










