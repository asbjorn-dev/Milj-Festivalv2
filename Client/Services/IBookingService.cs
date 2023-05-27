using MiljøFestivalv2.Shared;

// Interface definition til IBookingService. Alle klasser, der implementerer dette interface, skal implementere alle metoder, der er defineret i interfacet.
public interface IBookingService
{
    // Metode til at hente alle bookinger. Implementeringen skal returnere et array af Booking-objekter.
    Task<Booking[]> HentAlleBookinger();

    // Metode til at hente bookinger for en specifik bruger. Den tager et bruger_id som argument og skal returnere et array af Booking-objekter.
    Task<Booking[]> HentBookingerForBruger(int bruger_id);

    // Metode til at oprette en ny booking. Den tager et BookingSql objekt som argument.
    Task OpretBooking(BookingSql booking);

    // Metode til at slette en booking. Den tager et bookingId som argument.
    Task SletBooking(int bookingId);

    // Metode til at skifte låsestatus for en specifik booking. Den tager et BookingId som argument.
    Task SkiftLåsStatus(int BookingId);
}
