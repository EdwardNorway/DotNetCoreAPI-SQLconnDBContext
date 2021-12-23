using System.Linq;
using System.Collections.Generic;  
using DotNetCoreAPI.Models;

namespace DotNetCoreAPI.Repository
{
    public class OrderRepository  : IOrderRepository
    {
        private List<Order> _products;

           public OrderRepository()          
         {
             _products = new List<Order>()
             {
                 new Order(){Id = 2, Name = "Donald Trump", IsInventory = false, Description = "Donald Trump's brand"},
                 new Order(){Id = 3, Name = "T-shirt", IsInventory = true, Description = ""},
                 new Order(){Id = 4, Name = "Jacket", IsInventory = true, Description = ""},
                 new Order(){Id = 5, Name = "Sport Shoe (football)", IsInventory = true, Description = ""},
                 new Order(){Id = 6, Name = "Spaot Car", IsInventory = true, Description = ""},
                 new Order(){Id = 7, Name = "Sport Bicycle", IsInventory = true, Description = ""},
                 new Order(){Id = 1, Name = "Jo Bidden", IsInventory = false, Description = "USA President Biden"},
                 new Order(){Id = 8, Name = "Red Hat", IsInventory = true, Description = "the name OS of Linux PC"}
             };
         }
 
        public Order  GetOrder(long id){           
            return _products.FirstOrDefault(e => e.Id == id);
        }
        public  IEnumerable<Order> GetOrders(){            
            return  _products;
        }
        public Order  Add(Order product){
            product.Id = _products.Max(e => e.Id) + 1;
            _products.Add(product);
            return product;
        }
        public Order  Update(Order productChanges){           
            var result =  _products.FirstOrDefault(e => e.Id == productChanges.Id); 
            if (result == null)
            {
                return null; 
            }
            else
            {
                result.Name = productChanges.Name;
                result.IsInventory = productChanges.IsInventory;
                result.Description = productChanges.Description;                    
                return (result);
            }
        }
        public Order  Delete(long id){           
             Order prod = new Order();
             prod = GetOrder(id);
             _products.Remove(prod);          
            return prod;
        }

         private bool ProductExists(long id) {
             return _products.Any(e => e.Id == id);
        } 
    }
}