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
    public class CategoryController : ControllerBase
    {
        private readonly CategoryContext _context;

        public CategoryController(CategoryContext context)
        {
            _context = context;
        }

        // GET: api/Category
        // https://localhost:5008/api/Category
        [HttpGet]
        //  public async Task<ActionResult<IEnumerable<Category>>> GetCategory()
        // BOTH OK OK
        public async Task<IEnumerable<Category>> GetCategory()
        {
            return await _context.Categories.ToListAsync();
        }

        // GET: api/Category/NOK
        // https://localhost:5008/api/Category/NOK
        [HttpGet("{id}")]
       // public async Task<ActionResult<Category>> GetCategory(int currencyKey)
        public async Task<ActionResult<Category>> GetCategory(int id) //(int id)
        {
            // return await _context.Category.FindAsync(currencyKey);  //.ToListAsync();  //
            var result = await _context.Categories.FirstOrDefaultAsync(e => e.CategoryId == id);   //(e => e.CategoryKey == id);
             if (result == null)
            {
                return NotFound();
            } 

            return result;
        }

        // PUT: api/Category/DKK
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //public async Task<IActionResult> PutCustomer(string id, Category currency)
        public async Task<ActionResult<Category>> PutCategory(int id, Category currency)
        {
            if (id != currency.CategoryId)
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
                if (!CategoryExisted(id))
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


        // POST: api/Category
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category currency)
        {
           // currency.CategoryKey = 0;  //_context.Category.Max(e => e.CategoryKey) + 1;
            _context.Categories.Add(currency);
            await _context.SaveChangesAsync();
//string s1 = (currency.CategoryAlternateKey).ToString();
            return currency;  //"SUCCESS, " ;  //+ currency.CategoryAlternateKey;  //CreatedAtAction("GetCategory", new {currency});  //  new { CategoryAlternateKey = currency.CategoryAlternateKey}, currency);
        }

        // DELETE: api/Category/USD
        [HttpDelete("{id}")]
       // public async Task<IActionResult> DeleteCustomer(string CategoryAlternateKey)
           public async Task<ActionResult<Category>> DeleteCustomer(int id)
        {
           // var currency = await _context.Category.FindAsync(CategoryAlternateKey);
            var currency = await _context.Categories.FirstOrDefaultAsync(e => e.CategoryId == id);   //(e => e.CategoryKey == id);
            if (currency == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(currency);
            await _context.SaveChangesAsync();

            return currency;  //NoContent();
        }

        private bool CategoryExisted(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }


    
    }
}
