using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Microsoft.AspNetCore.Cors;
using Shared;
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
    }
}

