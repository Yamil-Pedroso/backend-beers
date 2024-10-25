using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        // When is something private for convention insert underscore
        private IPeopleService _peopleService;

        public PeopleController([FromKeyedServices("people2Service")]IPeopleService peopleService)
        {
            //_peopleService = new PeopleService(); This works but if in the future a parameter change, we have then to implement all the changes in any Class thas use this concrete Class
            //then is best practice do it as the example below

            _peopleService = peopleService;
        }

        [HttpGet("all")]
        public List<People> GetPeople() => Repository.People;

        [HttpGet("{id}")]
        public ActionResult<People> Get(int id)
        {
            //Repository.People.First(x => x.Id == id);
           var people = Repository.People.FirstOrDefault(p => p.Id == id);

            if (people == null)
            {
                return NotFound();
            }

            return Ok(people);
        }

        [HttpGet("search/{search}")]
        public List<People> Get(string search) =>
            Repository.People.Where(p => p.Name.ToUpper().Contains(search.ToUpper())).ToList();

        [HttpPost]
        public IActionResult Add(People people)
        {
            if (!_peopleService.Validate(people))
            {
                return BadRequest();
            }

            Repository.People.Add(people);

            return NoContent();
        }
    }

    public class Repository
    {
        public static List<People> People = new List<People>
        {
            new People()
            {
                Id = 1,
                Name = "Jane Doe",
                Birthdate = new DateTime(1990, 12, 23)
            },
             new People()
            {
                Id = 2,
                Name = "Peter Doe",
                Birthdate = new DateTime(1985, 4, 3)
            },
              new People()
            {
                Id = 3,
                Name = "Lili Smith",
                Birthdate = new DateTime(1980, 12, 20)
            },

        };
    }

    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime Birthdate { get; set; }
    }
}
