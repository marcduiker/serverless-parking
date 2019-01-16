using ServelessParking.Domain;

namespace ServerlessParking.Interfaces
{
    public interface ILicenplateRepository
    {
        Licenseplate FindByNumber(string number);

        void Add(Licenseplate licenseplate);

        void Remove(Licenseplate licenplate);
    }
}
