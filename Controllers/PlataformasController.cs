using Microsoft.AspNetCore.Mvc;
using RedisDbAPI.Data;
using RedisDbAPI.Models;

namespace RedisDbAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlataformasController : ControllerBase
    {
        private readonly IRedisPlataformaRepo _repo;

        public PlataformasController(IRedisPlataformaRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}", Name = "GetPlataformaPorId")]
        public ActionResult<Plataforma> GetPlataformaPorId(string id)
        {
            var plataforma = _repo.GetPlataformaPorId(id);
            if (plataforma != null)
            {
                return Ok(plataforma);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<Plataforma> CreatePlataforma(Plataforma plataforma)
        {
            _repo.CreatePlataforma(plataforma);

            return CreatedAtRoute(nameof(GetPlataformaPorId), new { Id = plataforma.Id }, plataforma);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Plataforma>> GetTodasPlataformas()
        {
            return Ok(_repo.GetTodasPlataformas());
        }
    }
}