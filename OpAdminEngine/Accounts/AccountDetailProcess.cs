using System;
using Commons;
using OpAdminDomain;
using OpAdminDomain.Accounts;
using static Commons.RailWayOrientation;

namespace OpAdminEngine.Accounts
{
    public class AccountDetailProcess : IProcessAccountDetail
    {
        private readonly IQueryCheckIfExist _queryCheckIfExist;
        private readonly IQueryGetAccountDetail _queryGetAccountDetail;
        private Optional<AccountDetailResponseMessage> _result = Optional<AccountDetailResponseMessage>.Create();

        public AccountDetailProcess(IQueryCheckIfExist queryCheckIfExist, IQueryGetAccountDetail queryGetAccountDetail)
        {
            _queryCheckIfExist = queryCheckIfExist;
            _queryGetAccountDetail = queryGetAccountDetail;
        }

        public CommandResult<ErrorCode> ProcessAccountDetail(Guid accountId) =>
            Railway(
                CommandResult<ErrorCode>.Create,
                ()=> (!accountId.Equals(Guid.Empty)).CheckValidation(EngineErrors.InvalidId),
                ()=> CheckAccountExist(accountId),
                ()=> _queryGetAccountDetail.GetAccountDetail(accountId),
                ExecuteSuccessFunc<ErrorCode>(()=> _result = _queryGetAccountDetail.ReadResult())
            );

        private CommandResult<ErrorCode> CheckAccountExist(Guid accountId) => 
            _queryCheckIfExist.CheckIfExistInStorage(accountId, ModelTypeEnum.Accounts)
                .AndThen(() => _queryCheckIfExist.Found().CheckValidation(AccountProcessErrors.AccountDoesNotExist), CommandResult<ErrorCode>.Create);

        public Optional<AccountDetailResponseMessage> ReadResult() => _result;
    }
}