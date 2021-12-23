using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotNetCoreAPI.Models
{    public class Currency
    {
       
      /*   [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CurrencyKey { get; set; } 
        
        CREATE TABLE [dbo].[DimCurrency](
	[CurrencyKey] [int] IDENTITY(1,1) NOT NULL,  <<<<<<<<<<<<<  NOT included in Class it will be AUTO assigned value and AUTO increased value 
	[CurrencyAlternateKey] [nchar](3) NOT NULL,
	[CurrencyName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_DimCurrency_CurrencyKey] PRIMARY KEY CLUSTERED 
(
	[CurrencyKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

        */

          [Key]  
        public string  CurrencyAlternateKey { get; set; }

    
        public string CurrencyName { get; set; }
    }
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