using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SettlementBookingAgent_NET6._0.API.Controllers;
using SettlementBookingAgent_NET6._0.API.DTOs;
using SettlementBookingAgent_NET6._0.API.Interfaces;
using SettlementBookingAgent_NET6._0.API.Models;

namespace BookingAgent.API.Tests
{
    [TestFixture]
    public class BookingControllerTests
    {
        private Mock<IBookingRepository> _mockBookingRepository;
        private Mock<ILogger<BookingController>> _mockLogger;
        private BookingController _controller;

        [SetUp]
        public void Setup()
        {
            _mockBookingRepository = new Mock<IBookingRepository>();
            _mockLogger = new Mock<ILogger<BookingController>>();
            _controller = new BookingController(_mockBookingRepository.Object, _mockLogger.Object);

        }

        [Test]
        public async Task AddBookingAsync_ValidBooking_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var bookingDto = new BookingDto
            {
                ClientName = "John Doe",
                BookingTime = "09:30",
                Organizer = "Organizer",
                Attendee = "Attendee",
                PurchaseType = "Type"
            };

            _mockBookingRepository.Setup(repo => repo.AddBookingAsync(It.IsAny<Booking>()))
                                 .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddBookingAsync(bookingDto);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result);
            var createdAtActionResult = result as CreatedAtActionResult;
            /*
            var apiReturn = createdAtActionResult.Value as ApiReturnObject<object>;
            Console.WriteLine(apiReturn.Data);*/
            Assert.AreEqual("AddBookingAsync", createdAtActionResult.ActionName);
            Assert.AreEqual(201, createdAtActionResult.StatusCode);

        }

        [Test]
        public async Task AddBookingAsync_InvalidBooking_ReturnsBadRequest()
        {
            // Arrange
            var bookingDto = new BookingDto
            {
                ClientName = "John Doe",
                BookingTime = "06:00", // Invalid booking time for the purpose of the test
                Organizer = "Organizer",
                Attendee = "Attendee",
                PurchaseType = "Type"
            };

            _mockBookingRepository.Setup(repo => repo.AddBookingAsync(It.IsAny<Booking>()))
                                  .Throws(new ArgumentException("Invalid booking request"));

            // Act
            var result = await _controller.AddBookingAsync(bookingDto);

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}