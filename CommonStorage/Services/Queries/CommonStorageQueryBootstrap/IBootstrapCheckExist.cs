using OpAdminDomain;
using OpAdminStorageLibrary;
using OpAdminStorageLibrary.Queries.ExistInStorage;

namespace CommonStorageQueryBootstrap
{
    public interface IBootstrapCheckExist
    {
        IQueryCheckIfExist BootstrapCheckIfExist(string connectionString);
    }
    
    public class BootstrapCheckExist : IBootstrapCheckExist
    {
        public IQueryCheckIfExist BootstrapCheckIfExist(string connectionString) =>
            new CheckIfExistInStorageQuery(new QueryConnection(connectionString));
    }
}