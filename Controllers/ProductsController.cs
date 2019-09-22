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
        public Product[] GetAllProducts(){ // Action Name does not affect working logic, so have a self-descriptive name
            return FakeData.Products.Values.ToArray();
        }

        [HttpGet("{id}")]
        //[HttpGet("ByID/{id}")]// optional way to make it more self-descriptive, works on https://5001/api/products/byid/0
        public Product Get(int id){
            if(FakeData.Products.ContainsKey(id))
                return FakeData.Products[id];
            else
                return null;
        }

        [HttpGet("from/{low:int}/to/{high:int}")]
        // example: https://5001/api/products/from/0/to/100
        public Product[] Get(int low, int high){
            var products = FakeData.Products.Values.Where(p=>p.Price >= low && p.Price <= high).ToArray();
            return products;
        }

        [HttpPost]
        public Product Post([FromBody] Product product){
            product.ID = FakeData.Products.Keys.Max()+1;
            FakeData.Products.Add(product.ID,product);
            return product;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product product){
            if(FakeData.Products.ContainsKey(id)){
                var target = FakeData.Products[id];
                target.ID = product.ID;
                target.Name = product.Name;
                target.Price = product.Price;
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id){
            if(FakeData.Products.ContainsKey(id)){
                FakeData.Products.Remove(id);
            }
        }
    }
}