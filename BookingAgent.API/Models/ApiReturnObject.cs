using Microsoft.AspNetCore.Http;

namespace SettlementBookingAgent_NET6._0.API.Models
{
    public class ApiReturnObject<T>
    {
        public int Status { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }

        public ApiReturnObject()
        {
        }

        public ApiReturnObject(T data, int statusCode, bool success = true, string message = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Status = statusCode;
        }

        public ApiReturnObject(List<string> errors, int statusCode, bool success = false, string message = null)
        {
            Success = success;
            Message = message;
            Errors = errors;
            Status = statusCode;
        }
    }
}
