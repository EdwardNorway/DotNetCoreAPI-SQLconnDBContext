using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using DotNetCoreAPI.Models;
using DotNetCoreAPI.Repository;

namespace DotNetCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
         private readonly ProductContext _context;  
         public ProductController(ProductContext context)
         { 
             if (context.Products.Count() < 1)
             {            
                foreach (Product _p in context._prod)
                {
                    if (_p != null)
                    context.Products.Add(_p); 
                }
                // adds the Product to the DbSet in memory and commits the changes to the database
                context.SaveChanges();              
             }
             _context = context;  
         }      

        // GET: api/Product
        // https://localhost:5008/api/Product
        [HttpGet]
        public  async Task<IEnumerable<Product>> GetProducts()  
        {   
            var result = _context.Products.ToList<Product>();
            return await Task.FromResult(result); 
        }

        // GET: api/Product/5
        [HttpGet("{id}")]
        public  async Task<ActionResult<Product>> GetProduct(long id)
        {
             var result = await _context.Products.FirstOrDefaultAsync(e => e.Id == id); 
                if (result == null)
                {
                    return NotFound();
                }
                else
                { 
                    return await Task.FromResult(result);
                }           
        }

        // PUT: api/Product/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Product> PutProduct(long id, Product product)
        {
           
             if (id != product.Id)
             {
                  return NotFound("bad request id != product.Id"); 
             }

             _context.Entry(product).State = EntityState.Modified;

             try
             {
                 if (!ProductExists(id))
                 {
                      return NotFound("Not Exist");
                 }

                var result = await _context.Products.FirstOrDefaultAsync(e => e.Id == id); 
                if (result == null)
                {
                    return NotFound("Not Found by using FirstOrDefaultAsync");
                }
                else
                {
                    result.Name = product.Name;
                    result.IsInventory = product.IsInventory;
                    result.Description = product.Description;
                    
                    await _context.SaveChangesAsync(); 
                    return await Task.FromResult(result);
                }
             }
             catch (DbUpdateConcurrencyException ex)
             {
                     return NotFound( ex.Message);
                     throw;             
             }
        }

        // POST: api/Product
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            var result = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return result.Entity;           
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]     
        public async Task<Product> DeleteProduct(long id)
        {
             if (ProductExists(id))
             {
                var result = await _context.Products.FirstOrDefaultAsync(e => e.Id == id); 
                if (result == null)
                {
                    return NotFound("not found by uing FirstOrDefaultAsync");
                }
                else
                {
                    _context.Products.Remove(result);
                    await _context.SaveChangesAsync(); 
                    return await Task.FromResult(result);
                }
             }
             else
             {
                 return NotFound("Not exist");
             }
        }

         private bool ProductExists(long id)
        {
             return _context.Products.Any(e => e.Id == id);
        }
         private Product NotFound(string s)
         {
              Product  _prod = 
                new Product(){Id = -1, Name = "Not Found: msg=" + s, IsInventory = false, Description = "There is no such Product to be found, or bad request or threw exception error. THIS IS A MESSAGE NOTIFICATION"};          
             return _prod;
         }  
    }
}
