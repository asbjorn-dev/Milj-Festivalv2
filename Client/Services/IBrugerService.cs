using System;
using Shared;

namespace Client.Services
{
    public interface IBrugerService 
    {
        Task<Bruger[]> HentAlleFrivillige();
    }
}
