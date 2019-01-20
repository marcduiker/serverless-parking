using System;
using System.Threading.Tasks;

namespace ServerlessParking.Repositories.LicensePlate
{
    public class LicencePlateRepository : ILicensePlateRepository
    {
        public Task<Domain.LicensePlate> GetByNumberAsync(string licensePlateNumber)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Domain.LicensePlate licensePlate)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Domain.LicensePlate licenplate)
        {
            throw new NotImplementedException();
        }
    }
}
