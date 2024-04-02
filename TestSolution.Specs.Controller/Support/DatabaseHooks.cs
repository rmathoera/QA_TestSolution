using TestSolution.Web.DataAccess;
using Reqnroll;

// ReSharper disable once CheckNamespace
namespace TestSolution.Specs.Support
{
    [Binding]
    public class DatabaseHooks
    {
        private readonly DataContext.IDataPersist _dataPersist = new DataContext.InMemoryPersist();

        [BeforeScenario(Order = 100)]
        public void ResetDatabaseToBaseline()
        {
            // configure app to use in-memory database
            DataContext.CreateDataPersist = () => _dataPersist;
            
            ClearDatabase();
            DomainDefaults.AddDefaultUsers();
        }

        private static void ClearDatabase()
        {
            var db = new DataContext();
            db.TruncateTables();
        }
    }
}
