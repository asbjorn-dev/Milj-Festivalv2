﻿using System;
using MiljøFestivalv2.Shared;

namespace Server.Models
{
	public interface IVagtRepository
	{
        Task<IEnumerable<Vagt>> HentAlleVagter();
        Task DeleteVagt(int vagt_id);
        Task TilføjVagt(Vagt vagt);
    }
}

