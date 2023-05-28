using System;
using MiljøFestivalv2.Shared;

namespace Server.Models
{
    public interface IMessageRepository
    {   

        Task<IEnumerable<Msg_board>> HentAlleBeskeder();

        Task TilføjBesked(Msg_board Msg);
    }
}
