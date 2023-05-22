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
    [Route("api/bookinger")]
    public class BookingController : ControllerBase
    {

        private IBookingRepository BookingRepo;

        public BookingController(IBookingRepository BookRepo)
        {
            BookingRepo = BookRepo;
        }

        [EnableCors("policy")]
        [HttpGet("hentallebookinger")]
        public async Task<IEnumerable<Booking>> HentAlleBookinger()
        {
            return await BookingRepo.HentAlleBookinger();
        }

        [EnableCors("policy")]
        [HttpGet("hentbookingerforbruger/{brugerId}")]
        public async Task<IEnumerable<Booking>> HentBookingerForBruger(int brugerId)
        {
            Console.WriteLine(brugerId);
            return await BookingRepo.HentBookingerForBruger(brugerId);
        }
    }
}

