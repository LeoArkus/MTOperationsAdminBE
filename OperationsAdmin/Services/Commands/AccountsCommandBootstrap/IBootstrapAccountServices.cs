using OpAdminDomain.Accounts;
using OpAdminStorageLibrary;
using OpAdminStorageLibrary.Commands.Accounts;

namespace OpAdminCommandBootstrap
{
    public interface IBootstrapAccountServices
    {
        ICommandCreateAccount BootstrapCreateAccount(string connectionString);
    }
    
    public class BootstrapAccountServices : IBootstrapAccountServices
    {
        public ICommandCreateAccount BootstrapCreateAccount(string connectionString) =>
            new StoreAccountCommand(new CommandConnection(connectionString));
    }
}