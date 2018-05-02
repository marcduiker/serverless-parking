using Microsoft.WindowsAzure.Storage.Table;

namespace ServerlessParking.Storage.Entities
{
    public sealed class Employee : TableEntity
    {

        public Employee()
        {}

        public Employee(
            string partitionKey, 
            string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }
        public string Name { get; set; }

        public string LicensePlate => RowKey;
    }
}
