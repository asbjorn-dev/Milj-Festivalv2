using MiljøFestivalv2.Shared;

// Definition af interface IVagtService. Klasser, der implementerer dette interface, skal implementere alle deklarerede metoder.
public interface IVagtService
{
    // Metode til at hente alle vagter. Den skal returnere et array af Vagt objekter.
    Task<Vagt[]> HentAlleVagter();

    // Metode til at slette en vagt baseret på en given id.
    Task DeleteVagt(int vagt_id);

    // Metode til at tilføje en vagt. Den tager et Vagt objekt som argument.
    Task TilføjVagt(Vagt vagt);

    // Metode til at opdatere en vagt. Den tager et Vagt objekt som argument.
    Task OpdaterVagt(Vagt OpdateretVagt);

    // Metode til at hente en enkelt vagt baseret på dens id. Den returnerer et Vagt objekt.
    Task<Vagt> HentVagtSingle(int vagt_id);
}
