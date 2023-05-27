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
    // Angiver at klassen er er en API controller
    [ApiController]
    // Ruten til controlleren
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
        // Henter alle vagter
        public async Task<IEnumerable<Vagt>> HentAlleVagter()
        {
            return await VagtReposi.HentAlleVagter();
        }

        [EnableCors("policy")]
        // Definerer en HTTP DELETE-metode og sætter URL'en
        [HttpDelete("{vagt_id}")]
        // Sletter en vagt med det angivne ID
        public async Task DeleteVagt(int vagt_id)
        {
            await VagtReposi.DeleteVagt(vagt_id);
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
        // Definerer en HTTP GET-metode og sætter URL'en
        [HttpGet("hentvagtsingle/{vagt_id}")]
        // Henter en enkelt vagt med det angivne ID
        public async Task<Vagt> HentVagtSingle(int vagt_id)
        {
            return await VagtReposi.HentVagtSingle(vagt_id);
        }

        [EnableCors("policy")]
        // Definerer en HTTP PUT-metode og sætter URL'en
        [HttpPut("opdatervagt/{vagt_id}")]
        // Opdaterer en eksisterende vagt med de angivne data
        public async Task OpdaterVagt([FromBody] Vagt OpdateretVagt)
        {
            await VagtReposi.OpdaterVagt(OpdateretVagt);
        }
    }
}
