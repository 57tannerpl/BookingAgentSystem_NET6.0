using Microsoft.EntityFrameworkCore;
using SettlementBookingAgent_NET6._0.API.Data;
using SettlementBookingAgent_NET6._0.API.DTOs;
using SettlementBookingAgent_NET6._0.API.Interfaces;
using SettlementBookingAgent_NET6._0.API.Models;
using static SettlementBookingAgent_NET6._0.API.Models.User;

namespace SettlementBookingAgent_NET6._0.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        /*
        private readonly SBADbContext _context;
        public UserRepository(SBADbContext context)
        {
            _context = context;
        }

        public async Task<string> GetUserIdByNameAsync(string name)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == name);
            return user?.Id.ToString();

        }*/

        //user memory users
        private List<User> _users;
        public UserRepository()
        {
            _users = new List<User>
            {
            new User { Id = 1, Name = "John Doe", Email = "john@example.com", UserRole = Role.Solicitor },
            new User { Id = 2, Name = "Jane Smith", Email = "jane@example.com", UserRole = Role.Solicitor  }
             };
        }

        public async Task<string> GetUserIdByNameAsync(string name,string role)
        {
            var user = _users.FirstOrDefault(u => u.Name == name);
            if(user == null) 
            {
                if(role == "Attendee")
                {
                    throw new ArgumentException("Attendee does not existed, Please check the registed Name");
                }
                if (role == "Organizer")
                {
                    throw new ArgumentException("Organizer not existed, Please check the registed Name");
                }
                if (role == "Broker")
                {
                    throw new ArgumentException("Broker does not existed, Please check the registed Name");
                }

            }
            return user?.Id.ToString();

        }
    }
}

