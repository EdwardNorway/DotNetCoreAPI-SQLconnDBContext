﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCoreAPI.Models
{
    public class DimProductSubcategory
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Display(Name = "Number")]
        [Key] 
        public int ProductSubcategoryKey { get; set; } 
/**

DatabaseGeneratedOption.Identity
You can mark the non-key (non-id) properties as DB-generated properties by using the DatabaseGeneratedOption.Identity option.
 This specifies that the value of the property will be generated by the database on the INSERT statement. This Identity property cannot be updated.

Please note that the way the value of the Identity property will be generated by the database depends on the database provider. 
It can be identity, rowversion or GUID. SQL Server makes an identity column for an integer property.

DatabaseGeneratedOption.Compute
DatabaseGeneratedOption.None

DatabaseGeneratedOption.None option specifies that the value of a property will not be generated by the underlying database. 
This will be useful to override the default convention for the id properties.


CREATE TABLE [dbo].[DimProductSubcategory](
	[ProductSubcategoryKey] [int] IDENTITY(1,1) NOT NULL, <<<<<<< should NOT be included in Class
	[ProductSubcategoryAlternateKey] [int] NULL,
	[EnglishProductSubcategoryName] [nvarchar](50) NOT NULL,
	[SpanishProductSubcategoryName] [nvarchar](50) NOT NULL,
	[FrenchProductSubcategoryName] [nvarchar](50) NOT NULL,
	[ProductCategoryKey] [int] NULL,
 CONSTRAINT [PK_DimProductSubcategory_ProductSubcategoryKey] PRIMARY KEY CLUSTERED 
(
	[ProductSubcategoryKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_DimProductSubcategory_ProductSubcategoryAlternateKey] UNIQUE NONCLUSTERED 
(
	[ProductSubcategoryAlternateKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

***/

       // [Key] 
        public int ProductSubcategoryAlternateKey { get; set; }

        [Required]  //  [StringLength(250, MinimumLength = 0)]
        public string EnglishProductSubcategoryName { get; set; }

         [Required]  // [StringLength(250, MinimumLength = 0)]
        public string SpanishProductSubcategoryName { get; set; }

         [Required]  // [StringLength(250, MinimumLength = 0)]
        public string FrenchProductSubcategoryName { get; set; }
     
       //Foreign key for DimProductCategory
        [ForeignKey("DimProductCategory")]
        public int ProductCategoryKey  { get; set; }

      
        // many-to-one   ????? 2021-12-21 virtual    Data Annotations - NotMapped Attribute
         [NotMapped]
       public  DimProductCategory DimProductCategory { get; set; }

        // one-to-many  virtual
      // public  ICollection<DimProductCategory> DimProductCategory { get; set; }
      //  public virtual ICollection<Instructor> Instructors { get; set; }

      

        /**
        public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Company Company { get; set; }
}
        ***/
        

    }

/**
 public class DimProductSubcategoryView
    {

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[Display(Name = "Number")]
        [Key] 
        public int ProductSubcategoryKey { get; set; } 
        public int ProductSubcategoryAlternateKey { get; set; }

        [StringLength(250, MinimumLength = 0)]
        public string EnglishProductSubcategoryName { get; set; }

        [StringLength(250, MinimumLength = 0)]
        public string SpanishProductSubcategoryName { get; set; }

        [StringLength(250, MinimumLength = 0)]
        public string FrenchProductSubcategoryName { get; set; }
     
        public int ProductCategoryKey  { get; set; }

      
        // many-to-one   ????? 2021-12-21
      // public virtual DimProductCategory DimProductCategory { get; set; }

        // one-to-many  virtual
     //  public  ICollection<DimProductCategory> DimProductCategory { get; set; }
      //  public virtual ICollection<Instructor> Instructors { get; set; }

    }
 ***/



}





        /**
        public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Employee> Employees { get; set; }
}
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Company Company { get; set; }
}



<<<<<<<<<<<<<<<<
public class Blog
{
    public int BlogId { get; set; }
    [Precision(14, 2)]
    public decimal Score { get; set; }
    [Precision(3)]
    public DateTime LastUpdated { get; set; }

      [Unicode(false)]
    [MaxLength(22)]
    public string Isbn { get; set; }

      [Required] // Data annotations needed to configure as required
    public string FirstName { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    [NotMapped]
    public string FullName => $"{FirstName} {LastName}";



}

        ***/