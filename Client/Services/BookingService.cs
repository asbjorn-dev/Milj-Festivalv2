using System;
using System.Net.Http.Json;
using MiljøFestivalv2.Shared;


namespace Client.Services
{

    public class BookingService : IBookingService
    {
        private readonly HttpClient HttpClient;

        public BookingService(HttpClient HttpClient)
        {
            this.HttpClient = HttpClient;
        }
        public Task<Booking[]> HentAlleBookinger()
        {
            var Resultat = HttpClient.GetFromJsonAsync<Booking[]>("https://festivalclient.azurewebsites.net/api/bookinger/hentallebookinger");
            return Resultat;
        }
        public Task<Booking[]> HentBookingerForBruger(int bruger_id)
        {
            var Resultat = HttpClient.GetFromJsonAsync<Booking[]>($"https://festivalclient.azurewebsites.net/api/bookinger/hentbookingerforbruger/{bruger_id}");
            return Resultat;
        }

        public async Task OpretBooking(Booking booking)
        {
            await HttpClient.PostAsJsonAsync("https://festivalclient.azurewebsites.net/api/bookinger/opretbooking", booking);
        }
    }
}
