using DenemeCoreAPI.Data;
using DenemeCoreAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DenemeCoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public ProductsController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult GetProducts()
        {
            return Ok(_db.Products.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_db.Products.FirstOrDefault(i => i.Id == id));
            //return Ok(_db.Products.Find(id));
        }

        [HttpPost]
        public IActionResult Create(Product model)
        {
            _db.Add(model);
            _db.SaveChanges();
            return Created("",model);
        }

        [HttpPut("{id}")]
        public IActionResult Update(/*[FromRoute]*/int id,/*[FromBody]*/ Product model)
        {
            var updateProduct = _db.Products.FirstOrDefault(i => i.Id == id);
            updateProduct.Name = model.Name;
            _db.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletedProduct = _db.Products.FirstOrDefault(i => i.Id == id);
            _db.Remove(deletedProduct);
            _db.SaveChanges();
            return NoContent();
        }
    }
}
