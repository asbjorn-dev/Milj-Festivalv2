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
    [Route("api/brugere")]
    public class BrugerController : ControllerBase
    {
        private IBrugerRepository FrivilligRepo;

        // Constructor til BrugerController, som initialiserer FrivilligRepo med den injicerede IBrugerRepository
        public BrugerController(IBrugerRepository FriRepo)
        {
            FrivilligRepo = FriRepo;
        }
       
        [EnableCors("policy")]
        // Definerer en HTTP GET-metode og sætter URL'en
        [HttpGet("hentallefrivillige")]
        public async Task<IEnumerable<Bruger>> HentAlleFrivillige()
        {
            return await FrivilligRepo.HentAlleFrivillige();
        }

        [EnableCors("policy")]
        // Definerer en HTTP POST-metode og sætter URL'en
        [HttpPost("tilfoejfrivillig")]
        public async Task TilføjFrivillig(Bruger Bruger)
        {
            await FrivilligRepo.TilføjFrivillig(Bruger);
        }

        [EnableCors("policy")]
        [HttpGet("login/{brugernavn}/{password}")]
        public Bruger Login(string brugernavn, string password)
        {
            return FrivilligRepo.HentBrugerMedBrugernavnOgPassword(brugernavn, password);
        }

        [EnableCors("policy")]
        // Definerer en HTTP PUT-metode og sætter URL'en
        [HttpPut("skiftaktivstatus/{bruger_id}")]
        public async Task SkiftAktivStatus(int bruger_id)
        {
            await FrivilligRepo.SkiftAktivStatus(bruger_id);
        }

        [EnableCors("policy")]
        [HttpPut("skiftblackliststatus/{bruger_id}")]
        public async Task SkiftBlacklistStatus(int bruger_id)
        {
            await FrivilligRepo.SkiftBlacklistStatus(bruger_id);
        }

        [EnableCors("policy")]
        [HttpGet("hentbrugersingle/{bruger_id}")]
        public async Task<Bruger> HentBrugerSingle(int bruger_id)
        {
            return await FrivilligRepo.HentBrugerSingle(bruger_id);
        }

        [EnableCors("policy")]
        [HttpPut("updatebruger/{bruger_id}")]
        public async Task UpdateBruger([FromBody] Bruger updatedBruger)
        {
            await FrivilligRepo.UpdateBruger(updatedBruger);
        }
    }
}
