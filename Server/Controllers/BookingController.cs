﻿using System;
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

        /* [EnableCors("policy")]
        [HttpPost("opretbooking")]
        public async Task OpretBooking(Booking booking)
        {
            await BookingRepo.OpretBooking(booking);
        } */

        [EnableCors("policy")]
        [HttpGet("hentbookingerforbruger/{brugerId}")]
        public async Task<IEnumerable<Booking>> HentBookingerForBruger(int brugerId)
        {
            Console.WriteLine(brugerId);
            return await BookingRepo.HentBookingerForBruger(brugerId);
        }

        [EnableCors("policy")]
        [HttpPost("opretbooking")]
        public async Task<IActionResult> OpretBooking([FromBody] BookingSql booking)
        {
            try
            {
                await BookingRepo.OpretBooking(booking);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [EnableCors("policy")]
        [HttpPut("skiftstatus/{BookingId}")]
        public async Task<IActionResult> SkiftLåsStatus(int BookingId)
        {
            try
            {
                await BookingRepo.SkiftLåsStatus(BookingId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [EnableCors("policy")]
        [HttpDelete("slet/{BookingId}")]
        public async Task SletBooking(int BookingId)
        {
            Booking booking = await BookingRepo.HentBookingSingle(BookingId);
            await BookingRepo.SletBooking(BookingId);
        }


	}
}

