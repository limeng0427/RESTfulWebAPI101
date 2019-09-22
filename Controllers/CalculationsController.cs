using Microsoft.AspNetCore.Mvc;
using WebServer.Models;

namespace WebServer.Controllers
{
    // this controller is used to demonstrate the action parameter data binding
    // [FromQuery],[FromRoute],[FromForm],[FromBody]
    // [FromRoute] is optional

    [Route("api/[controller]")]
    public class CalculationsController: Controller{ // class has to be public to make it accessable
        [HttpGet("add")]
        // example: https://5001/api/calculations/add?a=100&b=200
        public double Add([FromQuery]double a, [FromQuery]double b){
            return a + b;
        }

        [HttpGet("sub/a/{a}/b/{b}")]
        // example: https://5001/api/calculations/a/200/b/100
        public double Sub([FromRoute]double a,[FromRoute]double b){
            return a - b;
        }

        [HttpPost("mul")]
        // example: https://5001/api/calculations/mul
        // body: raw->json->{"a":100,"b":5}
        public double Sub([FromBody] CalculationParameter calculationParameter){
            return calculationParameter.A * calculationParameter.B;
        }

        [HttpPost("div")]
        // example: https://5001/api/calculations/div
        // body: HTML <form> element
        public double Div([FromForm] CalculationParameter calculationParameter){
            return calculationParameter.A / calculationParameter.B;
        }
    }
}