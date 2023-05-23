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
        private IMessageRepository MessageReposi;
        public MessageController(IMessageRepository MessageRepo)
        {
            MessageReposi = MessageRepo;
        }
        [EnableCors("policy")]
        [HttpGet("hentallebeskeder")]
        public async Task<IEnumerable<Msg_board>> HentAlleBeskeder()
        {
            return await MessageReposi.HentAlleBeskeder();
        }
    }
}
