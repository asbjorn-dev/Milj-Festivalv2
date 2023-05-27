﻿using Client.Services;
using MiljøFestivalv2.Shared;
using System.Net.Http.Json;

// Denne service bruges til at håndtere alle HTTP-anmodninger relateret til brugere til serveren
public class BrugerService : IBrugerService
{
    private readonly HttpClient HttpClient;

    // To forskellige hosts så vi kunne bytte mellem localhost og hosten til siden vi skulle deploy på
    public string Host = "https://localhost:7155";
    //public string Host = "https://xn--miljfestivalgruppe2-y7b.azurewebsites.net";

    // Konstruktør til BrugerService der tager en HttpClient som parameter
    // HttpClient bruges til at sende HTTP-anmodninger og modtage HTTP-respons fra en URI
    public BrugerService(HttpClient HttpClient)
    {
        this.HttpClient = HttpClient;
    }

    // Sender en HTTP GET-anmodning til den angivne URI for at hente alle brugere (frivillige)
    public Task<Bruger[]> HentAlleFrivillige()
    {
        var Resultat = HttpClient.GetFromJsonAsync<Bruger[]>($"{Host}/api/brugere/hentallefrivillige");
        return Resultat;
    }

    // Sender en HTTP POST-anmodning til den angivne URI for at tilføje en ny bruger
    public async Task TilføjBruger(Bruger bruger)
    {
        await HttpClient.PostAsJsonAsync($"{Host}/api/brugere/tilfoejfrivillig", bruger);
    }

    // Sender en HTTP GET-anmodning til den angivne URI for at logge en bruger ind
    public async Task<Bruger> Login(Login brugerinfo)
    {
        return await HttpClient.GetFromJsonAsync<Bruger>($"{Host}/api/brugere/login/{brugerinfo.Brugernavn}/{brugerinfo.Password}");
    }

    // Sender en HTTP PUT-anmodning til den angivne URI for at ændre status for en bruger (aktiv / inaktiv)
    public async Task SkiftAktivStatus(int bruger_id)
    {
        await HttpClient.PutAsync($"{Host}/api/brugere/skiftaktivstatus/{bruger_id}", null);
    }

    // Sender en HTTP PUT-anmodning til den angivne URI for at ændre blacklist status for en bruger
    public async Task SkiftBlacklistStatus(int bruger_id)
    {
        await HttpClient.PutAsync($"{Host}/api/brugere/skiftblackliststatus/{bruger_id}", null);
    }

    // Sender en HTTP GET-anmodning til den angivne URI for at hente en specifik bruger baseret på deres id
    public async Task<Bruger> HentBrugerSingle(int bruger_id)
    {
        return await HttpClient.GetFromJsonAsync<Bruger>($"{Host}/api/brugere/hentbrugersingle/{bruger_id}");
    }

    // Sender en HTTP PUT-anmodning til den angivne URI for at opdatere en bruger
    public async Task UpdateBruger(Bruger updatedBruger)
    {
        await HttpClient.PutAsJsonAsync($"{Host}/api/brugere/updatebruger/{updatedBruger.bruger_id}", updatedBruger);
    }
}
