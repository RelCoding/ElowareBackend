using ELOWARE_Backend.DTOs;
using ELOWARE_Backend.Objects;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace ELOWARE_Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonenController : ControllerBase
    {
        private readonly ILogger<PersonenController> _logger;
        private readonly ElowareContext _elowareContext;

        public PersonenController(ILogger<PersonenController> logger, ElowareContext elowareContext)
        {
            _logger = logger;
            _elowareContext = elowareContext;
        }

        [HttpGet]
        [Route("Person")]
        public PersonDto Get([FromRoute]string vorName, [FromRoute] string nachName)
        {
            //Finde Person in DB
            var dbPerson = _elowareContext.Personen.Where(x => x.VorName == vorName && x.NachName == nachName).FirstOrDefault();
            if (dbPerson == null)
            {
                return new PersonDto();
            }
            else
            {
                //Konvertiere zu DTO Objekt
                var personDto = new PersonDto();
                personDto.VorName = dbPerson.VorName;
                personDto.NachName = dbPerson.NachName;
                personDto.GeburtsDatum = dbPerson.GeburtsDatum;
                personDto.LieblingsFarbe = dbPerson.LieblingsFarbe;
                
                return personDto;
            }
                
        }

        [HttpPost]
        [Route("Personen")]
        public IEnumerable<PersonDto> GetPersonen(Guid[] ids)
        {
            //Finde Person in DB
            var dbPersonen = _elowareContext.Personen.ToList();
            if (dbPersonen.Count < 1)
            {
                return new List<PersonDto>();
            }
            else
            {
                //Konvertiere zu DTO Objekt
                var personen = new List<PersonDto>();
                foreach(var dbPerson in  dbPersonen)
                {
                    var personDto = new PersonDto();
                    personDto.VorName = dbPerson.VorName;
                    personDto.NachName = dbPerson.NachName;
                    personDto.GeburtsDatum = dbPerson.GeburtsDatum;
                    personDto.LieblingsFarbe = dbPerson.LieblingsFarbe;
                    personen.Add(personDto);
                }

                return personen;
            }

        }

        [HttpPost]
        [Route("Person")]
        public IActionResult Post(PersonDto person)
        {
            //Konvertiere zu DB Objekt
            var dbPerson = new Person();
            dbPerson.VorName = person.VorName;
            dbPerson.NachName = person.NachName;
            dbPerson.GeburtsDatum = person.GeburtsDatum;
            dbPerson.LieblingsFarbe = person.LieblingsFarbe;

            //Lege person an
            _elowareContext.Personen.Add(dbPerson);
            _elowareContext.SaveChanges();

            return new OkObjectResult("Person angelegt.");
        }

        [HttpPut]
        [Route("Person")]
        public IActionResult Put(PersonDto person, Guid personId)
        {
            //Finde Person in DB
            var personToChange = _elowareContext.Personen.Where(x => x.Id == personId).FirstOrDefault();
            if (personToChange == null)
                return new NotFoundObjectResult("Keine Person mit der Id gefunden");
            else
            {
                personToChange.VorName = person.VorName;
                personToChange.NachName = person.NachName;
                personToChange.GeburtsDatum = person.GeburtsDatum;
                personToChange.LieblingsFarbe = person.LieblingsFarbe;

                _elowareContext.SaveChanges();

                return new OkObjectResult("Person editiert.");
            }
        }

        [HttpDelete]
        [Route("Person")]
        public IActionResult Delete(Guid personId)
        {
            //Finde Person in DB
            var personToChange = _elowareContext.Personen.Where(x => x.Id == personId).FirstOrDefault();
            if (personToChange == null)
                return new NotFoundObjectResult("Keine Person mit der Id gefunden");
            else
            {
                _elowareContext.Personen.Remove(personToChange);
                _elowareContext.SaveChanges();

                return new OkObjectResult("Person gelöscht.");
            }
        }
    }
}
