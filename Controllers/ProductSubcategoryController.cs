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
// SUCCESS :: OK OK 2021-12-21

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
            services.AddDbContext<ProductSubcategoryContext>(opt => 
                                                opt.UseSqlServer(_sqlConnectionStrings));

     throw the below ERROR: 
        InvalidOperationException: Unable to resolve service for type 'DotNetCoreAPI.DAL.ProductSubcategoryContext' 
        while attempting to activate 'DotNetCoreAPI.Controllers.ProductSubcategoryController'.
        
(4). Models folder:    
     class definition for table

(5). Models folder:
     class-Context for the above table

(6). controller folder;
     add a controller-class


*********************************************************/

namespace DotNetCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSubcategoryController : ControllerBase
    {
        private readonly ProductSubcategoryContext _context;

        public ProductSubcategoryController(ProductSubcategoryContext context)
        {
            _context = context;
        }

        // GET: api/ProductSubcategory
        // https://localhost:5008/api/ProductSubcategory
        [HttpGet]
        //  public async Task<ActionResult<IEnumerable<ProductSubcategory>>> GetProductSubcategory()
        // BOTH OK OK

        public async Task<IEnumerable<DimProductSubcategory>> GetProductSubcategory()
        {
             return await _context.DimProductSubcategory.ToListAsync();
      
        }
/* 
        public async Task<IEnumerable<DimProductSubcategoryView>> GetProductSubcategory()
        {
            // return await _context.DimProductSubcategory.ToListAsync();

           List<DimProductSubcategoryView> res = new List<DimProductSubcategoryView>();
          await foreach (var item in  _context.DimProductSubcategory)
           {
               DimProductSubcategoryView tmp = new DimProductSubcategoryView();
               tmp.EnglishProductSubcategoryName = item.EnglishProductSubcategoryName;
               tmp.FrenchProductSubcategoryName = item.FrenchProductSubcategoryName;
               tmp.SpanishProductSubcategoryName = item.SpanishProductSubcategoryName;
               tmp.ProductCategoryKey = item.ProductCategoryKey;
               tmp.ProductSubcategoryAlternateKey = item.ProductSubcategoryAlternateKey;
               tmp.ProductSubcategoryKey = item.ProductSubcategoryKey;
                res.Add(tmp);
           }
            return res.ToList();  //await _context.DimProductSubcategory.ToListAsync();

        } */

        // GET: api/DimProductSubcategory/NOK
        // https://localhost:5008/api/ProductSubcategory/8
        [HttpGet("{id}")]
       // public async Task<ActionResult<DimProductSubcategory>> GetDimProductSubcategory(int currencyKey)
        public async Task<ActionResult<DimProductSubcategory>> GetProductSubcategory(int id) //(int id)
        {
            // return await _context.DimProductSubcategory.FindAsync(currencyKey);  //.ToListAsync();  //
            var result = await _context.DimProductSubcategory.FirstOrDefaultAsync(e => e.ProductSubcategoryAlternateKey == id);   //(e => e.DimProductSubcategoryKey == id);
             if (result == null)
            {
                return NotFound();
            } 

            return result;
        }

        // PUT: api/DimProductSubcategorys/DKK
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //public async Task<IActionResult> PutCustomer(string id, DimProductSubcategory currency)
        public async Task<ActionResult<DimProductSubcategory>> PutProductSubcategory(int id, DimProductSubcategory dimProductSubcategory)
        {
            if (id != dimProductSubcategory.ProductSubcategoryAlternateKey)
            {
                return BadRequest();
            }

            _context.Entry(dimProductSubcategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DimProductSubcategoryExisted(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return dimProductSubcategory;  //NoContent();
        }


        // POST: api/ProductSubcategory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DimProductSubcategory>> PostProductSubcategory(DimProductSubcategory dimProductSubcategory)
        {
           // dimProductSubcategory.DimProductSubcategoryKey = 0;  //_context.DimProductSubcategory.Max(e => e.DimProductSubcategoryKey) + 1;
            _context.DimProductSubcategory.Add(dimProductSubcategory);
            await _context.SaveChangesAsync();
//string s1 = (dimProductSubcategory.DimProductSubcategoryAlternateKey).ToString();
            return dimProductSubcategory;  //"SUCCESS, " ;  //+ currency.DimProductSubcategoryAlternateKey;  //CreatedAtAction("GetDimProductSubcategory", new {currency});  //  new { DimProductSubcategoryAlternateKey = currency.DimProductSubcategoryAlternateKey}, currency);
        }

        // DELETE: api/ProductSubcategory/9
        [HttpDelete("{id}")]
       // public async Task<IActionResult> DeleteCustomer(string DimProductSubcategoryAlternateKey)
           public async Task<ActionResult<DimProductSubcategory>> DeleteProductSubcategory(int id)
        {
           // var dimProductSubcategory = await _context.DimProductSubcategory.FindAsync(DimProductSubcategoryAlternateKey);
            var dimProductSubcategory = await _context.DimProductSubcategory.FirstOrDefaultAsync(e => e.ProductSubcategoryAlternateKey == id);   //(e => e.DimProductSubcategoryKey == id);
            if (dimProductSubcategory == null)
            {
                return NotFound();
            }

            _context.DimProductSubcategory.Remove(dimProductSubcategory);
            await _context.SaveChangesAsync();

            return dimProductSubcategory;  //NoContent();
        }

        private bool DimProductSubcategoryExisted(int id)
        {
            return _context.DimProductSubcategory.Any(e => e.ProductSubcategoryAlternateKey == id);
        }


    
    }
}
