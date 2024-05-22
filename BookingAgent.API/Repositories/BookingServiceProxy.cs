using SettlementBookingAgent_NET6._0.API.DTOs;
using SettlementBookingAgent_NET6._0.API.Interfaces;
using SettlementBookingAgent_NET6._0.API.Models;
using System.Threading;

namespace SettlementBookingAgent_NET6._0.API.Repositories
{
    public class BookingServiceProxy : IBookingRepository
    {
        private readonly ILogger<BookingServiceProxy> _logger;
        private readonly IBookingRepository _bookingRepository; // Real service implementation
        public BookingServiceProxy(IBookingRepository bookingRepository, ILogger<BookingServiceProxy> logger)
        {
            _bookingRepository = bookingRepository;
            _logger = logger;
        }

        public async Task<Booking> AddBookingAsync(BookingDto bookingDto)
        {
            // Additional logic before delegating to the real service
            // For example: Logging, validation, etc.
            if (await ValidateBookingInfoAsync(bookingDto))
            {
                Booking booking= await _bookingRepository.AddBookingAsync(bookingDto); // Delegate to the real service
                return booking;
            } else
            {
                throw new ArgumentException("AddBooking");
            }
            
        }

        public async Task<IEnumerable<Booking>> GetBookingsAsync()
        {
            return await _bookingRepository.GetBookingsAsync();
        }
        public async Task<bool> ValidateBasicBookingAsync(BookingDto bookingDto)
        {
            return await _bookingRepository.ValidateBasicBookingAsync(bookingDto);
        }
        public async Task<List<Booking>> GetBookingsByTimeAsync(DateTime bookingTime)
        {
            return await _bookingRepository.GetBookingsByTimeAsync(bookingTime);
        }
        private async Task<bool> ValidateBookingInfoAsync(BookingDto bookingDto)
        {
            bool isBasicValid = await ValidateBasicBookingAsync(bookingDto);
            if (!isBasicValid)
            {
                throw new ArgumentException("ValidateBasicBooking fail");
            }
            // Validation logic goes here
            // Check if booking time slot is available
            var ApiSetting = Extensions.XMLReadingSettings();
            int MaxReservation = ApiSetting.MaximumReservation;            
            var existingBookings = await GetBookingsByTimeAsync(DateTime.Parse(bookingDto.BookingTime));
            if (existingBookings.Count >= MaxReservation)
            {
                throw new ArgumentException("Maximum reservation number " + MaxReservation + " at the same time reached");
            }


            // Additional validation rules as needed
            return true;
        }

        public async Task SendingInvitationToAttendeesAsync(Booking booking)
        {
            try
            {
                _logger.LogInformation($"Sending invitation for booking {booking.BookingId} to attendees...");
                await Task.CompletedTask; // Replace with actual logic
                _logger.LogInformation($"Invitation for booking {booking.BookingId} sent successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while sending invitation for booking {booking.BookingId}");
                throw; // Optionally rethrow the exception if needed
            }
        }

        // Implement other proxy methods if needed
    }
}
