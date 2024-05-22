using SettlementBookingAgent_NET6._0.API.DTOs;

namespace SettlementBookingAgent_NET6._0.API.Interfaces
{
    public interface IUserRepository
    {
        Task<string> GetUserIdByNameAsync(string name,string role);
    }
}
