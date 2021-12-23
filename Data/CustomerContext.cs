using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DotNetCoreAPI.Models;

    public class CustomerContext : DbContext
    {
        public CustomerContext (DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }

        public DbSet<DotNetCoreAPI.Models.Order> Order { get; set; }
    }
