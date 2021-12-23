using System.Collections.Generic;  
using DotNetCoreAPI.Models;

public interface IOrderRepository
{    
         Order  GetOrder(long id);
         IEnumerable<Order> GetOrders();
         Order  Add(Order product);
         Order  Update(Order productChanges);
         Order  Delete(long id);
}