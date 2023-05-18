using System;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using MiljøFestivalv2.Shared;

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
            var Resultat = HttpClient.GetFromJsonAsync<Bruger[]>("https://localhost:7155/api/brugere/hentallefrivillige");
            return Resultat;
        }

        public async Task TilføjBruger(Bruger bruger)
        {
            await HttpClient.PostAsJsonAsync($"https://localhost:7155/api/brugere/tilfoejfrivillig", bruger);
        }

        public async Task<Login> Login(Login brugerinfo)
        {
            return await HttpClient.GetFromJsonAsync<Login>($"https://localhost:7155/api/brugere/login/{brugerinfo.Brugernavn}/{brugerinfo.Password}");
           
        }
    }
}
