namespace ServerlessParking.Application.Orchestrations.Models
{
    public sealed class ParkingOrchestrationResponse
    {
        public string Message { get; set; }

        public bool GateOpen { get; set; }
    }
}
