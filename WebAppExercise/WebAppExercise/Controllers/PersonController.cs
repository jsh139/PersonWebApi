using Microsoft.AspNetCore.Mvc;
using WebAppExercise.Models;
using WebAppExercise.Security;
using WebAppExercise.Services;

namespace WebAppExercise.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;

        public PersonController(ILogger<PersonController> logger, IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpGet, Produces(typeof(List<Person>))]
        public async Task<IActionResult> Get()
        {
            var includePII = HttpContext.User.HasClaim(SecurityConstants.ClaimTypeIncludePII, string.Empty);

            return Ok(await _personService.GetPeople(includePII));
        }

        [HttpGet("{id}"), Produces(typeof(Person))]
        public async Task<IActionResult> Get(long id)
        {
            var includePII = HttpContext.User.HasClaim(SecurityConstants.ClaimTypeIncludePII, string.Empty);

            var person = await _personService.GetPerson(id, includePII);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }
    }
}