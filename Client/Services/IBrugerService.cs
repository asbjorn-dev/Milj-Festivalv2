﻿using System;
using MiljøFestivalv2.Shared;

namespace Client.Services
{
    public interface IBrugerService 
    {
        Task<Bruger[]> HentAlleFrivillige();
        Task TilføjBruger(Bruger bruger);
        Task<Bruger> Login(Bruger bruger);
    }
}
