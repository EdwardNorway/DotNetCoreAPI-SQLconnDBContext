using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;
using DotNetCoreAPI.Models;
using DotNetCoreAPI.DAL;

/***********    how to connet to SQL DB  ::   ***************
// SUCCESS :: OK OK 2021-12-22

(1). appsettings.json - file:
    "ConnectionStrings": {
        "OrderContext": "Server=(localdb)\\mssqllocaldb;Database=OrderContext-61f239f7-761f-47d2-acac-a5d38d352e9f;Trusted_Connection=True;MultipleActiveResultSets=true",
        "DefaultConnection": "User ID=cs; password=Cenium12345!; server=CHDPC-T480\\SQLEXPRESS; Database=AdventureWorksDW2012;"
    } 

(2). .csproj-file:
    <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="5.0.7" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="5.0.7" />
(3). startup.cs - file:
            string _sqlConnectionStrings = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<Many2ManyContext>(opt => 
                                                opt.UseSqlServer(_sqlConnectionStrings));
(4). Models folder:
    two files
    class Item
    class ItemContext
(5). controller


*********************************************************/

namespace DotNetCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookContext _context;

        public BookController(BookContext context)
        {
            _context = context;
        }

        // GET: api/Book
        // https://localhost:5008/api/Book
        [HttpGet]
        //  public async Task<ActionResult<IEnumerable<Book>>> GetBook()
        // BOTH OK OK
        public async Task<IEnumerable<Book>> GetBook()
        {
            return await _context.Books.ToListAsync();
        }

        // GET: api/Book/NOK
        // https://localhost:5008/api/Book/NOK
        [HttpGet("{id}")]
       // public async Task<ActionResult<Book>> GetBook(int currencyKey)
        public async Task<ActionResult<Book>> GetBook(int id) //(int id)
        {
            // return await _context.Book.FindAsync(currencyKey);  //.ToListAsync();  //
            var result = await _context.Books.FirstOrDefaultAsync(e => e.BookId == id);   //(e => e.BookKey == id);
             if (result == null)
            {
                return NotFound();
            } 

            return result;
        }

        // PUT: api/Books/DKK
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //public async Task<IActionResult> PutCustomer(string id, Book currency)
        public async Task<ActionResult<Book>> PutBook(int id, Book currency)
        {
            if (id != currency.BookId)
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
                if (!BookExisted(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return currency;  //NoContent();
        }


        // POST: api/Book
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book currency)
        {
           // currency.BookKey = 0;  //_context.Book.Max(e => e.BookKey) + 1;
            _context.Books.Add(currency);
            await _context.SaveChangesAsync();
//string s1 = (currency.BookAlternateKey).ToString();
            return currency;  //"SUCCESS, " ;  //+ currency.BookAlternateKey;  //CreatedAtAction("GetBook", new {currency});  //  new { BookAlternateKey = currency.BookAlternateKey}, currency);
        }

        // DELETE: api/Book/USD
        [HttpDelete("{id}")]
       // public async Task<IActionResult> DeleteCustomer(string BookAlternateKey)
           public async Task<ActionResult<Book>> DeleteCustomer(int id)
        {
           // var currency = await _context.Book.FindAsync(BookAlternateKey);
            var currency = await _context.Books.FirstOrDefaultAsync(e => e.BookId == id);   //(e => e.BookKey == id);
            if (currency == null)
            {
                return NotFound();
            }

            _context.Books.Remove(currency);
            await _context.SaveChangesAsync();

            return currency;  //NoContent();
        }

        private bool BookExisted(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }


    
    }
}
