using BookingAgent_KafkaProducer.API.Interfaces;
using BookingAgent_KafkaProducer.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookingAgent_KafkaProducer.API.Controllers
{
    public class ProducerController : ControllerBase
    {
        private readonly IBookingReservationProducerService _kafkaService;
        private readonly ILogger<ProducerController> _logger;

        public ProducerController(IBookingReservationProducerService kafkaService, ILogger<ProducerController> logger)
        {
            _kafkaService = kafkaService;
            _logger = logger;
        }

        [HttpPost("produce-kafka-message")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ProduceBookingReservationMessage([FromQuery] string topic, string message)
        {
            await _kafkaService.ProduceBookingReservationMessageAsync(topic, message);
            _logger.LogInformation($"Message produced to topic {topic}.");
            return Ok(new { Message = $"Message produced to topic {topic}." });
        }


        
    }
}
