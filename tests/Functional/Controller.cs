using System.Net.Http.Json.Models;
using Microsoft.AspNetCore.Mvc;

namespace System.Net.Http.Json.Functional
{
    [Controller]
    [Route("json-formatter")]
    public class SystemTextJsonFormatterController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] SimpleType model)
        {
            return Ok(model);
        }

        [HttpPut]
        public IActionResult Put([FromBody] SimpleType model)
        {
            return Ok(model);
        }
    }
}