namespace ServerlessParking.Models
{
    public class DetermineParkingResult
    {
        public DetermineParkingResult()
        {
            ClientResult = new ParkingClientResult();
        }

        public string ActivityFunction { get; set; }

        public object ActivityData { get; set; }

        public ParkingClientResult ClientResult { get; set; }
    }
}
