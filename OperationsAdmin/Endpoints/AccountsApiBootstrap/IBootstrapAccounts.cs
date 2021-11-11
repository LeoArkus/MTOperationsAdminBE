using OpAdminDomain;
using OpAdminDomain.Accounts;
using OpAdminEngine.Accounts;

namespace OpAdminApiBootstrap
{
    public interface IBootstrapAccounts
    {
        IProcessAccountCreate BootstrapAccountCreate(ICommandCreateAccount commandCreateAccount, IQueryCheckIfExist queryCheckIfExist);
    }
    
    public class BootstrapAccounts : IBootstrapAccounts
    {
        public IProcessAccountCreate BootstrapAccountCreate(ICommandCreateAccount commandCreateAccount, IQueryCheckIfExist queryCheckIfExist) =>
            new AccountCreateProcess(commandCreateAccount, queryCheckIfExist, new AccountCreateValidator(), new AccountCreateGenerateIdentifiers());
    }
}