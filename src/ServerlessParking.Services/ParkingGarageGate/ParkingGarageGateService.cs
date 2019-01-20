using System.Threading.Tasks;
using ServerlessParking.Services.ParkingGarageGate.Models;

namespace ServerlessParking.Services.ParkingGarageGate
{
    public class ParkingGarageGateService : IParkingGarageGateService
    {
        public async Task DisplayMessage(DisplayMessageRequest request)
        {
            // Simulate successfull sending of a message to the gate.
            await Task.CompletedTask;
        }

        public async Task OpenGateAsync(string parkingGarageName)
        {
            // Simulate successfull sending of 'open gate' request.
            await Task.CompletedTask;
        }
    }
}
