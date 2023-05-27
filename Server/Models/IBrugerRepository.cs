using System;
using MiljøFestivalv2.Shared;

namespace Server.Models
{
    public interface IBrugerRepository
    {
        // Metode til at hente alle frivillige
        Task<IEnumerable<Bruger>> HentAlleFrivillige();

        // Metode til at tilføje en ny frivillig
        Task TilføjFrivillig(Bruger bruger);

        // Metode til at hente en bruger med brugernavn og password
        Bruger HentBrugerMedBrugernavnOgPassword(string Brugernavn, string Password);

        // Metode til at skifte aktiv status for en frivillig
        Task SkiftAktivStatus(int FrivilligId);

        // Metode til at skifte blacklist status for en frivillig
        Task SkiftBlacklistStatus(int FrivilligId);

        // Metode til at hente en enkelt bruger ved hjælp af bruger_id
        Task<Bruger> HentBrugerSingle(int bruger_id);

        // Metode til at opdatere en bruger
        Task UpdateBruger(Bruger updatedBruger);
    }
}
