using System;
using MiljøFestivalv2.Shared;

namespace Client.Services
{
    public interface IBookingService
    {
        Task<Booking[]> HentAlleBookinger();
        Task<Booking[]> HentBookingerForBruger(int bruger_id);
        Task OpretBooking(BookingSql booking);
        Task SkiftLåsStatus(int BookingId);
    }
}
