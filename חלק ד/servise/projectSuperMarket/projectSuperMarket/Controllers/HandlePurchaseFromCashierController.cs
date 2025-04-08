using Microsoft.AspNetCore.Mvc;
using projectSuperMarket.Entities;
using projectSuperMarket.Servies.Servies;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace projectSuperMarket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HandlePurchaseFromCashierController : ControllerBase
    {
        public readonly AutoOrderService _autoOrderService;
        public HandlePurchaseFromCashierController(AutoOrderService autoOrderService)
        {
            _autoOrderService = autoOrderService;
        }
      


        
        [HttpPost]
        public async Task<IActionResult> HandlePurchaseFromCashier([FromBody] Dictionary<string, int> soldItems)
        {
            var result = await _autoOrderService.CheckAndOrderIfNeeded(soldItems);

            var resultType = result.GetType();
            var ordersProp = resultType.GetProperty("orders");
            var messagesProp = resultType.GetProperty("messages");

            var orders = ordersProp?.GetValue(result) as List<Orders>;
            var messages = messagesProp?.GetValue(result) as List<string>;

            if ((orders == null || !orders.Any()) && (messages == null || !messages.Any()))
                return NotFound("לא בוצעה פעולה עבור הנתונים שנשלחו.");

            return Ok(new { orders = orders, messages = messages });
        }


       
    }
}
