using System;
using MiljøFestivalv2.Shared;

namespace Client.Services
{
	public interface IVagtService
	{
        Task<Vagt[]> HentAlleVagter();
    }
}

