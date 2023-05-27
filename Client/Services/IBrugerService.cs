using MiljøFestivalv2.Shared;

// Definition af interface IBrugerService. Klasser, der implementerer dette interface, skal implementere alle deklarerede metoder.
public interface IBrugerService
{
    // Metode til at hente alle frivillige. Den skal returnere et array af Bruger objekter.
    Task<Bruger[]> HentAlleFrivillige();

    // Metode til at tilføje en ny bruger. Den tager et Bruger objekt som argument.
    Task TilføjBruger(Bruger bruger);

    // Metode til at logge ind. Den tager et Login objekt som argument og skal returnere et Bruger objekt.
    Task<Bruger> Login(Login brugerinfo);

    // Metode til at skifte en brugers aktive status. Den tager et bruger_id som argument.
    Task SkiftAktivStatus(int bruger_id);

    // Metode til at skifte en brugers blacklist status. Den tager et bruger_id som argument.
    Task SkiftBlacklistStatus(int bruger_id);

    // Metode til at hente en enkelt bruger. Den tager et bruger_id som argument og skal returnere et Bruger objekt.
    Task<Bruger> HentBrugerSingle(int bruger_id);

    // Metode til at opdatere en bruger. Den tager et opdateret Bruger objekt som argument.
    Task UpdateBruger(Bruger updatedBruger);
}
