using DotNet8WebApi.Model;
using DotNet8WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hangfire;

namespace DotNet8WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OurHeroController : ControllerBase
    {
        private readonly IOurHeroService _ourHeroService;
        private readonly IConfiguration _configuration;
        public OurHeroController(IOurHeroService ourHeroService, IConfiguration configuration)
        {
            _ourHeroService = ourHeroService;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] bool? isActive = null)
        {
            return Ok(_ourHeroService.GetAllHeros(isActive));
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var hero = _ourHeroService.GetHeroById(id);
            if (hero == null) 
                return NotFound();
            return Ok(hero);
        }

        [HttpPost]
        public async Task<IActionResult> Post(AddUpdateOurHero addUpdateOurHero)
        {
            var hero = _ourHeroService.AddHero(addUpdateOurHero);
            if (hero == null)
                return BadRequest();

            var jobId = BackgroundJob.Schedule(() => _ourHeroService.AddHeroByJob(), TimeSpan.FromMinutes(1));

            return Ok(new { message = "Super Hero Created Successfully.", id = hero!.Id });
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] AddUpdateOurHero addUpdateOurHero)
        {
            var hero = _ourHeroService.UpdateHero(id, addUpdateOurHero);
            if (hero == null)
                return BadRequest();

            RecurringJob.AddOrUpdate(static () => Console.WriteLine("Sent similar product offer and suuggestions"), Cron.Daily);

            return Ok(new
            {
                message = "Super Hero Updated Successfully!!!",
                id = hero!.Id,
            });
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!_ourHeroService.DeleteHero(id))
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Super Hero Deleted Successfully!!!",
                id = id
            });
        }
    }
}
