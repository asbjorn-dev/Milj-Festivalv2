using MiljøFestivalv2.Shared;

// Interface definition til IBookingService. Alle klasser, der implementerer dette interface, skal implementere alle metoder, der er defineret i interfacet.
public interface IBookingService
{
    Task<Booking[]> HentAlleBookinger();

    Task<Booking[]> HentBookingerForBruger(int Brugerid);

    Task OpretBooking(BookingSql Booking);

    Task SletBooking(int BookingId);

    Task SkiftLåsStatus(int BookingId);
}
