using SettlementBookingAgent_NET6._0.API.Data;
using SettlementBookingAgent_NET6._0.API.DTOs;
using SettlementBookingAgent_NET6._0.API.Interfaces;
using SettlementBookingAgent_NET6._0.API.Models;

namespace SettlementBookingAgent_NET6._0.API.Repositories
{
    public class BookingRepository : IBookingRepository
    {

        private readonly SBADbContext _context;
        public BookingRepository(SBADbContext context)
        {
            _context = context;
        }
        public async Task AddBookingAsync(Booking booking)
        {
            if (await ValidateBasicBookingAsync(booking))
            {
                _context.Bookings.Add(booking);
                _context.SaveChanges();
                await Task.CompletedTask;
            }
            else
            {
                throw new ArgumentException("Invalid booking data.");
            }
        }

        public async Task<IEnumerable<Booking>> GetBookingsAsync()
        {
            var bookings = _context.Bookings.ToList();
            return await Task.FromResult(bookings);
        }

        public async Task<List<Booking>> GetBookingsByTimeAsync(DateTime bookingTime)
        {
            // Get all bookings within the specified time range
            var bookings = _context.Bookings
                .Where(b => DateTime.Parse(b.BookingTime) < bookingTime.AddHours(1) &&
                            DateTime.Parse(b.BookingTime).AddHours(1) >= bookingTime)
                .ToList();

            return await Task.FromResult(bookings);
        }

        public async Task<bool> ValidateBasicBookingAsync(Booking booking)
        {
            var ApiSetting = Extensions.XMLReadingSettings();
            var startHour = new TimeSpan(ApiSetting.BookingStartTime, 0, 0);
            var endHour = new TimeSpan(ApiSetting.BookingEndTime, 0, 0);

            if (booking == null)
            {
                return false;
            }
            if (DateTime.Parse(booking.BookingTime) <= DateTime.Now && DateTime.Now.TimeOfDay <= endHour)
            {
                throw new ArgumentException("Booking time must be after now");

            }

            // Validate that the booking time is between 9:00 and 16:00            
            if (DateTime.Parse(booking.BookingTime).TimeOfDay < startHour || DateTime.Parse(booking.BookingTime).TimeOfDay >= endHour)
            {
                throw new ArgumentException("Booking time must be between " + ApiSetting.BookingStartTime + ":00 and " + ApiSetting.BookingEndTime + ":00.");
            }

            return await Task.FromResult(true);
        }

    }
}

