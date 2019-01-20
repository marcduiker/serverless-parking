using System.Threading.Tasks;

namespace ServerlessParking.Services.ParkingGarageGate
{
    public class ParkingGarageGateService : IParkingGarageGateService
    {
        public async Task DisplayMessage(string message)
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
