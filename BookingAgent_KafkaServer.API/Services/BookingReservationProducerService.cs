using BookingAgent_KafkaProducer.API.Interfaces;
using Confluent.Kafka;
using Newtonsoft.Json;
using System.Text.Json;

namespace BookingAgent_KafkaProducer.API.Services
{
    public class BookingReservationProducerService : IBookingReservationProducerService
    {
        private readonly ILogger<BookingReservationProducerService> _logger;
        private readonly IProducer<string, string> _producer;

        public BookingReservationProducerService(ILogger<BookingReservationProducerService> logger)
        {
            _logger = logger;
            var producerConfig = new ProducerConfig { BootstrapServers = "localhost:9092" };
            _producer = new ProducerBuilder<string, string>(producerConfig).Build();

        }
        public async Task ProduceBookingReservationMessageAsync(string topic, string message="okokok")
        {
            var deliveryReport = await _producer.ProduceAsync(topic, new Message<string, string> { Value = message });
            Console.WriteLine($"Message delivered to topic: {deliveryReport.Topic}, partition: {deliveryReport.Partition}, offset: {deliveryReport.Offset}");

        }
    }
}
