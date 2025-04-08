using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectSuperMarket.core.Servies;
using projectSuperMarket.Entities;
using projectSuperMarket.Models;


namespace projectSuperMarket.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderServies _IorderServies;
        public OrdersController(IOrderServies orderServies)
        {
           _IorderServies = orderServies;
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<Orders>> Get() => Ok(_IorderServies.GetAll());

        [HttpGet("{id}")]
        public ActionResult<Orders> Get(int id)
        {
            var order = _IorderServies.GetById(id);
            return order != null ? Ok(order) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] OrderPostModel model)
        {
            try
            {
                await _IorderServies.AddOrderAsync(model);
                return Ok("הזמנה נוספה בהצלחה");
            }
            catch (Exception ex)
            {
                return BadRequest($"שגיאה: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Orders updatedOrder)
        {
            updatedOrder.Id = id;
            _IorderServies.Update(updatedOrder);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _IorderServies.Delete(id);
            return Ok();
        }
        [HttpPut("updateStatus/{id}")]
        public IActionResult UpdateStatus(int id, [FromBody] int newStatusValue)
        {
            if (!Enum.IsDefined(typeof(StatusOrder), newStatusValue))
                return BadRequest("סטטוס לא חוקי");

            var status = (StatusOrder)newStatusValue;
            _IorderServies.UpdateStatus(id, status);
            return Ok($"הסטטוס עודכן ל: {status}");
        }
    }
}
