using MiljøFestivalv2.Shared;
using System.Net.Http.Json;

// Definerer klassen MessageService, som implementerer IMessageService interfacet
public class MessageService : IMessageService
{
    // Deklarerer en privat, readonly HttpClient, der vil blive brugt til at foretage HTTP-anmodninger
    private readonly HttpClient HttpClient;

    // To forskellige hosts så vi kunne bytte mellem localhost og hosten til siden vi skulle deploy på
    public string Host = "https://localhost:7155";
    //public string Host = "https://xn--miljfestivalgruppe2-y7b.azurewebsites.net";

    // Constructor, der tager en HttpClient som parameter og initialiserer den private HttpClient med den.
    public MessageService(HttpClient HttpClient)
    {
        this.HttpClient = HttpClient;
    }

    // Metode til at hente alle beskeder fra serveren. Det sender en GET-anmodning til angivet URL og returnerer resultatet som en opgave af en besked-array.
    public Task<Msg_board[]> HentAlleBeskeder()
    {
        var Resultat = HttpClient.GetFromJsonAsync<Msg_board[]>($"{Host}/api/beskeder/hentallebeskeder");
        return Resultat;
    }

    // Metode til at sende en besked til serveren. Det sender en POST-anmodning med den angivne besked til angivet URL.
    public async Task TilføjBesked(Msg_board msg)
    {
        await HttpClient.PostAsJsonAsync($"{Host}/api/beskeder/tilfoejbesked", msg);
    }
}
