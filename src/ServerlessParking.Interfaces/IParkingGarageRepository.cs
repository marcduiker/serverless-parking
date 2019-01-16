using System;
using ServelessParking.Domain;

namespace ServerlessParking.Interfaces
{
    public interface IParkingGarageRepository
    {
        ParkingGarage FindByNameAndDate(string name, DateTime date);

        void Add(ParkingGarage parkingGarage);

        void Update(ParkingGarage parkingGarage);

        void Remove(ParkingGarage parkingGarage);
    }
}
