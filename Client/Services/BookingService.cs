using System;
using System.Net.Http.Json;
using MiljøFestivalv2.Shared;
using Client.Services;


namespace Client.Services
{

    public class BookingService : IBookingService
    {
        private readonly HttpClient HttpClient;

        public string Host = "https://localhost:7155";
        //public string Host = "https://xn--miljfestivalgruppe2-y7b.azurewebsites.net";

        public BookingService(HttpClient HttpClient)
        {
            this.HttpClient = HttpClient;
        }
        public Task<Booking[]> HentAlleBookinger()
        {
            var Resultat = HttpClient.GetFromJsonAsync<Booking[]>($"{Host}/api/bookinger/hentallebookinger");
            return Resultat;
        }
        public Task<Booking[]> HentBookingerForBruger(int bruger_id)
        {
            var Resultat = HttpClient.GetFromJsonAsync<Booking[]>($"{Host}/api/bookinger/hentbookingerforbruger/{bruger_id}");
            return Resultat;
        }

        public async Task OpretBooking(BookingSql booking)
        {
            Console.WriteLine(booking);
            var response = await HttpClient.PostAsJsonAsync($"{Host}/api/bookinger/opretbooking", booking);
            
            // Kontrollere svaret og kaster en exception hvis den fejler
            response.EnsureSuccessStatusCode();
        }

    }
}
