using Microsoft.EntityFrameworkCore;
using SettlementBookingAgent_NET6._0.API.Models;
using System.Collections.Generic;

namespace SettlementBookingAgent_NET6._0.API.Data
{
    public class SBADbContext : DbContext
    {
        public SBADbContext(DbContextOptions<SBADbContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
    }
}
