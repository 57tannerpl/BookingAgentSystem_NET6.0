using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace SettlementBookingAgent_NET6._0.API.Middlewares
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // Default to 500 Internal Server Error
            var ApiSetting = Extensions.XMLReadingSettings();
            int MaxReservation = ApiSetting.MaximumReservation;
            //customize the error response based on the exception type
            if (exception.Message == "Maximum reservation number " + MaxReservation + " at the same time reached")
            {
                code = HttpStatusCode.Conflict;
            }
            if (exception.Message == "Booking time must be between " + ApiSetting.BookingStartTime + ":00 and " + ApiSetting.BookingEndTime + ":00.")
            {
                code = HttpStatusCode.BadRequest;
            }
            if (exception.Message == "Booking time must be after now")
            {
                code = HttpStatusCode.BadRequest;
            }
            var result = JsonConvert.SerializeObject(new {
                HttpStatusCode = code,
                error = exception.Message,                
                });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
