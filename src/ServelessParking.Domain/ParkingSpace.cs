using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ServelessParking.Domain
{
    public class ParkingSpace
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public ParkingSpaceStatusType Status { get; set; }

        public void Reserve()
        {
            Status = ParkingSpaceStatusType.Reserved;
        }

        public void Occupy()
        {
            Status = ParkingSpaceStatusType.Occupied;
        }

        public void Free()
        {
            Status = ParkingSpaceStatusType.Free;
        }
    }
}
