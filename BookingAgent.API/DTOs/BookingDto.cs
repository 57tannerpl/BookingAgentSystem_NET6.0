using SettlementBookingAgent_NET6._0.API.Models;
using System.ComponentModel.DataAnnotations;

namespace SettlementBookingAgent_NET6._0.API.DTOs
{
    public class BookingDto
    {
        [Required(ErrorMessage = "BookingTime is required.")]
        [RegularExpression(@"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$", ErrorMessage = "Invalid booking time format.")]
        public string BookingTime { get; set; }

        [Required(ErrorMessage = "Organizer is required.")]
        [MinLength(1, ErrorMessage = "Organizer cannot be empty.")]
        public string Organizer { get; set; } //who makes this booking ?
        public string Attendee { get; set; } //who attends this booking ? 

        public string PurchaseType { get; set; } //buy or sell ?

        [Required(ErrorMessage = "buyer/seller's Name is required.")]
        [MinLength(1, ErrorMessage = "buyer/seller's Name cannot be empty.")]
        public string ClientName { get; set; } //buyer/seller's Name
        public DateTime CreatedAt { get; set; } // Timestamp of when the booking was created


        // Additional properties can be added as needed
    }
}
