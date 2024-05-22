using BookingAgent_KafkaProducer.API.Interfaces;
using BookingAgent_KafkaProducer.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingAgent_KafkaProducer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        
        private readonly BookingReservationConsumerService _kafkaService;
        private readonly ILogger<ProducerController> _logger;

        public ConsumerController(BookingReservationConsumerService kafkaService, ILogger<ProducerController> logger)
        {
            _kafkaService = kafkaService;
            _logger = logger;
        }
        [HttpPost("start")]
        public IActionResult StartConsumer()
        {
            _kafkaService.StartAsync(default);
            return Ok("Consumer started.");
        }

        [HttpPost("stop")]
        public IActionResult StopConsumer()
        {
            _kafkaService.StopAsync(default);
            return Ok("Consumer stopped.");
        }

    }
}
