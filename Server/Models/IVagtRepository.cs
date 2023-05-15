using System;
using MiljøFestivalv2.Shared;

namespace Server.Models
{
	public interface IVagtRepository
	{
        Task<IEnumerable<Vagt>> HentAlleVagter();
    }
}

