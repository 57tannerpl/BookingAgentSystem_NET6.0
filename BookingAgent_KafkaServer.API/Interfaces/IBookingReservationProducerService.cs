namespace BookingAgent_KafkaProducer.API.Interfaces
{
    public interface IBookingReservationProducerService
    {
        Task ProduceBookingReservationMessageAsync(string topic, string message);
    }
}
