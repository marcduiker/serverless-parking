using System;
using System.Threading.Tasks;
using ServerlessParking.Domain;

namespace ServerlessParking.Repositories.LicensePlate
{
    public class LicencePlateRegistrationRepository : ILicensePlateRegistrationRepository
    {
        public Task<LicensePlateRegistration> GetByNumberAsync(string licensePlateNumber)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(LicensePlateRegistration licensePlateRegistration)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(LicensePlateRegistration licenplate)
        {
            throw new NotImplementedException();
        }
    }
}
