﻿using System;
using MiljøFestivalv2.Shared;

namespace Client.Services
{
    public interface IBrugerService 
    {
        Task<Bruger[]> HentAlleFrivillige();
    }
}
