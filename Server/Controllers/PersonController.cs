using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Microsoft.AspNetCore.Cors;
using MiljøFestivalv2.Shared;
using System.Diagnostics;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/bruger")]
    public class PersonController : ControllerBase
    {
        private IPersonRepository personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        [EnableCors("policy")]
        [HttpGet("{brugerId}")]
        public async Task<ActionResult<Person>> GetPerson(int brugerId)
        {
            var person = await personRepository.GetPerson(brugerId);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [EnableCors("policy")]
        [HttpPut("{brugerId}")]
        public async Task<ActionResult> UpdatePerson(int brugerId, [FromBody] Person updatedPerson)
        {
            if (brugerId != updatedPerson.bruger_id)
            {
                return BadRequest();
            }

            var existingPerson = await personRepository.GetPerson(brugerId);
            if (existingPerson == null)
            {
                return NotFound();
            }

            // Update the existing person with the updated values
            existingPerson.fulde_navn = updatedPerson.fulde_navn;
            existingPerson.email = updatedPerson.email;
            existingPerson.password = updatedPerson.password;
            existingPerson.telefon_nummer = updatedPerson.telefon_nummer;

            // Save the changes to the database
            await personRepository.UpdatePerson(existingPerson);

            return NoContent();
        }
    }
}
