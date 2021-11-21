using BloggingWebAPI.Models;
using BloggingWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloggingWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        // GET: api/<TodoController>
        [HttpGet]
        public async Task<string> Get()
        {
            return System.Text.Json.JsonSerializer.Serialize(await CosmosDBService.Get<ToDo>());
        }

        // GET api/<TodoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TodoController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TodoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TodoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

