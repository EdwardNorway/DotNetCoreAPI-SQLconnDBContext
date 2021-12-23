using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


using Microsoft.EntityFrameworkCore;
using DotNetCoreAPI.Models;
using DotNetCoreAPI.DAL;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;


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
            services.AddDbContext<ProductCategoryContext>(opt => 
                                                opt.UseSqlServer(_sqlConnectionStrings));

     throw the below ERROR: 
        InvalidOperationException: Unable to resolve service for type 'DotNetCoreAPI.DAL.ProductCategoryContext' 
        while attempting to activate 'DotNetCoreAPI.Controllers.ProductCategoryController'.

(4). Models folder:    
     class definition for table

(5). Models folder:
     class-Context for the above table

(6). controller folder;
     add a controller-class


*********************************************************/

/***   to use SQLClient ::::

(1) add the below to file .csproj:
    <PackageReference Include="Swashbuckle.AspNetCore" Version="5.6.3" />
    <PackageReference Include="System.Data.SqlClient" Version="4.6.0" />
(2) click "restore" the pop-up box
 Determining projects to restore...
  Restored c:\Users\deng\Downloads\DotNetCoreAPI-SQLconnectionDBContext-Test\DotNetCoreAPI.csproj (in 481 ms).

**/

namespace DotNetCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ProductCategoryContext _context;

        public ProductCategoryController(ProductCategoryContext context)
        {
            _context = context;
        }

        // GET: api/ProductCategory
        // https://localhost:5008/api/ProductCategory
        [HttpGet]
        //  public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategory()
        // BOTH OK OK
        public async Task<IEnumerable<DimProductCategory>> GetProductCategory()
        {
            return await _context.DimProductCategory.ToListAsync();
        }

        // GET: api/DimProductCategory/NOK
        // https://localhost:5008/api/ProductCategory/3
        [HttpGet("{id}")]
       // public async Task<ActionResult<DimProductCategory>> GetDimProductCategory(int currencyKey)
        public async Task<ActionResult<DimProductCategory>> GetProductCategory(int id) //(int id)
        {
            // return await _context.DimProductCategory.FindAsync(currencyKey);  //.ToListAsync();  //
            var result = await _context.DimProductCategory.FirstOrDefaultAsync(e => e.ProductCategoryKey == id);   //(e => e.DimProductCategoryKey == id);
             if (result == null)
            {
                return NotFound();
            } 

            return result;
        }

        // PUT: api/ProductCategorys/2
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        //public async Task<IActionResult> PutCustomer(string id, DimProductCategory currency)
        public async Task<ActionResult<DimProductCategory>> PutProductCategory(int id, DimProductCategory dimProductCategory)
        {
            // should use : productCategoryKey
            if (id != dimProductCategory.ProductCategoryKey)
            {
                return BadRequest();
            }

            _context.Entry(dimProductCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DimProductCategoryExisted(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return dimProductCategory;  //NoContent();
        }


        // POST: api/ProductCategory
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DimProductCategory>> PostProductCategory(DimProductCategory dimProductCategory)
        {
           // dimProductCategory.DimProductCategoryKey = 0;  //_context.DimProductCategory.Max(e => e.DimProductCategoryKey) + 1;
            _context.DimProductCategory.Add(dimProductCategory);
            await _context.SaveChangesAsync();
//string s1 = (dimProductCategory.DimProductCategoryKey).ToString();
            return dimProductCategory;  //"SUCCESS, " ;  //+ currency.DimProductCategoryKey;  //CreatedAtAction("GetDimProductCategory", new {currency});  //  new { DimProductCategoryKey = currency.DimProductCategoryKey}, currency);
        }

        // DELETE: api/ProductCategory/3
        [HttpDelete("{id}")]
       // public async Task<IActionResult> DeleteCustomer(string DimProductCategoryKey)
           public async Task<ActionResult<DimProductCategory>> DeleteProductCategory(int id)
        {
           // var _productCategoryKey //= await GetProductCategory(id); var result 
           //  = await _context.DimProductCategory.FirstOrDefaultAsync(e => e.ProductCategoryKey == id);   //(e => e.DimProductCategoryKey == id);
          
          
// use url to delete ::::

        //ProductSubcategoryController ctrl = new ProductSubcategoryController();
                

           // var dimProductCategory = await _context.DimProductCategory.FindAsync(DimProductCategoryKey);
            var dimProductCategory = await _context.DimProductCategory.FirstOrDefaultAsync(e => e.ProductCategoryKey == id);   //(e => e.DimProductCategoryKey == id);
            if (dimProductCategory == null)
            {
                return NotFound();
            }


         //   Delete(id);

           /*  var contextDelete =  new ProductSubcategoryDeleteContext();
                var _productCategoryKey = contextDelete.DimProductCategoryView.FirstOrDefaultAsync(a => a.ProductCategoryKey == id);
                if (_productCategoryKey != null)
                {  
                    var _productSubcategory = contextDelete.DimProductSubcategoryView.Where(b => EF.Property<int>(b, "ProductCategoryKey") == _productCategoryKey.Result.ProductCategoryKey);
                    foreach (var oneSubcategory in _productSubcategory)
                    {
                        contextDelete.Remove(oneSubcategory);
                    }
                    contextDelete.Remove(_productCategoryKey);
                    contextDelete.SaveChanges();
     
                } */




            _context.DimProductCategory.Remove(dimProductCategory);
            await _context.SaveChangesAsync();

            return dimProductCategory;  //NoContent();
        }

        private bool DimProductCategoryExisted(int id)
        {
            return _context.DimProductCategory.Any(e => e.ProductCategoryKey == id);
        }


// DELETE ::::

/***
(1) add the below to file .csproj:
<PackageReference Include="Swashbuckle.AspNetCore" Version="5.6.3" />
    <PackageReference Include="System.Data.SqlClient" Version="4.6.0" />
(2) click "restore" the pop-up box
 Determining projects to restore...
  Restored c:\Users\deng\Downloads\DotNetCoreAPI-SQLconnectionDBContext-Test\DotNetCoreAPI.csproj (in 481 ms).

**/

  public  void Delete(int productCategoryId)
        {
            /*****************   test code 2021-08-30    ************
            cmd:
            dotnet new console (this is console app for how to connect to SQL)
            dotnet restore
            dotnet run

            **********************************************************/

            try 
            { 
                if (productCategoryId < 1) return;

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

                builder.DataSource = "CHDPC-T480\\SQLEXPRESS";  //"<your_server.database.windows.net>"; 
                builder.UserID = "cs";  //"<your_username>";            
                builder.Password ="Cenium12345!";  //"<your_password>";     
                builder.InitialCatalog = "AdventureWorksDW2012";  //"<your_database>";
         
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    Console.WriteLine("\nQuery data example:");
                    Console.WriteLine("=========================================\n");
                    
                    connection.Open();       

                    String sql = "SELECT name, collation_name FROM sys.databases";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("{0} {1}", reader.GetString(0), reader.GetString(1));
                            }
                        }
                    } 

                    // test 2 - by Deng chunhua :: OK it works
                    Console.WriteLine("============= Should Be Deleted ===============\n");
                  //  sql = "SELECT TOP (1000) * FROM [AdventureWorksDW2012].[dbo].[DimProduct] WHERE ProductCategoryKey = " + productCategoryId;
                   // string s = productCategoryId.ToString();
                    sql = " SELECT * FROM [AdventureWorksDW2012].[dbo].[DimProductSubcategory] WHERE ProductCategoryKey = " + productCategoryId ;
                      using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                               // Console.WriteLine("{0} {1}", reader.GetString(1), reader.GetString(5));
                               Console.WriteLine("{0} {1}", reader.GetInt32(1).ToString(), reader.GetString(2));
                            }
                        }
                    }

                     sql = " DELETE FROM [AdventureWorksDW2012].[dbo].[DimProductSubcategory] WHERE ProductCategoryKey = " + productCategoryId ;
                      using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            /* while (reader.Read())
                            {
                               // Console.WriteLine("{0} {1}", reader.GetString(1), reader.GetString(5));
                               Console.WriteLine("{0} {1}", reader.GetInt32(1).ToString(), reader.GetString(2));
                            } */
                        }
                    }

               /*  ',[ProductAlternateKey]' +
                ',[ProductSubcategoryKey]' +
                ',[WeightUnitMeasureCode]' +
                ',[SizeUnitMeasureCode]' +
                ',[EnglishProductName]' +
               // ',[SpanishProductName]' +
               // ',[FrenchProductName]' +
                ',[StandardCost]' +
                ',[FinishedGoodsFlag]' +
                ',[Color]' +
                ',[SafetyStockLevel]' +
                ',[ReorderPoint]' +
                ',[ListPrice]' +
                ',[Size]' +
                ',[SizeRange]' +
                ',[Weight]' +                
                ',[ProductLine]' +
                ',[DealerPrice]' +
                ',[Class]' +
                ',[Style]' +
                ',[ModelName]' +
               // ',[LargePhoto]' +
                ',[EnglishDescription]' +                
                ',[StartDate]' +
                ',[EndDate]' +
                ',[Status]' + */
               
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nDone. >> reach the END");
            Console.WriteLine("=========================================\n");
           // Console.ReadLine(); 
        
               /** end 2021-08-30*/   

          //  CreateHostBuilder(args).Build().Run();
        }



