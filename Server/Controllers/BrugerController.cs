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
        [HttpPost("login")]
        public async Task<Bruger> Login(Bruger Bruger)
        {
            return await FrivilligRepo.HentBrugerMedBrugernavnOgPassword(Bruger.brugernavn, Bruger.password);

        }


    }
}

