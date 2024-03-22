using MiljøFestivalv2.Shared;
using System.Net.Http.Json;

// Definerer klassen MessageService, som implementerer IMessageService interfacet
public class MessageService : IMessageService
{
    // Deklarerer en privat, readonly HttpClient, der vil blive brugt til at foretage HTTP-anmodninger
    private readonly HttpClient HttpClient;

    // To forskellige hosts så vi kunne bytte mellem localhost og hosten til siden vi skulle deploy på
    //public string Host = "https://localhost:7155";
    public string Host = "https://festivalmiljo.azurewebsites.net";

    // Constructor, der tager en HttpClient som parameter og initialiserer den private HttpClient med den.
    public MessageService(HttpClient HttpClient)
    {
        this.HttpClient = HttpClient;
    }

    public Task<Msg_board[]> HentAlleBeskeder()
    {
        var Resultat = HttpClient.GetFromJsonAsync<Msg_board[]>($"{Host}/api/beskeder/hentallebeskeder");
        return Resultat;
    }

    public async Task TilføjBesked(Msg_board Msg)
    {
        await HttpClient.PostAsJsonAsync($"{Host}/api/beskeder/tilfoejbesked", Msg);
    }
}