// DELETE ::::

public class ProductSubcategoryDeleteContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("server=CHDPC-T480\\SQLEXPRESS; Database=AdventureWorksDW2012; User ID=cs; password=Cenium12345!;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DimProductSubcategoryView>()
                               // .HasNoKey()
                                .HasOne<DimProductCategoryView>(e => e.DimProductCategoryView)
                                .WithMany(c => c.DimProductSubcategoryView)   //
                                .HasForeignKey("ProductCategoryKey");
    }

    public DbSet<DimProductSubcategoryView> DimProductSubcategoryView { get; set; }
    public DbSet<DimProductCategoryView> DimProductCategoryView { get; set; }
}

  public class DimProductCategoryView
    {

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          [Key] 
       // [Display(Name = "ProductCategoryKey")]
         public int ProductCategoryKey { get; set; }
          
        public int ProductCategoryAlternateKey { get; set; }

       
        public string EnglishProductCategoryName { get; set; }

     
        public string SpanishProductCategoryName { get; set; }

  
        public string FrenchProductCategoryName { get; set; }
   
       // public virtual DimProductSubcategory DimProductSubcategory { get; set; }

       
        // one-to-many ::   virtual  [NotMapped]
        //[NotMapped]
         public  ICollection<DimProductSubcategoryView> DimProductSubcategoryView { get; set; }
   
    }

    public class DimProductSubcategoryView
    {  
         [Key]   
        public int  ProductSubcategoryKey { get; set; }  
        public int ProductSubcategoryAlternateKey { get; set; }

        //  [StringLength(250, MinimumLength = 0)]
        public string EnglishProductSubcategoryName { get; set; }

         // [StringLength(250, MinimumLength = 0)]
        public string SpanishProductSubcategoryName { get; set; }

        // [StringLength(250, MinimumLength = 0)]
        public string FrenchProductSubcategoryName { get; set; }
     
       //Foreign key for DimProductCategory
       
        public int ProductCategoryKey  { get; set; }

      
        // many-to-one   ????? 2021-12-21 virtual    Data Annotations - NotMapped Attribute
      
       public  DimProductCategoryView DimProductCategoryView { get; set; }
    }

    
    }
}
