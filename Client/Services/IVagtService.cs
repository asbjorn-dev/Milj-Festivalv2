using System;
using Shared;

namespace Client.Services
{
	public interface IVagtService
	{
        Task<Vagt[]> HentAlleVagter();
    }
}

