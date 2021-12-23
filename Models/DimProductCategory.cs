
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCoreAPI.Models
{
    public class DimProductCategory
    {

/***
CREATE TABLE [dbo].[DimProductCategory](
	[ProductCategoryKey] [int] IDENTITY(1,1) NOT NULL,   <<<<<<<<<  NOT included in Class (auto assigned and auto increase)
	[ProductCategoryAlternateKey] [int] NULL,
	[EnglishProductCategoryName] [nvarchar](50) NOT NULL,
	[SpanishProductCategoryName] [nvarchar](50) NOT NULL,
	[FrenchProductCategoryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_DimProductCategory_ProductCategoryKey] PRIMARY KEY CLUSTERED 
(
	[ProductCategoryKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_DimProductCategory_ProductCategoryAlternateKey] UNIQUE NONCLUSTERED 
(
	[ProductCategoryAlternateKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
**/
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
           [Key] 
       // [Display(Name = "ProductCategoryKey")]
         public int ProductCategoryKey { get; set; }
        //       [Key] 
        public int ProductCategoryAlternateKey { get; set; }

        [StringLength(250, MinimumLength = 0)]
        public string EnglishProductCategoryName { get; set; }

        [StringLength(250, MinimumLength = 0)]
        public string SpanishProductCategoryName { get; set; }

        [StringLength(250, MinimumLength = 0)]
        public string FrenchProductCategoryName { get; set; }
   



       // public virtual DimProductSubcategory DimProductSubcategory { get; set; }

       
        // one-to-many ::   virtual  [NotMapped]
        [NotMapped]
         public  ICollection<DimProductSubcategory> DimProductSubcategory { get; set; }
        // public virtual ICollection<Instructor> Instructors { get; set; }


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

    }
}
