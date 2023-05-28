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
        [HttpGet("login/{Brugernavn}/{Password}")]
        public Bruger Login(string Brugernavn, string Password)
        {
            return FrivilligRepo.HentBrugerMedBrugernavnOgPassword(Brugernavn, Password);
        }

        [EnableCors("policy")]
        // Definerer en HTTP PUT-metode og sætter URL'en
        [HttpPut("skiftaktivstatus/{BrugerId}")]
        public async Task SkiftAktivStatus(int BrugerId)
        {
            await FrivilligRepo.SkiftAktivStatus(BrugerId);
        }

        [EnableCors("policy")]
        [HttpPut("skiftblackliststatus/{BrugerId}")]
        public async Task SkiftBlacklistStatus(int BrugerId)
        {
            await FrivilligRepo.SkiftBlacklistStatus(BrugerId);
        }

        [EnableCors("policy")]
        [HttpGet("hentbrugersingle/{BrugerId}")]
        public async Task<Bruger> HentBrugerSingle(int BrugerId)
        {
            return await FrivilligRepo.HentBrugerSingle(BrugerId);
        }

        [EnableCors("policy")]
        [HttpPut("updatebruger/{BrugerId}")]
        public async Task UpdateBruger([FromBody] Bruger UpdatedBruger)
        {
            await FrivilligRepo.UpdateBruger(UpdatedBruger);
        }
    }
}
