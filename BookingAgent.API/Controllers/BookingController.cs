using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // Add this namespace
using SettlementBookingAgent_NET6._0.API.DTOs;
using SettlementBookingAgent_NET6._0.API.Models;
using SettlementBookingAgent_NET6._0.API.Interfaces;
using SettlementBookingAgent_NET6._0.API.Repositories;

namespace SettlementBookingAgent_NET6._0.API.Controllers{
    
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ILogger<BookingController> _logger;

        public BookingController(IBookingRepository bookingRepository,
                                 ILogger<BookingController> logger)
        {
            _bookingRepository = bookingRepository;
            _logger = logger;
        }

        //GET /bookings
        [HttpGet]
        public async Task<ActionResult> GetBookingsASync()
        {
            // Return List<BookingDTO> for response
            var bookings = (await _bookingRepository.GetBookingsAsync());
            
            if (!bookings.Any())
            {
                return Ok(new ApiReturnObject<object>(null, StatusCodes.Status200OK, true, "No booking yet."));

            }
            _logger.LogInformation($"{DateTime.UtcNow.ToString("hh:mm:ss")}: Retrieved all {bookings.Count()} bookings");
            
            return Ok(new ApiReturnObject<object>(new { bookings }, StatusCodes.Status200OK, true, "Booking Retrived"));

        }

        // Post /Booking
        [HttpPost]
        [ActionName("AddBookingAsync")]
        public async Task<ActionResult> AddBookingAsync(BookingDto bookingDto)
        {
            Booking newBooking = await _bookingRepository.AddBookingAsync(bookingDto);
            _logger.LogInformation("Adding booking: {@Booking}", newBooking);
            return CreatedAtAction(nameof(AddBookingAsync),
                                   new { newBooking.BookingId },
                                   new ApiReturnObject<object>(new { newBooking.BookingId }, StatusCodes.Status200OK, true, "Booking successed."));

        }
    }
}
