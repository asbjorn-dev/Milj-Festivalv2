using System;
using MiljøFestivalv2.Shared;

namespace Server.Models
{
	public interface IVagtRepository
	{
        Task<IEnumerable<Vagt>> HentAlleVagter();
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

