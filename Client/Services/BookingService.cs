using System.Net.Http.Json;
using MiljøFestivalv2.Shared;


namespace Client.Services
{
    public class BookingService : IBookingService
    {
        // HttpClient bliver brugt til at sende HTTP-forespørgsler og modtage HTTP-svar fra en URI
        private readonly HttpClient HttpClient;

        // To forskellige hosts så vi kunne bytte mellem localhost og hosten til siden vi skulle deploy på
        //public string Host = "https://localhost:7155";
        public string Host = "https://xn--miljfestivalgruppe2-y7b.azurewebsites.net";

        // Constructor til BookingService, som initialiserer HttpClient med den injicerede HttpClient
        public BookingService(HttpClient HttpClient)
        {
            this.HttpClient = HttpClient;
        }

        public Task<Booking[]> HentAlleBookinger()
        {
            // Sender en HTTP GET-anmodning til den angivne URI og returnere svaret
            // .GetFromJsonAsync er en hjælpemetode, der udfører en GET-anmodning og deserialiserer svaret til en given type
            var Resultat = HttpClient.GetFromJsonAsync<Booking[]>($"{Host}/api/bookinger/hentallebookinger");

            // Vi returnerer en Task<Booking[]>, som repræsenterer en igangværende operation, der producerer et array af Booking
            return Resultat;
        }

        public Task<Booking[]> HentBookingerForBruger(int BrugerId)
        {
            // Send en HTTP GET-anmodning til den angivne URI med bruger_id som parameter i URL og returnere svaret
            var Resultat = HttpClient.GetFromJsonAsync<Booking[]>($"{Host}/api/bookinger/hentbookingerforbruger/{BrugerId}");

            // Vi returnerer en Task<Booking[]>, som repræsenterer en igangværende operation, der producerer et array af Booking
            return Resultat;
        }


        public async Task OpretBooking(BookingSql Booking)
        {
            // Sender en POST-request til serveren med bookingen som JSON i body
            var response = await HttpClient.PostAsJsonAsync($"{Host}/api/bookinger/opretbooking", Booking);
            // Kontrollere svaret og kaster en exception hvis den fejler
            // Dette sikrer, at hvis noget går galt, vil vi få en exception her.
            response.EnsureSuccessStatusCode();
        }

        public async Task SletBooking(int BookingId)
        {
            // Sender en DELETE-request til serveren med bookingId i URL'en
            await HttpClient.DeleteAsync($"{Host}/api/bookinger/slet/{BookingId}");
        }

        public async Task SkiftLåsStatus(int BookingId)
        {
            // Sender en PUT-request til serveren med BookingId i URL'en
            await HttpClient.PutAsync($"{Host}/api/bookinger/skiftstatus/{BookingId}", null);
        }
    }
}
