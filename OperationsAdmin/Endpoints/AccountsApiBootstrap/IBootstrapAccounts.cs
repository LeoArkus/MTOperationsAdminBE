using OpAdminDomain.Accounts;
using OpAdminEngine.Accounts;

namespace OpAdminApiBootstrap
{
    public interface IBootstrapAccounts
    {
        IProcessAccountCreate BootstrapAccountCreate(ICommandCreateAccount commandCreateAccount);
    }
    
    public class BootstrapAccounts : IBootstrapAccounts
    {
        public IProcessAccountCreate BootstrapAccountCreate(ICommandCreateAccount commandCreateAccount) =>
            new AccountCreateProcess(commandCreateAccount, new AccountCreateValidator(), new AccountCreateGenerateIdentifiers());
    }
}