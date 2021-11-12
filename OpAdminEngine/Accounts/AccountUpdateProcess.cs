using System.Collections.Generic;
using Commons;
using OpAdminDomain;
using OpAdminDomain.Accounts;
using static Commons.BindCommandResultOptional;
using static Commons.RailWayOrientation;

namespace OpAdminEngine.Accounts
{
    public class AccountUpdateProcess : IProcessAccountUpdate
    {
        private readonly ICommandCreateAccount _command;
        private readonly IQueryCheckIfExist _queryCheckIfExist;
        private readonly IValidateModel<AccountUpsertMessage> _validator;

        public AccountUpdateProcess(ICommandCreateAccount command, IQueryCheckIfExist queryCheckIfExist, IValidateModel<AccountUpsertMessage> validator)
        {
            _command = command;
            _queryCheckIfExist = queryCheckIfExist;
            _validator = validator;
        }

        public CommandResult<IEnumerable<ErrorCode>> ProcessAccountUpdate(IParseRawRequest<AccountUpsertMessage> parser) =>
            Railway(
                CommandResult<IEnumerable<ErrorCode>>.Create,
                ()=> parser.Parse().ToEnumerable(),
                Bind(parser.ReadParsed, _validator.IsValid, EngineErrors.UnableToReadParsed.ToEnumerable()),
                BindEnumerable(parser.ReadParsed, CheckAccountExist, EngineErrors.UnableToReadParsed),
                BindEnumerable(parser.ReadParsed, CheckUserExist, EngineErrors.UnableToReadParsed),
                BindEnumerable(parser.ReadParsed, x=> _command.StoreAccount(x), EngineErrors.UnableToReadParsed)
            );

        private CommandResult<ErrorCode> CheckUserExist(AccountUpsertMessage accountCreateMessage) =>
            _queryCheckIfExist.CheckIfExistInStorage(accountCreateMessage.UserOperationManagerId, ModelTypeEnum.Users)
                .AndThen(() => _queryCheckIfExist.Found().CheckValidation(AccountProcessErrors.OperationManagerDoesNotExist), CommandResult<ErrorCode>.Create);

        private CommandResult<ErrorCode> CheckAccountExist(AccountUpsertMessage accountUpsertMessage) => 
            _queryCheckIfExist.CheckIfExistInStorage(accountUpsertMessage.Id, ModelTypeEnum.Accounts)
                .AndThen(() => _queryCheckIfExist.Found().CheckValidation(AccountProcessErrors.AccountDoesNotExist), CommandResult<ErrorCode>.Create);
    }
}