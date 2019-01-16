using System;
using ServelessParking.Domain;
using ServerlessParking.Interfaces;

namespace ServerlessParking.Repositories
{
    public class ParkingGarageRepository : IParkingGarageRepository
    {
        public ParkingGarage FindByNameAndDate(string name, DateTime date)
        {
            throw new NotImplementedException();
        }

        public void Add(ParkingGarage parkingGarage)
        {
            throw new NotImplementedException();
        }

        public void Update(ParkingGarage parkingGarage)
        {
            throw new NotImplementedException();
        }

        public void Remove(ParkingGarage parkingGarage)
        {
            throw new NotImplementedException();
        }
    }
}
