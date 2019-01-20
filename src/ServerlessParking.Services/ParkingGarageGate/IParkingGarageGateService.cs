using System.Threading.Tasks;

namespace ServerlessParking.Services.ParkingGarageGate
{
    public interface IParkingGarageGateService
    {
        Task OpenGateAsync(string parkingGarageName);
    }
}
