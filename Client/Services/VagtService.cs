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

    // Henter alle vagter fra serveren
    public Task<Vagt[]> HentAlleVagter()
    {
        var Resultat = HttpClient.GetFromJsonAsync<Vagt[]>($"{Host}/api/vagter/hentallevagter");
        return Resultat;
    }

    // Sletter en specifik vagt fra serveren
    public async Task DeleteVagt(int vagt_id)
    {
        await HttpClient.DeleteAsync($"{Host}/api/vagter/{vagt_id}");
    }

    // Henter en specifik vagt fra serveren
    public async Task<Vagt> HentVagtSingle(int vagt_id)
    {
        return await HttpClient.GetFromJsonAsync<Vagt>($"{Host}/api/vagter/hentvagtsingle/{vagt_id}");
    }

    // Opdaterer en specifik vagt på serveren
    public async Task OpdaterVagt(Vagt OpdateretVagt)
    {
        await HttpClient.PutAsJsonAsync($"{Host}/api/vagter/opdatervagt/{OpdateretVagt.vagt_id}", OpdateretVagt);
    }

    // Tilføjer en vagt til serveren
    public async Task TilføjVagt(Vagt vagt)
    {
        await HttpClient.PostAsJsonAsync($"{Host}/api/vagter/tilfoejvagt", vagt);
    }
}
