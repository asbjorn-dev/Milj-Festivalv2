using MiljøFestivalv2.Shared;

// Interface definition til IBookingService. Alle klasser, der implementerer dette interface, skal implementere alle metoder, der er defineret i interfacet.
public interface IBookingService
{
    Task<Booking[]> HentAlleBookinger();

    Task<Booking[]> HentBookingerForBruger(int bruger_id);

    Task OpretBooking(BookingSql booking);

    Task SletBooking(int bookingId);

    Task SkiftLåsStatus(int BookingId);
}
