using System;
using Shared;

namespace Client.Services
{
    public interface IBookingService
    {
        Task<Booking[]> HentAlleBookinger();
    }
}
