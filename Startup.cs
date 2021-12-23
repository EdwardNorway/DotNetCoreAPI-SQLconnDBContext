using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

using DotNetCoreAPI.Repository;
using DotNetCoreAPI.Models;
using DotNetCoreAPI.DAL;

namespace DotNetCoreAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(); 
            services.AddTransient<OrderRepository>(); 
            services.AddDbContext<ProductContext>(opt => 
                                               opt.UseInMemoryDatabase("ProductList"));   // opt.UseSqlServer()
            services.AddDbContext<CustomerContext>(options =>
                    options.UseInMemoryDatabase("OrderContext")); 

// SUCCESS :: OK OK 2021-09-02
            string _sqlConnectionStrings = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CurrencyContext>(opt => 
                                                opt.UseSqlServer(_sqlConnectionStrings)); 
                                                  //
// ADDed the below two stataments on 2021-12-21 ::
            services.AddDbContext<ProductCategoryContext>(opt => 
                                                opt.UseSqlServer(_sqlConnectionStrings));
            services.AddDbContext<ProductSubcategoryContext>(opt => 
                                                opt.UseSqlServer(_sqlConnectionStrings));
            services.AddDbContext<BookCategoryContext>(opt => 
                                                opt.UseSqlServer(_sqlConnectionStrings));
            services.AddDbContext<BookContext>(opt => 
                                                opt.UseSqlServer(_sqlConnectionStrings));
            services.AddDbContext<CategoryContext>(opt => 
                                                opt.UseSqlServer(_sqlConnectionStrings));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
