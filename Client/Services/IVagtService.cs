using System;
using MiljøFestivalv2.Shared;

namespace Client.Services
{
	public interface IVagtService
	{
        Task<Vagt[]> HentAlleVagter();
        Task DeleteVagt(int vagt_id);
<<<<<<< HEAD
        Task TilføjVagt(Vagt vagt);
    }
=======
        Task OpdaterVagt(Vagt OpdateretVagt);
		Task<Vagt> HentVagtSingle(int vagt_id);

	}
>>>>>>> c4063f873bf0f4c2fa57cee4a23442eb2b34840e
}

