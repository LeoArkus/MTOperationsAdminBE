using Commons;

namespace OpAdminDomain.Accounts
{
    public interface ICommandCreateAccount
    {
        CommandResult<ErrorCode> StoreAccount(AccountUpsertMessage accountMessage);
    }
}