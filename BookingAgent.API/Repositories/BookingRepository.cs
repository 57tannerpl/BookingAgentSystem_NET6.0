using SettlementBookingAgent_NET6._0.API.Data;
using SettlementBookingAgent_NET6._0.API.DTOs;
using SettlementBookingAgent_NET6._0.API.Interfaces;
using SettlementBookingAgent_NET6._0.API.Models;

namespace SettlementBookingAgent_NET6._0.API.Repositories
{
    public class BookingRepository : IBookingRepository
    {

        private readonly SBADbContext _context;
        private readonly IUserRepository _userRepository;
        public BookingRepository(SBADbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }
        public async Task<Booking> AddBookingAsync(BookingDto bookingDto)
        {
            if (await ValidateBasicBookingAsync(bookingDto))
            {
                
                var newBooking = await CreateNewBooking(bookingDto);
                _context.Bookings.Add(newBooking);
                _context.SaveChanges();
                return await Task.FromResult(newBooking);
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
            

            // Validate that the booking time is between 9:00 and 16:00            
            if (DateTime.Parse(bookingDto.BookingTime).TimeOfDay < startHour || DateTime.Parse(bookingDto.BookingTime).TimeOfDay >= endHour)
            {
                throw new ArgumentException("Booking time must be between " + ApiSetting.BookingStartTime + ":00 and " + ApiSetting.BookingEndTime + ":00.");
            }

            return await Task.FromResult(true);
        }

        private async Task<Booking> CreateNewBooking(BookingDto bookingDto)
        {
            //validate the purchasetype
            if (!Enum.TryParse<PurchaseType>(bookingDto.PurchaseType, true, out var purchaseType))
            {
                throw new ArgumentException("Invalid PurchaseType. Must be either 'buy' or 'sell'.");
            }

            var organizerId = await _userRepository.GetUserIdByNameAsync(bookingDto.Organizer, "Organizer");
            var attendeeId = await _userRepository.GetUserIdByNameAsync(bookingDto.Attendee, "Attendee");
            return new Booking
            {
                BookingId = Guid.NewGuid(), // Assigning a new Guid for the booking id
                ClientName = bookingDto.ClientName,
                BookingTime = bookingDto.BookingTime,
                OrganizerId = organizerId,
                AttendeeId = attendeeId,
                CreatedAt = bookingDto.CreatedAt,
                PurchaseType = purchaseType,//PurchaseType = bookingDto.PurchaseType,
            };
        }
    }
}

