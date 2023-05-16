using System;
using MiljøFestivalv2.Shared;
using System.Net.Http.Json;

namespace Client.Services
{
	public class VagtService : IVagtService
	{
        private readonly HttpClient HttpClient;

        public VagtService(HttpClient HttpClient)
        {
            this.HttpClient = HttpClient;
        }
        public Task<Vagt[]> HentAlleVagter()
        {
            var Resultat = HttpClient.GetFromJsonAsync<Vagt[]>("https://localhost:7155/api/vagter/hentallevagter");
            return Resultat;
        }
        public async Task DeleteVagt(int vagt_id)
        {
            await HttpClient.DeleteAsync($"https://localhost:7155/api/vagter/{vagt_id}");
        }

    }
}

