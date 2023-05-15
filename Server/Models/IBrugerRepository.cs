using System;
using Shared;

namespace Server.Models
{
	public interface IBrugerRepository
	{
		Task<IEnumerable<Bruger>> HentAlleFrivillige();
	}
}

