using System;
using MiljøFestivalv2.Shared;

namespace Server.Models
{
    public interface IVagtRepository
    {
        // Metode til at hente alle vagter fra databasen
        Task<IEnumerable<Vagt>> HentAlleVagter();

        // Metode til at slette en vagt fra databasen ved hjælp af vagt_id
        Task DeleteVagt(int vagt_id);

        // Metode til at tilføje en ny vagt til databasen
        Task TilføjVagt(Vagt vagt);

        // Metode til at opdatere en eksisterende vagt i databasen
        Task OpdaterVagt(Vagt OpdateretVagt);

        // Metode til at hente en enkelt vagt fra databasen ved hjælp af vagt_id
        Task<Vagt> HentVagtSingle(int vagt_id);
    }
}