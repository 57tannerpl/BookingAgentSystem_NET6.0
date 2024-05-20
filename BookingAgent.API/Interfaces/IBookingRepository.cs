using SettlementBookingAgent_NET6._0.API.DTOs;
using SettlementBookingAgent_NET6._0.API.Models;

namespace SettlementBookingAgent_NET6._0.API.Interfaces
{
    public interface IBookingRepository
    {
        Task AddBookingAsync(Booking booking);
        Task<IEnumerable<Booking>> GetBookingsAsync();
        Task<bool> ValidateBasicBookingAsync(Booking booking);
        Task<List<Booking>> GetBookingsByTimeAsync(DateTime bookingTime);
        // Add other methods as needed
    }
}
