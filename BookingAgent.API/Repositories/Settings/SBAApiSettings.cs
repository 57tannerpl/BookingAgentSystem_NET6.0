using System.Xml.Serialization;

namespace SettlementBookingAgent_NET6._0.API.Repositories.Settings
{
    public class SBAApiSettings
    {
        [XmlElement("MaximumReservation")]
        public int MaximumReservation { get; set; }
        public int BookingStartTime { get; set; }
        public int BookingEndTime { get; set; }
        public int BookingDueration { get; set; }
        
    }
}
