namespace BookingAgent_KafkaProducer.API.Interfaces
{
    public interface IBookingReservationConsumerService
    {
        Task ProcessKafkaMessage();

    }
}