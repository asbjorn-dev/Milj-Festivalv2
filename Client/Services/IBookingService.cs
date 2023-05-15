using System;
using MiljøFestivalv2.Shared;

namespace Client.Services
{
    public interface IBookingService
    {
        Task<Booking[]> HentAlleBookinger();
    }
}
