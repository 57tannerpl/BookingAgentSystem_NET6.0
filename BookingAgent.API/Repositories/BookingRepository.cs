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
        public async Task<Booking> AddBookingAsync(BookingDto bookingDto)
        {
            if (await ValidateBasicBookingAsync(bookingDto))
            {
                if (!Enum.TryParse<PurchaseType>(bookingDto.PurchaseType, true, out var purchaseType))
                {
                    throw new ArgumentException("Invalid PurchaseType. Must be either 'buy' or 'sell'.");
                }
                var newBooking = new Booking
                {
                    BookingId = Guid.NewGuid(), // Assigning a new Guid for the booking id
                    ClientName = bookingDto.ClientName,
                    BookingTime = bookingDto.BookingTime,
                    Organizer = bookingDto.Organizer,
                    Attendee = bookingDto.Attendee,
                    PurchaseType = purchaseType,//PurchaseType = bookingDto.PurchaseType,
                };
                _context.Bookings.Add(newBooking);
                _context.SaveChanges();
                return newBooking;
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

        public async Task<bool> ValidateBasicBookingAsync(BookingDto bookingDto)
        {
            var ApiSetting = Extensions.XMLReadingSettings();
            var startHour = new TimeSpan(ApiSetting.BookingStartTime, 0, 0);
            var endHour = new TimeSpan(ApiSetting.BookingEndTime, 0, 0);

            if (bookingDto == null)
            {
                return false;
            }
            if (DateTime.Parse(bookingDto.BookingTime) <= DateTime.Now && DateTime.Now.TimeOfDay <= endHour)
            {
                throw new ArgumentException("Booking time must be after now");

            }

            //validate the purchasetype
            if (!Enum.TryParse<PurchaseType>(bookingDto.PurchaseType, true, out var purchaseType))
            {
                throw new ArgumentException("Invalid PurchaseType. Must be either 'buy' or 'sell'.");
            }

            // Validate that the booking time is between 9:00 and 16:00            
            if (DateTime.Parse(bookingDto.BookingTime).TimeOfDay < startHour || DateTime.Parse(bookingDto.BookingTime).TimeOfDay >= endHour)
            {
                throw new ArgumentException("Booking time must be between " + ApiSetting.BookingStartTime + ":00 and " + ApiSetting.BookingEndTime + ":00.");
            }

            return await Task.FromResult(true);
        }

    }
}

