using System;
using System.Net.Http.Json;
using Shared;


namespace Client.Services
{
    
    public class BrugerService : IBrugerService
    {
        private readonly HttpClient HttpClient;

        public BrugerService(HttpClient HttpClient)
        {
            this.HttpClient = HttpClient;
        }
        public Task<Bruger[]> HentAlleFrivillige() 
        {
            var Resultat = HttpClient.GetFromJsonAsync<Bruger[]>("https://localhost:7139/api/brugere/hentallefrivillige");
            return Resultat;
        }
    }
}
