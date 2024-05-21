namespace SettlementBookingAgent_NET6._0.API.Models
{
    public enum RoleType
    {
        Solicitor,
        Broker,
        Client
    }

    public class Role
    {
        public int Id { get; set; }
        public RoleType Type { get; set; }

        public Role(int id, RoleType type)
        {
            Id = id;
            Type = type;
        }
    }
}
