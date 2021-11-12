using System;
using Commons;
using OpAdminDomain;
using OpAdminDomain.Accounts;
using static Commons.RailWayOrientation;

namespace OpAdminEngine.Accounts
{
    public class AccountDeleteProcess : IProcessAccountDelete
    {
        private readonly ICommandDeleteModel _commandDelete;
        private readonly IQueryCheckIfExist _queryCheckIfExist;

        public AccountDeleteProcess(ICommandDeleteModel commandDelete, IQueryCheckIfExist queryCheckIfExist)
        {
            _commandDelete = commandDelete;
            _queryCheckIfExist = queryCheckIfExist;
        }

        public CommandResult<ErrorCode> ProcessAccountDelete(Guid accountId) =>
            Railway(
                CommandResult<ErrorCode>.Create,
                ()=> (!accountId.Equals(Guid.Empty)).CheckValidation(EngineErrors.InvalidId),
                ()=> CheckAccountExist(accountId),
                ()=> _commandDelete.TryDeleteModel(accountId, ModelTypeEnum.Accounts)
            );
        
        private CommandResult<ErrorCode> CheckAccountExist(Guid accountId) => 
            _queryCheckIfExist.CheckIfExistInStorage(accountId, ModelTypeEnum.Accounts)
                .AndThen(() => _queryCheckIfExist.Found().CheckValidation(AccountProcessErrors.AccountDoesNotExist), CommandResult<ErrorCode>.Create);

    }
}