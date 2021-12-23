using System.Linq;
using System.Collections.Generic;  
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DotNetCoreAPI.Models;


namespace DotNetCoreAPI.DAL
{
    public class CurrencyContext : DbContext
    {
        public List<Currency> _prod =
                        new List<Currency>()
                            {
                                }; 
            
           /*  private List<Currency> _pInit =
                        new List<Currency>()
                            {
                                new Currency(){Id = 12, Name = "Donald Trump - 12", IsInventory = false, Description = "Donald Trump's brand"},
                                new Currency(){Id = 13, Name = "T-shirt - 13", IsInventory = true, Description = "garment - 1"},
                                new Currency(){Id = 14, Name = "Jacket - 14", IsInventory = true, Description = "garment - 2"},
                                new Currency(){Id = 15, Name = "Sport Shoe (football)", IsInventory = true, Description = "sport shoes"},
                                new Currency(){Id = 16, Name = "Spaot Car - 16", IsInventory = true, Description = ""},
                                new Currency(){Id = 17, Name = "Sport Bicycle - 17", IsInventory = true, Description = ""},
                                new Currency(){Id = 11, Name = "Jo Biden - 11", IsInventory = false, Description = "USA President Biden"},
                                new Currency(){Id = 18, Name = "Red Hat - 18", IsInventory = true, Description = "the name OS of Linux PC"}
                            };   
 */
        private CurrencyContext _prodContext;
        public CurrencyContext(DbContextOptions<CurrencyContext> options)
            : base(options)
        {     
           // _prodContext.Currency.Attach(_prod);
           // _prodContext.Currency.Attach(_pInit[0]);  //MyDBContext.MyEntity.Add(mynewObject)
        }

        public DbSet<Currency> Currency { get; set; }

        public DbSet<DimProductCategory> DimProductCategory { get; set; }
        public DbSet<DimProductSubcategory> DimProductSubcategory { get; set; }
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Currency>(
                    eb =>
                    {
                       // eb.HasNoKey();   // also  
                        eb.ToTable("DimCurrency");
                       // eb.Property(v => v.CurrencyKey).HasColumnName("CurrencyKey");  //.HasColumnType("int(11)");
                       eb.Property(v => v.CurrencyAlternateKey).HasColumnName("CurrencyAlternateKey");
                  
                   // .HasDefaultValueSql("'1'");
                       // eb.ValueGeneratedOnAdd();
                    });
/* 
             base.OnModelCreating(modelBuilder);
                var keysProperties = modelBuilder.Model.GetEntityTypes().Select(x => x.FindPrimaryKey()).SelectMany(x => x.Properties);
                foreach (var property in keysProperties)
                {
                    property.ValueGenerated = ValueGenerated.OnAdd;
                } */
                
        }
/***
“how to set unique constraint from EF core” Code Answer
protected override void OnModelCreating(ModelBuilder builder)
{
    builder.Entity<User>()
        .HasIndex(u => u.Email)
        .IsUnique();
}


        protected override void OnModelCreating(ModelBuilder modelBuilder)  
         {   
            base.OnModelCreating(modelBuilder); 
            modelBuilder.Entity<Currency>().HasData(new Currency
            {
                 Id = 500, Name = "Sport Shoe (football)", IsInventory = true, Description = "data in On Model Creating"
            });
             modelBuilder.Entity<Currency>().HasData(new Currency
            {
                Id = 502, Name = "Donald Trump-2", IsInventory = false, Description = "Donald Trump's brand"
                
             });
             modelBuilder.Entity<Currency>().HasData(new Currency
            {
                Id = 503, Name = "T-shirt-3", IsInventory = true, Description = ""
               
            });
             modelBuilder.Entity<Currency>().HasData(new Currency
            {
                 Id = 504, Name = "Jacket-4", IsInventory = true, Description = ""
           });
             modelBuilder.Entity<Currency>().HasData(new Currency
            {
                Id = 506, Name = "Spaot Car-6", IsInventory = true, Description = ""
               
            });
             modelBuilder.Entity<Currency>().HasData(new Currency
            {                
                Id = 507, Name = "Sport Bicycle-7", IsInventory = true, Description = ""
               
            });  
         } 

        public async Task<Currency> GetCurrency(long id){           
            return await  _prodContext.Currency.FirstOrDefaultAsync(e => e.Id == id);
        }
        public  async Task<IEnumerable<Currency>> GetProducts(){            
            return  await _prodContext.Currency.ToListAsync<Currency>();
        }

        **/

     
        public async Task<Currency>  Add(Currency Currency){
            var result = await _prodContext.Currency.AddAsync(Currency);
            await _prodContext.SaveChangesAsync();
            return result.Entity;
        } 
        
          /***
        public async Task<Currency>  Update(Currency productChanges){        
            var result = await _prodContext.Currency.FirstOrDefaultAsync(e => e.Id == productChanges.Id); 
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

            var result = await _prodContext.Currency.FirstOrDefaultAsync(e => e.Id == id); 
            if (result != null)
            {                
                _prodContext.Currency.Remove(result);
                await _prodContext.SaveChangesAsync(); 
            }
        } 
        
           **/
    }

 
}