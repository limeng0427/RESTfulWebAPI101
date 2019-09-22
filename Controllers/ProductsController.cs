using Microsoft.AspNetCore.Mvc;
using WebServer.Models;
using System.Linq;

namespace WebServer.Controllers
{
    // could use following Control Route to hide the api meaning from public
    // [Route("api/a3aaae2f-0795-46b4-b74d-7f044557e783")]

    // Flexible Web API Update with [PlaceHolder]
    [Route("api/[controller]")] // Controller Route
    public class ProductsController: Controller
    {
        [HttpGet] // Action Route
        //[Route("all")] // optional Action Route which only works on https://5001/api/products/all
        //[HttpGet("all")]
        public ActionResult GetAllProducts(){ // Action Name does not affect working logic, so have a self-descriptive name
            if(FakeData.Products != null){
                return Ok(FakeData.Products.Values.ToArray());//200 return meaningfull status code
            } else {
                return NotFound();//404
            }
        }

        [HttpGet("{id}")]
        //[HttpGet("ByID/{id}")]// optional way to make it more self-descriptive, works on https://5001/api/products/byid/0
        public ActionResult Get(int id){
            if(FakeData.Products.ContainsKey(id))
                return Ok(FakeData.Products[id]);
            else
                return NotFound();
        }

        [HttpGet("from/{low:int}/to/{high:int}")]
        // example: https://5001/api/products/from/0/to/100
        public ActionResult Get(int low, int high){
            var products = FakeData.Products.Values.Where(p=>p.Price >= low && p.Price <= high).ToArray();
            if(products.Any())
                return Ok(products);
            else
                return NotFound();
        }

        [HttpPost]
        public ActionResult Post([FromBody] Product product){
            product.ID = FakeData.Products.Keys.Max()+1;
            FakeData.Products.Add(product.ID,product);
            return Created($"api/products/{product.ID}",product);//url+new object 201
            //Hypermedia as the engine of application state HATEOAS
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Product product){
            if(FakeData.Products.ContainsKey(id)){
                var target = FakeData.Products[id];
                target.ID = product.ID;
                target.Name = product.Name;
                target.Price = product.Price;
                return Ok();
            } else{
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id){
            if(FakeData.Products.ContainsKey(id)){
                FakeData.Products.Remove(id);
                return Ok();
            } else{
                return NotFound();
            }
        }
    }
}