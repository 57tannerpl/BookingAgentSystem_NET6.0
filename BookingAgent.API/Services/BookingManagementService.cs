using SettlementBookingAgent_NET6._0.API.Controllers;
using SettlementBookingAgent_NET6._0.API.DTOs;
using SettlementBookingAgent_NET6._0.API.Interfaces;
using SettlementBookingAgent_NET6._0.API.Models;

namespace SettlementBookingAgent_NET6._0.API.Services
{
    public class BookingManagementService : IBookingManagementService
    {
        private readonly IBookingRepository _bookingRepository;
        public BookingManagementService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<bool> ValidateBookingInfoAsync(Booking booking)
        {
            // Validation logic goes here
            if (booking == null)
            {
                return false;
            }

            if (DateTime.Parse(booking.BookingTime) <= DateTime.UtcNow)
            {
                return false;
            }

            var ApiSetting = Extensions.XMLReadingSettings();
            int MaxReservation = ApiSetting.MaximumReservation;

            // Check if booking time is within valid hours
            var startHour = new TimeSpan(ApiSetting.BookingStartTime, 0, 0);
            var endHour = new TimeSpan(ApiSetting.BookingEndTime, 0, 0);
            if (DateTime.Parse(booking.BookingTime).TimeOfDay < startHour || DateTime.Parse(booking.BookingTime).TimeOfDay >= endHour)
            {
                throw new ArgumentException("Booking time must be between " + ApiSetting.BookingStartTime + ":00 and " + ApiSetting.BookingEndTime + ":00.");
            }


            // Check if booking time slot is available
            var existingBookings = await _bookingRepository.GetBookingsByTimeAsync(DateTime.Parse(booking.BookingTime));
            if (existingBookings.Count >= MaxReservation)
            {
                throw new ArgumentException("Maximum reservation number " + MaxReservation + " at the same time reached");
            }

            // Additional validation rules as needed
            return true;
        }

        public async Task AddBookingAsync(Booking booking)
        {
            await ValidateBookingInfoAsync(booking);
            await _bookingRepository.AddBookingAsync(booking);
            await SendBookingConfirmationEmailAsync(booking);
        }

        public async Task SendBookingConfirmationEmailAsync(Booking booking)
        {
            // Construct the email message and send it using the EmailService
        }
    }
}
