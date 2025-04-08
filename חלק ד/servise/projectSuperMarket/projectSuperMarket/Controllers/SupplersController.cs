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
    public class SupplersController : ControllerBase
    {
        private readonly ISupplersServies _ISupplersServies;

        public SupplersController(ISupplersServies supplersServies)
        {
            _ISupplersServies = supplersServies;
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<Suppliers>> Get()
        {
            return Ok(_ISupplersServies.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Suppliers> Get(int id)
        {
            var supplier = _ISupplersServies.GetById(id);
            if (supplier == null)
                return NotFound();

            return Ok(supplier);
        }

        [HttpPost]
        public IActionResult Post([FromBody] SupplersPostModel supplier)
        {
            var suppliers = new Suppliers { Id = supplier.Id, CompanyName = supplier.CompanyName, PhoneNumber = supplier.PhoneNumber, RepresentativeName = supplier.RepresentativeName } ;
            _ISupplersServies.Add(suppliers);
            return Ok(supplier);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Suppliers supplier)
        {
            if (id != supplier.Id)
                return BadRequest();

            _ISupplersServies.Update(supplier);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _ISupplersServies.Delete(id);
            return Ok();
        }
    }
}
