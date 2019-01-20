using System.Threading.Tasks;

namespace ServerlessParking.Services.ParkingGarageGate
{
    public class ParkingGarageGateService : IParkingGarageGateService
    {
        public async Task OpenGateAsync(string parkingGarageName)
        {
            // Simulate successfull sending of 'open gate' request.
            await Task.CompletedTask;
        }
    }
}
