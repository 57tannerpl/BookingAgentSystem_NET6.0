using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SettlementBookingAgent_NET6._0.API.Models;
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
            string generalMessage = "An error occurred while processing your request.";
            string detailedMessage = exception.Message;

            //customize the error response based on the exception type
            if (exception.Message == "Maximum reservation number " + MaxReservation + " at the same time reached")
            {
                code = HttpStatusCode.Conflict;
                generalMessage = "Conflict error";
            }
            if (exception.Message == "Booking time must be between " + ApiSetting.BookingStartTime + ":00 and " + ApiSetting.BookingEndTime + ":00.")
            {
                code = HttpStatusCode.BadRequest;
                generalMessage = "Bad request error";
            }
            if (exception.Message == "Booking time must be after now")
            {
                code = HttpStatusCode.BadRequest;
                generalMessage = "Bad request error";
            }

            var response = new ApiReturnObject<object>
            {
                Status = (int)code,
                Success = false,
                Message = generalMessage,
                Errors = new List<string>
                {
                    detailedMessage
                }
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            var jsonResponse = JsonConvert.SerializeObject(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
