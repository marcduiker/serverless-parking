using System.Threading.Tasks;

namespace ServerlessParking.Services.ParkingGarageGate
{
    public interface IParkingGarageGateService
    {
        Task DisplayMessage(string message);

        Task OpenGateAsync(string parkingGarageName);
    }
}
