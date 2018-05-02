using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace ServerlessParking.Storage
{
    public class ParkingStorageClient : IParkingStorageClient
    {
        private readonly string _storageConnectionString = Environment.GetEnvironmentVariable("ServerlessParkingTableStorageConnection");

        public async Task<T> RetrieveEntity<T>(
            string tableName,
            string entityType,
            string licensePlate)
            where T : TableEntity, new()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(_storageConnectionString);
            var client = storageAccount.CreateCloudTableClient();
            var table = client.GetTableReference(tableName);
            var tableOperation = TableOperation.Retrieve<T>(
                partitionKey: entityType,
                rowkey: licensePlate);
            var tableResult = await table.ExecuteAsync(tableOperation);

            var entity = tableResult.Result as T;

            return entity;
        }
    }
}
