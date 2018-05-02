namespace ServerlessParking.Models
{
    public sealed class ParkingClientResult
    {
        public string Message { get; set; }

        public bool GateOpen { get; set; }
    }
}
