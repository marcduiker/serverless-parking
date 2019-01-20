namespace ServerlessParking.Application.ConfirmParking.Models
{
    public class ConfirmParkingResponse
    {
        public ConfirmParkingResponse(string parkingGarageName, bool isSuccess)
        {
            ParkingGarageName = parkingGarageName;
            IsSuccess = isSuccess;
        }

        public string ParkingGarageName { get; set; }

        public bool IsSuccess { get; set; }
    }
}
