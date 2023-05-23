using System;
using MiljøFestivalv2.Shared;
using System.Net.Http.Json;

namespace Client.Services
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient HttpClient;
        public string Host = "https://localhost:7155";
        // public string Host = "https://xn--miljfestivalgruppe2-y7b.azurewebsites.net";

        public MessageService(HttpClient HttpClient)
        {
            this.HttpClient = HttpClient;
        }
        public Task<Msg_board[]> HentAlleBeskeder()
        {
            var Resultat = HttpClient.GetFromJsonAsync<Msg_board[]>($"{Host}/api/beskeder/hentallebeskeder");
            return Resultat;
        }

        public async Task TilføjBesked(Msg_board msg)
        {
            await HttpClient.PostAsJsonAsync($"{Host}/api/beskeder/tilfoejbesked", msg);
        }
    }
}
