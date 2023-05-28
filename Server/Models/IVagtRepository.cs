using System;
using MiljøFestivalv2.Shared;

namespace Server.Models
{
    public interface IVagtRepository
    {
        Task<IEnumerable<Vagt>> HentAlleVagter();

        Task DeleteVagt(int VagtId);

        Task TilføjVagt(Vagt vagt);

        Task OpdaterVagt(Vagt OpdateretVagt);

        Task<Vagt> HentVagtSingle(int VagtId);
    }
}