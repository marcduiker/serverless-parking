using ServerlessParking.Domain;

namespace ServerlessParking.Interfaces
{
    public interface ILicenplateRepository
    {
        LicensePlate FindByNumber(string number);

        void Add(LicensePlate licensePlate);

        void Remove(LicensePlate licenplate);
    }
}
