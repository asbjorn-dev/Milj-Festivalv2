using System;
using MiljøFestivalv2.Shared;

namespace Server.Models
{
    public interface IBookingRepository
    {
        // Metode til at hente alle bookinger
        Task<IEnumerable<Booking>> HentAlleBookinger();

        // Metode til at hente bookinger for en specifik bruger
        Task<IEnumerable<Booking>> HentBookingerForBruger(int brugerId);

        // Metode til at hente en enkelt booking ved hjælp af BookingId
        Task<Booking> HentBookingSingle(int BookingId);

        // Metode til at oprette en ny booking
        Task OpretBooking(BookingSql booking);

        // Metode til at slette en booking ved hjælp af BookingId
        Task SletBooking(int BookingId);

        // Metode til at skifte låsstatus for en booking
        Task SkiftLåsStatus(int BookingId);
    }
}
