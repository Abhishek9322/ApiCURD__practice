using ApiCURD__practice.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCURD__practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context; 
        public ProductController(AppDbContext context) 
        {
          _context = context;   
        
        }
        //Get Api OR Read API
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            return product;

        } 
        //Create Api
        [HttpPost]
        public async Task<ActionResult<Product>> Create (Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }


        //update Api

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,Product product)
        {
            if(id!=product.Id) return BadRequest();
 
           _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();

        }

        //Delete Api
        [HttpDelete("{id}")]
        public  async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
