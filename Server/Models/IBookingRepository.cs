using System;
using MiljøFestivalv2.Shared;

namespace Server.Models
{
	public interface IBookingRepository 
	{
        Task<IEnumerable<Booking>> HentAlleBookinger();
    }
}

