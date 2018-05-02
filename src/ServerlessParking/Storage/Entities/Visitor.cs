using Microsoft.WindowsAzure.Storage.Table;

namespace ServerlessParking.Storage.Entities
{
    public sealed class Visitor : TableEntity
    {
        public Visitor()
        {}

        public Visitor(string partitionKey, string rowKey)
        {
            PartitionKey = partitionKey;
            RowKey = rowKey;
        }

        public string Name { get; set; }

        public string Company { get; set; }

        public string LicensePlate => RowKey;
    }
}
