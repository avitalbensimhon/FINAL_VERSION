using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using projectSuperMarket.core.Models;
using projectSuperMarket.core.Servies;
using projectSuperMarket.Entities;
using projectSuperMarket.Models;
using projectSuperMarket.Servies.Servies;


namespace projectSuperMarket.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly IGoodsServies _IgoodsServies;
        public GoodsController(IGoodsServies IgoodsServies)
        {
            _IgoodsServies = IgoodsServies;
        }
       
        [AllowAnonymous]
        [HttpGet]

        public ActionResult<List<Goods>> Get() => Ok(_IgoodsServies.GetAll());

        
        [HttpGet("{id}")]
        public ActionResult<Goods> Get(int id)
        {
            Goods item = _IgoodsServies.GetGoodsById(id);
            return item != null ? Ok(item) : NotFound();
        }

       
        [HttpPost]
        public ActionResult Post([FromBody] GoodsPostModelOrders goods)
        {
            var good = new Goods { ProductName = goods.ProductName, PricePerItem = goods.PricePerItem, MinQuantity = goods.MinQuantity,SupplierId=goods.SupplierId
            };
            _IgoodsServies.Add(good);
            return Ok(good);
        }

        
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Goods updatedGoods)
        {
            var item = _IgoodsServies.GetAll().FirstOrDefault(g => g.Id == id);
            if (item == null) return NotFound();
            item.ProductName = updatedGoods.ProductName;
            item.PricePerItem = updatedGoods.PricePerItem;
            item.MinQuantity = updatedGoods.MinQuantity;
            return Ok();
        }

       
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var item = _IgoodsServies.GetAll().FirstOrDefault(g => g.Id == id);
            if (item == null) return NotFound();
            _IgoodsServies.Delete(id);
            return Ok();
        }
    }
}

    

