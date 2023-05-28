using MiljøFestivalv2.Shared;

// Definition af interface IBrugerService. Klasser, der implementerer dette interface, skal implementere alle deklarerede metoder.
public interface IBrugerService
{
    Task<Bruger[]> HentAlleFrivillige();

    Task TilføjBruger(Bruger bruger);

    Task<Bruger> Login(Login BrugerInfo);

    Task SkiftAktivStatus(int BrugerId);

    Task SkiftBlacklistStatus(int BrugerId);

    Task<Bruger> HentBrugerSingle(int BrugerId);

    Task UpdateBruger(Bruger UpdatedBruger);
}
