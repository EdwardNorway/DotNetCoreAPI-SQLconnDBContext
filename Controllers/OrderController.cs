using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using DotNetCoreAPI.Models;
using DotNetCoreAPI.Repository;

namespace DotNetCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
          private readonly OrderRepository _contextOrder;
         public OrderController(OrderRepository context)
         {           
             _contextOrder = context;  
         } 

        // GET: api/Order
        // https://localhost:5008/api/Order
        [HttpGet]
        public  async Task<IEnumerable<Order>> GetOrders()  
        {
             var result = _contextOrder.GetOrders();
             return await Task.FromResult(result.ToList()); 
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public  async Task<ActionResult<Order>> GetOrder(long id)
        {
            var result = _contextOrder.GetOrder(id);           
            if (result == null)
            {
                return NotFound("can not found");
            }
            return await Task.FromResult(result);           
        }

        // PUT: api/Order/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Order> PutOrder(long id, Order product)
        {           
             if (id != product.Id)
             {
                  return NotFound("Bad request, id != product.id");
             }
            var result =  _contextOrder.Update(product);
            return await Task.FromResult(result);                
        }

        // POST: api/Order
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order product)
        {
            var result = _contextOrder.Add(product);
            return await Task.FromResult(result);
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]     
        public async Task<Order> DeleteOrder(long id)
        {
             if (ProductExists(id))
             {
                var result =  _contextOrder.Delete(id);
                return await Task.FromResult(result);
             }
             else
             {
                 return NotFound("Not exist");
             }
        }

         private bool ProductExists(long id)
        {
             return _contextOrder.GetOrders().Any(e => e.Id == id);
        }
         private Order NotFound(string s)
         {
              Order  _prod = 
                new Order(){Id = -1, Name = "Not Found msg=" + s, IsInventory = false, Description = "There is no such Order to be found, or bad request or threw exception error. THIS IS A MESSAGE NOTIFICATION"};          
             return _prod;
         }
    }
}

