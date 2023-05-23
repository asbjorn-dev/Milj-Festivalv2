using System;
using MiljøFestivalv2.Shared;
using System.Net.Http.Json;

namespace Client.Services
{
	public class VagtService : IVagtService
	{
        private readonly HttpClient HttpClient;
        public string Host = "https://localhost:7155";
       // public string Host = "https://xn--miljfestivalgruppe2-y7b.azurewebsites.net";

        public VagtService(HttpClient HttpClient)
        {
            this.HttpClient = HttpClient;
        }
        public Task<Vagt[]> HentAlleVagter()
        {
            var Resultat = HttpClient.GetFromJsonAsync<Vagt[]>($"{Host}/api/vagter/hentallevagter");
            return Resultat;
        }
        public async Task DeleteVagt(int vagt_id)
        {
            await HttpClient.DeleteAsync($"{Host}/api/vagter/{vagt_id}");
        }
		public async Task<Vagt> HentVagtSingle(int vagt_id)
		{
			return await HttpClient.GetFromJsonAsync<Vagt>($"{Host}/api/vagter/hentvagtsingle/{vagt_id}");
		}

		public async Task OpdaterVagt(Vagt OpdateretVagt)
        {
            await HttpClient.PutAsJsonAsync($"{Host}/api/vagter/opdatervagt/{OpdateretVagt.vagt_id}", OpdateretVagt);
        }

        public async Task TilføjVagt(Vagt vagt)
        {
            await HttpClient.PostAsJsonAsync($"{Host}/api/vagter/tilfoejvagt", vagt);
        }


     
    }
}

