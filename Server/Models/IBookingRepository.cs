using System;
using Shared;

namespace Server.Models
{
	public interface IBookingRepository 
	{
        Task<IEnumerable<Booking>> HentAlleBookinger();
    }
}

