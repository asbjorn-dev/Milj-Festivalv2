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

        public BrugerController(IBrugerRepository FriRepo)
        {
            FrivilligRepo = FriRepo;
        }

        [EnableCors("policy")]
        [HttpGet("hentallefrivillige")]
        public async Task<IEnumerable<Bruger>> HentAlleFrivillige()
        {
            return await FrivilligRepo.HentAlleFrivillige();
        }

        [EnableCors("policy")]
        [HttpPost("tilfoejfrivillig")]
        public async Task TilføjFrivillig(Bruger Bruger)
        {
            await FrivilligRepo.TilføjFrivillig(Bruger);
        }

        [EnableCors("policy")]
        [HttpGet("login/{brugernavn}/{password}")]
        public Login Login(string brugernavn, string password)
        {
            return FrivilligRepo.HentBrugerMedBrugernavnOgPassword(brugernavn, password);

        }

        [HttpPut("skiftaktivstatus/{bruger_id}")]
        public async Task SkiftAktivStatus(int bruger_id)
        {
            await FrivilligRepo.SkiftAktivStatus(bruger_id);
        }

        [HttpPut("skiftblackliststatus/{bruger_id}")]
        public async Task SkiftBlacklistStatus(int bruger_id)
        {
            await FrivilligRepo.SkiftBlacklistStatus(bruger_id);
        }

        [HttpGet("hentbrugersingle/{bruger_id}")]
        public async Task <Bruger>HentBrugerSingle(int bruger_id)
        {
            return await FrivilligRepo.HentBrugerSingle(bruger_id);
        }

		[HttpPut("updatebruger/{bruger_id}")]
		public async Task UpdateBruger(int bruger_id, [FromBody] Bruger updatedBruger)
		{
			await FrivilligRepo.UpdateBruger(updatedBruger);
		}

	}
}

