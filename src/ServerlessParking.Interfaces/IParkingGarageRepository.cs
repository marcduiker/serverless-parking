using System;
using System.Threading.Tasks;
using ServerlessParking.Domain;

namespace ServerlessParking.Interfaces
{
    public interface IParkingGarageRepository
    {
        Task<ParkingGarage> FindByNameAndDateAsync(string name, DateTime date);

        Task AddAsync(ParkingGarage parkingGarage);

        Task UpdateAsync(ParkingGarage parkingGarage);

        Task RemoveAsync(ParkingGarage parkingGarage);
    }
}
