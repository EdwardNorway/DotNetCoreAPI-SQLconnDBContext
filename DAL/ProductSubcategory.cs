using System.Linq;
using System.Collections.Generic;  
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using DotNetCoreAPI.Models;
//using Microsoft.EntityFrameworkCore.Relational;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace DotNetCoreAPI.DAL
{
    public class ProductSubcategoryContext : DbContext
    {
        
  /*       public List<Currency> _prod =
                        new List<Currency>()
                            {
                                };  */
            
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
        private ProductSubcategoryContext _prodContext;
        public ProductSubcategoryContext(DbContextOptions<ProductSubcategoryContext> options)
            : base(options)
        {  
            // Configuration.ProxyCreationEnabled = true;
             //Configuration.LazyLoadingEnabled = true;   
           // _prodContext.Currency.Attach(_prod);
           // _prodContext.Currency.Attach(_pInit[0]);  //MyDBContext.MyEntity.Add(mynewObject)
        }

      //  public DbSet<Currency> Currency { get; set; }

        public DbSet<DimProductSubcategory> DimProductSubcategory { get; set; }
       // public DbSet<DimProductSubcategory> DimProductSubcategory { get; set; }
       protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<DimProductSubcategory>(
                    eb =>
                    {
                       // eb.HasKey("ProductSubcategoryAlternateKey");   // also  
                        eb.ToTable("DimProductSubcategory");
                       // eb.Property(v => v.CurrencyKey).HasColumnName("CurrencyKey");  //.HasColumnType("int(11)");
                                                                  
                        eb.Property(v => v.ProductSubcategoryAlternateKey).HasColumnName("ProductSubcategoryAlternateKey");
                        
                   // .HasDefaultValueSql("'1'");
                       // eb.ValueGeneratedOnAdd();
                    });
                     //Configure FK for one-to-many relationship

                     modelBuilder.Entity<DimProductSubcategory>()
                                .HasOne<DimProductCategory>(e => e.DimProductCategory)
                                .WithMany(c => c.DimProductSubcategory)   //
                                .HasForeignKey("ProductCategoryKey");   //(p => p.ProductCategoryKey);
                                //.OnDelete(DeleteBehavior.SetNull); // sets the foreign key value of the dependent entity to null in the event that the principal is deleted
                                //.OnDelete(DeleteBehavior.Delete); // result in the dependent entity being deleted:
                   /*
Configuring One To Many Relationships in Entity Framework Core (ompany/Employees)                   
https://www.learnentityframeworkcore.com/configuration/one-to-many-relationship-configuration

Relationships (Blob posts)
https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key

Configure One-to-Many Relationships using Fluent API in Entity Framework Core  (Grade/Students)
https://www.entityframeworktutorial.net/efcore/configure-one-to-many-relationship-using-fluent-api-in-ef-core.aspx

Entity Properties
https://docs.microsoft.com/en-us/ef/core/modeling/entity-properties?tabs=data-annotations%2Cwithout-nrt


 protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        //Configure primary key
        modelBuilder.Entity<Student>().HasKey<int>(s => s.StudentKey);
        modelBuilder.Entity<Standard>().HasKey<int>(s => s.StandardKey);

        //Configure composite primary key
        modelBuilder.Entity<Student>().HasKey<int>(s => new { s.StudentKey, s.StudentName }); 
    }


                         modelBuilder.Entity<DimProductSubcategory>()//.HasMany("ProductCategoryKey");
                        .HasMany(<int>)
                        .HasForeignKey("ProductCategoryKey")
                        .IsRequired();

                 modelBuilder.Entity<DimProductSubcategory>()
                    .HasMany(m => m.ProductCategoryKey)
                    //  .HasForeignKey(m => m.ProductCategoryKey);
                    .HasRequired<DimProductCategory>(s ⇒ s.Student)
                    .WithMany(t ⇒ t.Enrollments)
                    .HasForeignKey(u ⇒ u.StdntID); */
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

     
        public async Task<DimProductSubcategory>  Add(DimProductSubcategory DimProductSubcategory){
            var result = await _prodContext.DimProductSubcategory.AddAsync(DimProductSubcategory);
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