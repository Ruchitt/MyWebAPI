using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private static List<SuperHero> list = new List<SuperHero>
        {
            new SuperHero{ Id = 1,Name="SpiderMan",Place="New York"}
        };

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Get(int id)
        {
            var hero = list.Find(x => x.Id == id);
            if (hero == null)
                return BadRequest("Hero not Found");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> Post(SuperHero hero)
        {
            list.Add(hero);
            return Ok(list);
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero obj)
        {
            var hero = list.Find(x => x.Id == obj.Id);
            if (hero == null)
                return BadRequest("Hero not Found");
            hero.Name= obj.Name;
            hero.Place=obj.Place;
            return Ok(list);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var hero = list.Find(x => x.Id == id);
            if (hero == null)
                return BadRequest("Hero not Found");
            list.Remove(hero);
            return Ok(list);
        }
    }
}
