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
    [Route("api/beskeder")]
    public class MessageController : ControllerBase
    {
        // Privat instans af IMessageRepository
        private IMessageRepository MessageReposi;

        // Constructor til MessageController, som initialiserer MessageReposi med den injicerede IMessageRepository
        public MessageController(IMessageRepository MessageRepo)
        {
            MessageReposi = MessageRepo;
        }

        [EnableCors("policy")]
        // Definerer en HTTP GET-metode og sætter URL'en
        [HttpGet("hentallebeskeder")]
        public async Task<IEnumerable<Msg_board>> HentAlleBeskeder()
        {
            return await MessageReposi.HentAlleBeskeder();
        }

        [EnableCors("policy")]
        // Definerer en HTTP POST-metode og sætter URL'en
        [HttpPost("tilfoejbesked")]
        public async Task TilføjBesked(Msg_board msg)
        {
            await MessageReposi.TilføjBesked(msg);
        }
    }
}
