namespace ServerlessParking.Services.ParkingConfirmation.Models
{
    public class ConfirmParkingResponse
    {
        public ConfirmParkingResponse(
            string parkingGarageName, 
            bool isSuccess,
            string message)
        {
            ParkingGarageName = parkingGarageName;
            IsSuccess = isSuccess;
            Message = message;
        }

        public string ParkingGarageName { get; set; }

        public bool IsSuccess { get; set; }


        public string Message { get; set; }
    }
}
