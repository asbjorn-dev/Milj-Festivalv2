using System;
using Shared;

namespace Server.Models
{
	public interface IVagtRepository
	{
        Task<IEnumerable<Vagt>> HentAlleVagter();
    }
}

