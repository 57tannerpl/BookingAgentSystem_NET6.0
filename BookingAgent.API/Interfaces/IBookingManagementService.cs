using SettlementBookingAgent_NET6._0.API.DTOs;
using SettlementBookingAgent_NET6._0.API.Models;

namespace SettlementBookingAgent_NET6._0.API.Interfaces
{
    public interface IBookingManagementService
    {
        Task<bool> ValidateBookingInfoAsync(Booking booking);
        Task SendBookingConfirmationEmailAsync(Booking booking);
        Task AddBookingAsync(Booking booking);

    }
}
