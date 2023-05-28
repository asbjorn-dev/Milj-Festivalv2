using MiljøFestivalv2.Shared;

// Definition af interface IVagtService. Klasser, der implementerer dette interface, skal implementere alle deklarerede metoder.
public interface IVagtService
{
    Task<Vagt[]> HentAlleVagter();

    Task DeleteVagt(int vagt_id);

    Task TilføjVagt(Vagt vagt);

    Task OpdaterVagt(Vagt OpdateretVagt);

    Task<Vagt> HentVagtSingle(int vagt_id);
}
