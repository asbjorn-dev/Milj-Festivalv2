using System;
using MiljøFestivalv2.Shared;

namespace Client.Services
{
	public interface IVagtService
	{
        Task<Vagt[]> HentAlleVagter();
        Task DeleteVagt(int vagt_id);
        Task TilføjVagt(Vagt vagt);
        Task OpdaterVagt(Vagt OpdateretVagt);
		Task<Vagt> HentVagtSingle(int vagt_id);

	}

}

