namespace MetricsManager.Repository
{
    public class ConnectionManager : IConnectionManager
    {
        public const string ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        public string GetConnection()
        {
            return ConnectionString;
        }
    }
}