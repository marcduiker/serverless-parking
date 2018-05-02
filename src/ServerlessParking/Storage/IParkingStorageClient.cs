using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace ServerlessParking.Storage
{
    public interface IParkingStorageClient
    {
        Task<T> RetrieveEntity<T>(string tableName, string entityType, string licensePlate)
            where T : TableEntity, new();
    }
}
