using MiljøFestivalv2.Shared;

// Definition af interface IBrugerService. Klasser, der implementerer dette interface, skal implementere alle deklarerede metoder.
public interface IBrugerService
{
    Task<Bruger[]> HentAlleFrivillige();

    Task TilføjBruger(Bruger bruger);

    Task<Bruger> Login(Login brugerinfo);

    Task SkiftAktivStatus(int bruger_id);

    Task SkiftBlacklistStatus(int bruger_id);

    Task<Bruger> HentBrugerSingle(int bruger_id);

    Task UpdateBruger(Bruger updatedBruger);
}
