using MiljøFestivalv2.Shared;

namespace Server.Models
{
    public interface IBookingRepository
    {
        Task<IEnumerable<Booking>> HentAlleBookinger();
        Task<IEnumerable<Booking>> HentBookingerForBruger(int BrugerId);

        Task<Booking> HentBookingSingle(int BookingId);

        Task OpretBooking(BookingSql Booking);

        Task SletBooking(int BookingId);

        Task SkiftLåsStatus(int BookingId);
    }
}
