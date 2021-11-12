using System;
using Commons;

namespace OpAdminDomain.Accounts
{
    public interface ICommandCreateAccount
    {
        CommandResult<ErrorCode> StoreAccount(AccountUpsertMessage accountMessage);
    }

    public interface IQueryGetAccountDetail
    {
        CommandResult<ErrorCode> GetAccountDetail(Guid accountId);
        Optional<AccountDetailResponseMessage> ReadResult();
    }
}