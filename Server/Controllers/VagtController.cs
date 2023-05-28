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
        // Privat instans af IVagtRepository
        private IVagtRepository VagtReposi;

        // Constructor til VagtController, som initialiserer VagtReposi med den injicerede IVagtRepository
        public VagtController(IVagtRepository VagtRepo)
        {
            VagtReposi = VagtRepo;
        }

        
        [EnableCors("policy")]
        // Definerer en HTTP GET-metode og sætter URL'en
        [HttpGet("hentallevagter")]
        public async Task<IEnumerable<Vagt>> HentAlleVagter()
        {
            return await VagtReposi.HentAlleVagter();
        }

        [EnableCors("policy")]
        // Definerer en HTTP DELETE-metode og sætter URL'en
        [HttpDelete("{VagtId}")]
        public async Task DeleteVagt(int VagtId)
        {
            await VagtReposi.DeleteVagt(VagtId);
        }

        [EnableCors("policy")]
        // Definerer en HTTP POST-metode og sætter URL'en
        [HttpPost("tilfoejvagt")]
        // Tilføjer en ny vagt
        public async Task TilføjVagt(Vagt vagt)
        {
            await VagtReposi.TilføjVagt(vagt);
        }

        [EnableCors("policy")]
        [HttpGet("hentvagtsingle/{VagtId}")]
        public async Task<Vagt> HentVagtSingle(int VagtId)
        {
            return await VagtReposi.HentVagtSingle(VagtId);
        }

        [EnableCors("policy")]
        // Definerer en HTTP PUT-metode og sætter URL'en
        [HttpPut("opdatervagt/{VagtId}")]
        public async Task OpdaterVagt([FromBody] Vagt OpdateretVagt)
        {
            await VagtReposi.OpdaterVagt(OpdateretVagt);
        }
    }
}
