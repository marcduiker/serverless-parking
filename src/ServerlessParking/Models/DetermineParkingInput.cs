namespace ServerlessParking.Models
{
    public sealed class DetermineParkingInput
    {
        public ActivityResult IsAppointment { get; set; }

        public ActivityResult IsEmployee { get; set; }

        public ActivityResult IsParkingSpotAvailable { get; set; }

        public string LicensePlate { get; set; }
    }
}
