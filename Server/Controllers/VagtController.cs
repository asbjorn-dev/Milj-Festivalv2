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
    [Route("api/vagter")]
    public class VagtController : ControllerBase
    {

        private IVagtRepository VagtReposi;

        public VagtController(IVagtRepository VagtRepo)
        {
            VagtReposi = VagtRepo;
        }

        [EnableCors("policy")]
        [HttpGet("hentallevagter")]
        public async Task<IEnumerable<Vagt>> HentAlleVagter()
        {
            return await VagtReposi.HentAlleVagter();
        }

        [EnableCors("policy")]
        [HttpDelete("{vagt_id}")]
        public void DeleteVagt(int vagt_id)
        {
            VagtReposi.DeleteVagt(vagt_id);
        }
    }
}

