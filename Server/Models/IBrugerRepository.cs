using System;
using MiljøFestivalv2.Shared;

namespace Server.Models
{
	public interface IBrugerRepository
	{
		Task<IEnumerable<Bruger>> HentAlleFrivillige();
		Task TilføjFrivillig(Bruger bruger);
		Login HentBrugerMedBrugernavnOgPassword(string Brugernavn, string Password);
        Task SkiftAktivStatus(int FrivilligId);
        Task SkiftBlacklistStatus(int FrivilligId);
		Task<Bruger> HentBrugerSingle(int bruger_id);
		Task UpdateBruger(Bruger updatedBruger);
	}
}

