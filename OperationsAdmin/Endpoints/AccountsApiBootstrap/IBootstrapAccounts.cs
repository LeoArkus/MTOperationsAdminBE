using OpAdminDomain;
using OpAdminDomain.Accounts;
using OpAdminEngine.Accounts;

namespace OpAdminApiBootstrap
{
    public interface IBootstrapAccounts
    {
        IProcessAccountCreate BootstrapAccountCreate(ICommandCreateAccount commandCreateAccount, IQueryCheckIfExist queryCheckIfExist);
        IProcessAccountUpdate BootstrapAccountUpdate(ICommandCreateAccount commandCreateAccount, IQueryCheckIfExist queryCheckIfExist);
        IProcessAccountDetail BootstrapAccountDetail(IQueryGetAccountDetail queryGetAccountDetail, IQueryCheckIfExist queryCheckIfExist);

    }
    
    public class BootstrapAccounts : IBootstrapAccounts
    {
        public IProcessAccountCreate BootstrapAccountCreate(ICommandCreateAccount commandCreateAccount, IQueryCheckIfExist queryCheckIfExist) =>
            new AccountCreateProcess(commandCreateAccount, queryCheckIfExist, new AccountCreateValidator(), new AccountCreateGenerateIdentifiers());

        public IProcessAccountUpdate BootstrapAccountUpdate(ICommandCreateAccount commandCreateAccount, IQueryCheckIfExist queryCheckIfExist) =>
            new AccountUpdateProcess(commandCreateAccount, queryCheckIfExist, new AccountCreateValidator());

        public IProcessAccountDetail BootstrapAccountDetail(IQueryGetAccountDetail queryGetAccountDetail, IQueryCheckIfExist queryCheckIfExist) =>
            new AccountDetailProcess(queryCheckIfExist, queryGetAccountDetail);
    }
}