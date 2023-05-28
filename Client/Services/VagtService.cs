// VagtService klasse implementerer IVagtService interfacet
using MiljøFestivalv2.Shared;
using System.Net.Http.Json;

public class VagtService : IVagtService
{
    // Deklarerer en privat, readonly HttpClient, der vil blive brugt til at foretage HTTP-anmodninger
    private readonly HttpClient HttpClient;

    // To forskellige hosts så vi kunne bytte mellem localhost og hosten til siden vi skulle deploy på
    public string Host = "https://localhost:7155";
    //public string Host = "https://xn--miljfestivalgruppe2-y7b.azurewebsites.net";

    // Constructor, der tager en HttpClient som parameter og initialiserer den private HttpClient med den.
    public VagtService(HttpClient HttpClient)
    {
        this.HttpClient = HttpClient;
    }

    public Task<Vagt[]> HentAlleVagter()
    {
        var Resultat = HttpClient.GetFromJsonAsync<Vagt[]>($"{Host}/api/vagter/hentallevagter");
        return Resultat;
    }

    public async Task DeleteVagt(int VagtId)
    {
        await HttpClient.DeleteAsync($"{Host}/api/vagter/{VagtId}");
    }

    public async Task<Vagt> HentVagtSingle(int VagtId)
    {
        return await HttpClient.GetFromJsonAsync<Vagt>($"{Host}/api/vagter/hentvagtsingle/{VagtId}");
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
