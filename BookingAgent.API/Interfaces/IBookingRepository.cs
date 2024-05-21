using SettlementBookingAgent_NET6._0.API.DTOs;
using SettlementBookingAgent_NET6._0.API.Models;

namespace SettlementBookingAgent_NET6._0.API.Interfaces
{
    public interface IBookingRepository
    {
        Task<Booking> AddBookingAsync(BookingDto bookingDto);
        Task<IEnumerable<Booking>> GetBookingsAsync();
        Task<bool> ValidateBasicBookingAsync(BookingDto bookingDto);
        Task<List<Booking>> GetBookingsByTimeAsync(DateTime bookingTime);
        // Add other methods as needed
    }
}
