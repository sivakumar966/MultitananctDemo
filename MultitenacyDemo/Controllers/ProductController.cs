using Microsoft.AspNetCore.Mvc;
using MultitenacyDemo.Domains;

namespace MultitenacyDemo.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly AppDbContext dbContext;

    public ProductController(AppDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(dbContext.Products.ToList());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        return Ok(dbContext.Products.FirstOrDefault(x => x.Id == id));
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        dbContext.Products.Add(product);
        dbContext.SaveChanges();
        return Ok(product);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        dbContext.Products.Remove(dbContext.Products.FirstOrDefault(x => x.Id == id));
        dbContext.SaveChanges();
        return NoContent();
    }
}
