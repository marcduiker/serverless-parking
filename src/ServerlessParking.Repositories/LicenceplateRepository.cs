using System;
using ServelessParking.Domain;
using ServerlessParking.Interfaces;

namespace ServerlessParking.Repositories
{
    public class LicenceplateRepository : ILicenplateRepository
    {
        public Licenseplate FindByNumber(string number)
        {
            throw new NotImplementedException();
        }

        public void Add(Licenseplate licenseplate)
        {
            throw new NotImplementedException();
        }

        public void Remove(Licenseplate licenplate)
        {
            throw new NotImplementedException();
        }
    }
}
