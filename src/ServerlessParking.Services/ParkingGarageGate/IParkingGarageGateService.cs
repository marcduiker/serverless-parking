using System.Threading.Tasks;
using ServerlessParking.Services.ParkingGarageGate.Models;

namespace ServerlessParking.Services.ParkingGarageGate
{
    public interface IParkingGarageGateService
    {
        Task DisplayMessage(DisplayMessageRequest request);

        Task OpenGateAsync(string parkingGarageName);
    }
}
