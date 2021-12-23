using System.Linq;
using System.Collections.Generic;  
using DotNetCoreAPI.Models;
using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

namespace DotNetCoreAPI.Repository
{
    public class ProductContext : DbContext
    {
        public List<Product> _prod =
                        new List<Product>()
                            {
                                new Product(){Id = 2, Name = "Donald Trump", IsInventory = false, Description = "Donald Trump's brand"},
                                new Product(){Id = 3, Name = "T-shirt", IsInventory = true, Description = "garment - 1"},
                                new Product(){Id = 4, Name = "Jacket", IsInventory = true, Description = "garment - 2"},
                                new Product(){Id = 5, Name = "Sport Shoe (football)", IsInventory = true, Description = "sport shoes"},
                                new Product(){Id = 6, Name = "Spaot Car", IsInventory = true, Description = ""},
                                new Product(){Id = 7, Name = "Sport Bicycle", IsInventory = true, Description = ""},
                                new Product(){Id = 1, Name = "Jo Bidden", IsInventory = false, Description = "USA President Biden"},
                                new Product(){Id = 8, Name = "Red Hat", IsInventory = true, Description = "the name OS of Linux PC"}
                            }; 
            
           /*  private List<Product> _pInit =
                        new List<Product>()
                            {
                                new Product(){Id = 12, Name = "Donald Trump - 12", IsInventory = false, Description = "Donald Trump's brand"},
                                new Product(){Id = 13, Name = "T-shirt - 13", IsInventory = true, Description = "garment - 1"},
                                new Product(){Id = 14, Name = "Jacket - 14", IsInventory = true, Description = "garment - 2"},
                                new Product(){Id = 15, Name = "Sport Shoe (football)", IsInventory = true, Description = "sport shoes"},
                                new Product(){Id = 16, Name = "Spaot Car - 16", IsInventory = true, Description = ""},
                                new Product(){Id = 17, Name = "Sport Bicycle - 17", IsInventory = true, Description = ""},
                                new Product(){Id = 11, Name = "Jo Biden - 11", IsInventory = false, Description = "USA President Biden"},
                                new Product(){Id = 18, Name = "Red Hat - 18", IsInventory = true, Description = "the name OS of Linux PC"}
                            };   
 */
        private ProductContext _prodContext;
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {     
    
           // _prodContext.Products.Attach(_pInit[0]);
        }

        public DbSet<Product> Products { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)  
         {   
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<Product>().HasData(new Product
            {
                 Id = 500, Name = "Sport Shoe (football)", IsInventory = true, Description = "data in On Model Creating"
            });
             modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 502, Name = "Donald Trump-2", IsInventory = false, Description = "Donald Trump's brand"
                
             });
             modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 503, Name = "T-shirt-3", IsInventory = true, Description = ""
               
            });
             modelBuilder.Entity<Product>().HasData(new Product
            {
                 Id = 504, Name = "Jacket-4", IsInventory = true, Description = ""
           });
             modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 506, Name = "Spaot Car-6", IsInventory = true, Description = ""
               
            });
             modelBuilder.Entity<Product>().HasData(new Product
            {                
                Id = 507, Name = "Sport Bicycle-7", IsInventory = true, Description = ""
               
            });  
         } 

        public async Task<Product> GetProduct(long id){           
            return await  _prodContext.Products.FirstOrDefaultAsync(e => e.Id == id);
        }
        public  async Task<IEnumerable<Product>> GetProducts(){            
            return  await _prodContext.Products.ToListAsync<Product>();
        }
        public async Task<Product>  Add(Product product){
            var result = await _prodContext.Products.AddAsync(product);
            await _prodContext.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<Product>  Update(Product productChanges){        
            var result = await _prodContext.Products.FirstOrDefaultAsync(e => e.Id == productChanges.Id); 
            if (result == null)
            {
                return null; 
            }
            else
            {
                result.Name = productChanges.Name;
                result.IsInventory = productChanges.IsInventory;
                result.Description = productChanges.Description;
                
                await _prodContext.SaveChangesAsync(); 
                return await Task.FromResult(result);
            }
        }
        public async void Delete(long id){        

            var result = await _prodContext.Products.FirstOrDefaultAsync(e => e.Id == id); 
            if (result != null)
            {                
                _prodContext.Products.Remove(result);
                await _prodContext.SaveChangesAsync(); 
            }
        } 
    }
}