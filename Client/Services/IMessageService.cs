using System;
using MiljøFestivalv2.Shared;

namespace Client.Services
{
    public interface IMessageService
    {
        Task<Msg_board[]> HentAlleBeskeder();
    }
}
