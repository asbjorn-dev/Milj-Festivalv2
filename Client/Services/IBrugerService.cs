using System;
using MiljøFestivalv2.Shared;

namespace Client.Services
{
    public interface IBrugerService 
    {
        Task<Bruger[]> HentAlleFrivillige();
        Task TilføjBruger(Bruger bruger);
        Task<Login> Login(Login brugerinfo);
        Task SkiftAktivStatus(int bruger_id);
        Task SkiftBlacklistStatus(int bruger_id);
    }
}
