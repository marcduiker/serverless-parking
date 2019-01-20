using System;
using System.Threading.Tasks;

namespace ServerlessParking.Repositories.LicensePlate
{
    public class LicencePlateRegistrationRepository : ILicensePlateRegistrationRepository
    {
        public Task<Domain.LicensePlateRegistration> GetByNumberAsync(string licensePlateNumber)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Domain.LicensePlateRegistration licensePlateRegistration)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Domain.LicensePlateRegistration licenplate)
        {
            throw new NotImplementedException();
        }
    }
}
