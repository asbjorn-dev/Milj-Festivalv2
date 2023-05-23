using System;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using MiljøFestivalv2.Shared;

namespace Client.Services
{
    
    public class BrugerService : IBrugerService
    {
        private readonly HttpClient HttpClient;

        private string Host = "https://localhost:7155";
        //private string Host = "https://xn--miljfestivalgruppe2-y7b.azurewebsites.net";

        public BrugerService(HttpClient HttpClient)
        {
            this.HttpClient = HttpClient;
        }
        public Task<Bruger[]> HentAlleFrivillige() 
        {
            var Resultat = HttpClient.GetFromJsonAsync<Bruger[]>($"{Host}/api/brugere/hentallefrivillige");
            return Resultat;
        }

        public async Task TilføjBruger(Bruger bruger)
        {
            await HttpClient.PostAsJsonAsync($"{Host}/api/brugere/tilfoejfrivillig", bruger);
        }

        public async Task<Login> Login(Login brugerinfo)
        {
            return await HttpClient.GetFromJsonAsync<Login>($"{Host}/api/brugere/login/{brugerinfo.Brugernavn}/{brugerinfo.Password}");
           
        }

        public async Task SkiftAktivStatus(int bruger_id)
        {
            await HttpClient.PutAsync($"{Host}/api/brugere/skiftaktivstatus/{bruger_id}", null);
        }

        public async Task SkiftBlacklistStatus(int bruger_id)
        {
            await HttpClient.PutAsync($"{Host}/api/brugere/skiftblackliststatus/{bruger_id}", null);
        }

        public async Task<Bruger> HentBrugerSingle(int bruger_id)
        {
            return await HttpClient.GetFromJsonAsync<Bruger>($"{Host}/api/brugere/hentbrugersingle/{bruger_id}");
        }


        public async Task UpdateBruger(Bruger updatedBruger)
        {
            await HttpClient.PutAsJsonAsync($"{Host}/api/brugere/updatebruger/{updatedBruger.bruger_id}", updatedBruger);

        }

    }
}
