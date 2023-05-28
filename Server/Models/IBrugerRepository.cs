using MiljøFestivalv2.Shared;

namespace Server.Models
{
    public interface IBrugerRepository
    {

        Task<IEnumerable<Bruger>> HentAlleFrivillige();

        Task TilføjFrivillig(Bruger bruger);

        Bruger HentBrugerMedBrugernavnOgPassword(string Brugernavn, string Password);

        Task SkiftAktivStatus(int FrivilligId);

        Task SkiftBlacklistStatus(int FrivilligId);

        Task<Bruger> HentBrugerSingle(int BrugerId);

        Task UpdateBruger(Bruger UpdatedBruger);
    }
}
