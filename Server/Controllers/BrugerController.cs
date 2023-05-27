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
    // Angiver at klassen er en API controller
    [ApiController]
    // Rute til denne controller
    [Route("api/brugere")]
    public class BrugerController : ControllerBase
    {
        // Privat instans af IBrugerRepository
        private IBrugerRepository FrivilligRepo;

        // Constructor til BrugerController, som initialiserer FrivilligRepo med den injicerede IBrugerRepository
        public BrugerController(IBrugerRepository FriRepo)
        {
            FrivilligRepo = FriRepo;
        }
       
        [EnableCors("policy")]
        // Definerer en HTTP GET-metode og sætter URL'en
        [HttpGet("hentallefrivillige")]
        // Henter alle frivillige
        public async Task<IEnumerable<Bruger>> HentAlleFrivillige()
        {
            return await FrivilligRepo.HentAlleFrivillige();
        }

        [EnableCors("policy")]
        // Definerer en HTTP POST-metode og sætter URL'en
        [HttpPost("tilfoejfrivillig")]
        // Tilføjer en ny frivillig
        public async Task TilføjFrivillig(Bruger Bruger)
        {
            await FrivilligRepo.TilføjFrivillig(Bruger);
        }

        [EnableCors("policy")]
        // Definerer en HTTP GET-metode og sætter URL'en
        [HttpGet("login/{brugernavn}/{password}")]
        // Logger en bruger ind med det givne brugernavn og password
        public Bruger Login(string brugernavn, string password)
        {
            return FrivilligRepo.HentBrugerMedBrugernavnOgPassword(brugernavn, password);
        }

        [EnableCors("policy")]
        // Definerer en HTTP PUT-metode og sætter URL'en
        [HttpPut("skiftaktivstatus/{bruger_id}")]
        // Skifter den aktive status for en bruger med det givne ID
        public async Task SkiftAktivStatus(int bruger_id)
        {
            await FrivilligRepo.SkiftAktivStatus(bruger_id);
        }

        [EnableCors("policy")]
        // Definerer en HTTP PUT-metode og sætter URL'en
        [HttpPut("skiftblackliststatus/{bruger_id}")]
        // Skifter blacklist-status for en bruger med det givne ID
        public async Task SkiftBlacklistStatus(int bruger_id)
        {
            await FrivilligRepo.SkiftBlacklistStatus(bruger_id);
        }

        [EnableCors("policy")]
        // Definerer en HTTP GET-metode og sætter URL'en
        [HttpGet("hentbrugersingle/{bruger_id}")]
        // Henter en enkelt bruger med det givne ID
        public async Task<Bruger> HentBrugerSingle(int bruger_id)
        {
            return await FrivilligRepo.HentBrugerSingle(bruger_id);
        }

        [EnableCors("policy")]
        // Definerer en HTTP PUT-metode og sætter URL'en
        [HttpPut("updatebruger/{bruger_id}")]
        // Opdaterer en eksisterende bruger med de angivne data
        public async Task UpdateBruger([FromBody] Bruger updatedBruger)
        {
            await FrivilligRepo.UpdateBruger(updatedBruger);
        }
    }
}
