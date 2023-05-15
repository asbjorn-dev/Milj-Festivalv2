using System;
using System.Net.Http.Json;
using Shared;


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
            var Resultat = HttpClient.GetFromJsonAsync<Booking[]>("https://localhost:7139/api/bookinger/hentallebookinger");
            return Resultat;
        }
    }
}
