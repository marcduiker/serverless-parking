namespace ServerlessParking.Models
{
    public class DetermineParkingOutcomeResult
    {
        public DetermineParkingOutcomeResult()
        {
            ParkingClientResult = new ParkingClientResult();
        }

        public string ActivityFunction { get; set; }

        public object ActivityData { get; set; }

        public ParkingClientResult ParkingClientResult { get; set; }
    }
}
