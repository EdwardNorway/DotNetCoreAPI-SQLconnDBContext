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
// SUCCESS :: OK OK 2021-09-02

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
            services.AddDbContext<CurrencyContext>(opt => 
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
    public class CurrencyController : ControllerBase
    {
        private readonly CurrencyContext _context;

        public CurrencyController(CurrencyContext context)
        {
            _context = context;
        }

        // GET: api/Currency
        // https://localhost:5008/api/Currency
        [HttpGet]
        //  public async Task<ActionResult<IEnumerable<Currency>>> GetCurrency()
        // BOTH OK OK
        public async Task<IEnumerable<Currency>> GetCurrency()
        {
            return await _context.Currency.ToListAsync();
        }

        // GET: api/Currency/NOK
        // https://localhost:5008/api/Currency/NOK
        [HttpGet("{id}")]
       // public async Task<ActionResult<Currency>> GetCurrency(int currencyKey)
        public async Task<ActionResult<Currency>> GetCurrency(string id) //(int id)
        {
            // return await _context.Currency.FindAsync(currencyKey);  //.ToListAsync();  //
            var result = await _context.Currency.FirstOrDefaultAsync(e => e.CurrencyAlternateKey == id);   //(e => e.CurrencyKey == id);
             if (result == null)
            {
                return NotFound();
            } 

            return result;
        }

        // PUT: api/Currencys/DKK
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //public async Task<IActionResult> PutCustomer(string id, Currency currency)
        public async Task<ActionResult<Currency>> PutCurrency(string id, Currency currency)
        {
            if (id != currency.CurrencyAlternateKey)
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
                if (!CurrencyExisted(id))
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


        // POST: api/Currency
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Currency>> PostCurrency(Currency currency)
        {
           // currency.CurrencyKey = 0;  //_context.Currency.Max(e => e.CurrencyKey) + 1;
            _context.Currency.Add(currency);
            await _context.SaveChangesAsync();
//string s1 = (currency.CurrencyAlternateKey).ToString();
            return currency;  //"SUCCESS, " ;  //+ currency.CurrencyAlternateKey;  //CreatedAtAction("GetCurrency", new {currency});  //  new { CurrencyAlternateKey = currency.CurrencyAlternateKey}, currency);
        }

        // DELETE: api/Currency/USD
        [HttpDelete("{id}")]
       // public async Task<IActionResult> DeleteCustomer(string CurrencyAlternateKey)
           public async Task<ActionResult<Currency>> DeleteCustomer(string id)
        {
           // var currency = await _context.Currency.FindAsync(CurrencyAlternateKey);
            var currency = await _context.Currency.FirstOrDefaultAsync(e => e.CurrencyAlternateKey == id);   //(e => e.CurrencyKey == id);
            if (currency == null)
            {
                return NotFound();
            }

            _context.Currency.Remove(currency);
            await _context.SaveChangesAsync();

            return currency;  //NoContent();
        }

        private bool CurrencyExisted(string id)
        {
            return _context.Currency.Any(e => e.CurrencyAlternateKey == id);
        }


    
    }
}
