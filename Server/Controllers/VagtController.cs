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
        public async Task DeleteVagt(int vagt_id)
        {
            await VagtReposi.DeleteVagt(vagt_id);
        }

<<<<<<< HEAD
        [EnableCors("policy")]
        [HttpPost("tilfoejvagt")]
        public async Task TilføjVagt(Vagt vagt)
        {
            await VagtReposi.TilføjVagt(vagt);
        }



=======
		[HttpGet("hentvagtsingle/{vagt_id}")]
		public async Task<Vagt> HentVagtSingle(int vagt_id)
		{
			return await VagtReposi.HentVagtSingle(vagt_id);
		}

		[HttpPut("opdatervagt/{vagt_id}")]
        public async Task OpdaterVagt([FromBody] Vagt OpdateretVagt)
        {
            await VagtReposi.OpdaterVagt(OpdateretVagt);
        }

>>>>>>> c4063f873bf0f4c2fa57cee4a23442eb2b34840e
    }



}

