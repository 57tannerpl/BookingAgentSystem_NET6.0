namespace SettlementBookingAgent_NET6._0.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Role UserRole { get; set; } // Reference to the user's role
        public enum Role
        {
            Solicitor,
            Broker,
            Client
        }
    }
}
