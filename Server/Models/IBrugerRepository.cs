﻿using System;
using MiljøFestivalv2.Shared;

namespace Server.Models
{
	public interface IBrugerRepository
	{
		Task<IEnumerable<Bruger>> HentAlleFrivillige();
	}
}

