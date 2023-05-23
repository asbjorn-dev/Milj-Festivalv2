using System;
using MiljøFestivalv2.Shared;
using System.Net.Http.Json;

namespace Client.Services
{
	public class VagtService : IVagtService
	{
        private readonly HttpClient HttpClient;
        public string LocalHost = "https://localhost:7155";
        public string AzureHost = "https://xn--miljfestivalgruppe2-y7b.azurewebsites.net";

        public VagtService(HttpClient HttpClient)
        {
            this.HttpClient = HttpClient;
        }
        public Task<Vagt[]> HentAlleVagter()
        {
            var Resultat = HttpClient.GetFromJsonAsync<Vagt[]>($"{LocalHost}/api/vagter/hentallevagter");
            return Resultat;
        }
        public async Task DeleteVagt(int vagt_id)
        {
            await HttpClient.DeleteAsync($"{LocalHost}/api/vagter/{vagt_id}");
        }
		public async Task<Vagt> HentVagtSingle(int vagt_id)
		{
			return await HttpClient.GetFromJsonAsync<Vagt>($"{LocalHost}/api/vagter/hentvagtsingle/{vagt_id}");
		}

		public async Task OpdaterVagt(Vagt OpdateretVagt)
        {
            await HttpClient.PutAsJsonAsync($"{LocalHost}/api/vagter/opdatervagt/{OpdateretVagt.vagt_id}", OpdateretVagt);
        }

        public async Task TilføjVagt(Vagt vagt)
        {
            await HttpClient.PostAsJsonAsync($"{LocalHost}/api/vagter/tilfoejvagt", vagt);
        }


     
    }
}

