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
		public async Task<Vagt> HentVagtSingle(int vagt_id)
		{
			return await HttpClient.GetFromJsonAsync<Vagt>($"https://localhost:7155/api/vagter/hentvagtsingle/{vagt_id}");
		}

		public async Task OpdaterVagt(Vagt OpdateretVagt)
        {
            await HttpClient.PutAsJsonAsync($"https://localhost:7155/api/vagter/opdatervagt/{OpdateretVagt.vagt_id}", OpdateretVagt);
        }

    }
}

