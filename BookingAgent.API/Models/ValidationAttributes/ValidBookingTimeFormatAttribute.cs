using System.ComponentModel.DataAnnotations;

namespace SettlementBookingAgent_NET6._0.API.Models.ValidationAttributes
{
    public class ValidBookingTimeFormatAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null || !(value is string))
            {
                // Value is not a string or is null
                return false;
            }

            string bookingTime = (string)value;
            TimeSpan time;

            // Validate the format using TimeSpan.TryParseExact
            return TimeSpan.TryParseExact(bookingTime, "HH:mm", null, out time);
        }
    }
}
