using Azure;
using Azure.Data.Tables;

public class MenuItem : ITableEntity
{
    public string PartitionKey { get; set; } = "uge1"; // Standardværdi for uge
    public string RowKey { get; set; }                 // Dag som nøgle (Mandag, Tirsdag osv.)
    public string KoldRet { get; set; }
    public string VarmRet { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
}
