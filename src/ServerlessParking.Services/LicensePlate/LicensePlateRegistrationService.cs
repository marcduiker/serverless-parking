using System.Threading.Tasks;
using ServerlessParking.Domain;
using ServerlessParking.Repositories.LicensePlate;

namespace ServerlessParking.Services.LicensePlate
{
    public class LicensePlateRegistrationService : ILicensePlateRegistrationService
    {
        private readonly ILicensePlateRegistrationRepository _repository;

        public LicensePlateRegistrationService(ILicensePlateRegistrationRepository repository = null)
        {
            _repository = repository ?? new LicencePlateRegistrationRepository();
        }

        public async Task<LicensePlateRegistration> GetAsync(string licensePlateNumber)
        {
            var licensePlate = await _repository.GetByNumberAsync(licensePlateNumber);

            return licensePlate;
        }
    }
}
