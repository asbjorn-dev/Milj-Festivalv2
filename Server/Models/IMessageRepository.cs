using System;
using MiljøFestivalv2.Shared;

namespace Server.Models
{
    public interface IMessageRepository
    {   
        // Metode til at hente alle beskeder fra Databasen
        Task<IEnumerable<Msg_board>> HentAlleBeskeder();

        // Metode til at tilføje en besked til databasen
        Task TilføjBesked(Msg_board msg);
    }
}
