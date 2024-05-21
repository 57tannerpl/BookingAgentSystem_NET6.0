using SettlementBookingAgent_NET6._0.API.Models;
using System.ComponentModel.DataAnnotations;

namespace SettlementBookingAgent_NET6._0.API.DTOs
{
    public class BookingDto
    {
        [Required(ErrorMessage = "BookingTime is required.")]
        [RegularExpression(@"^(0[9]|1[0-6]):(00|30)$", ErrorMessage = "Invalid booking time format or value. The booking time format should be 'HH:MM' like '09:30' and in full half-hour format (e.g., 9:00, 9:30, 10:00, etc.).")]
        public string BookingTime { get; set; }
        public string EndTime => DateTime.Parse(BookingTime).AddHours(1).ToString("HH:mm");

        [Required]
        [RegularExpression(@"^(buy|sell)$", ErrorMessage = "PurchaseType must be either 'buy' or 'sell'.")]
        public string PurchaseType { get; set; } //buy or sell ?

        [Required(ErrorMessage = "buyer/seller's Name is required.")]
        [MinLength(1, ErrorMessage = "buyer/seller's Name cannot be empty.")]
        public string ClientName { get; set; } //buyer/seller's Name
        public DateTime CreatedAt { get; set; } // Timestamp of when the booking was created

        [Required(ErrorMessage = "Organizer is required.")]
        public string Organizer { get; set; } //who makes this booking ?   

        [Required(ErrorMessage = "Attendee is required.")]
        public string Attendee { get; set; } //who attends this booking ? 
        // Additional properties can be added as needed
    }
}
