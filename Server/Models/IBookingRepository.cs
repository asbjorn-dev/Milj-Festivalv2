using System;
using MiljøFestivalv2.Shared;

namespace Server.Models
{
	public interface IBookingRepository 
	{
        Task<IEnumerable<Booking>> HentAlleBookinger();
        Task<IEnumerable<Booking>> HentBookingerForBruger(int brugerId);
        Task<Booking> HentBookingSingle(int BookingId);
        Task OpretBooking(BookingSql booking);
        Task SletBooking(int BookingId);
        Task SkiftLåsStatus(int BookingId);
    }
}

