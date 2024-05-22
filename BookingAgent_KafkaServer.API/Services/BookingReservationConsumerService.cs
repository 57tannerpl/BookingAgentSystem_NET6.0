using BookingAgent_KafkaProducer.API.Interfaces;
using Confluent.Kafka;
using static Confluent.Kafka.ConfigPropertyNames;

namespace BookingAgent_KafkaProducer.API.Services
{
    public class BookingReservationConsumerService: IHostedService
    {
        private readonly ILogger<BookingReservationProducerService> _logger;

        private readonly IConsumer<Ignore, string> _consumer;

        public BookingReservationConsumerService(ILogger<BookingReservationProducerService> logger)
        {
            _logger = logger;
            var config = new ConsumerConfig
            {
                BootstrapServers = "127.0.0.1:9092",
                GroupId = "test-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        }

        public async Task ProcessKafkaMessage()
        {
            try
            {
                var message = "check here";

                _logger.LogInformation($"Received inventory update: {message}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error processing Kafka message: {ex.Message}");
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _consumer.Subscribe("test-topic");

            Task.Run(() => ProcessKafkaMessage());

            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _consumer.Close();
            await Task.CompletedTask;
        }
    }
}
