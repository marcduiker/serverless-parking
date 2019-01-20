using System;
using System.Threading.Tasks;
using ServerlessParking.Domain;
using ServerlessParking.Interfaces.Repositories;

namespace ServerlessParking.Repositories
{
    public class ParkingGarageRepository : IParkingGarageRepository
    {
        public Task<ParkingGarage> FindByNameAndDateAsync(string name, DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(ParkingGarage parkingGarage)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ParkingGarage parkingGarage)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(ParkingGarage parkingGarage)
        {
            throw new NotImplementedException();
        }
    }
}
