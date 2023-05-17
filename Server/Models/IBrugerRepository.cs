using System;
using MiljøFestivalv2.Shared;

namespace Server.Models
{
	public interface IBrugerRepository
	{
		Task<IEnumerable<Bruger>> HentAlleFrivillige();
		Task TilføjFrivillig(Bruger bruger);
		Task<Bruger> HentBrugerMedBrugernavnOgPassword(string brugernavn, string password);
    }
}

