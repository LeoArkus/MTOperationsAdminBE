using OpAdminDomain.Accounts;
using OpAdminStorageLibrary;
using OpAdminStorageLibrary.Queries.Accounts;

namespace OpAdminQueryBootstrap
{
    public interface IBootstrapAccountsQuery
    {
        IQueryGetAccountDetail BootstrapAccountsDetail(string connectionString);
    }
    
    public class BootstrapAccountsQuery : IBootstrapAccountsQuery
    {
        public IQueryGetAccountDetail BootstrapAccountsDetail(string connectionString) =>
            new AccountDetailQueryReader(new QueryConnection(connectionString));
    }
}