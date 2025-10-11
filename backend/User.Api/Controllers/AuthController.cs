using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = new[]
            {
                new { Id = 1, first_name = "João", last_name = "Pedro", Age = 18 },
                new { Id = 2, first_name = "Maria",last_name = "Julia", Age = 18 }
            };
            return Ok(users);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var user = new { Id = id, Name = $"João, ID: {id}", Age = 18 };
            return Ok(user);
        }
    }
}