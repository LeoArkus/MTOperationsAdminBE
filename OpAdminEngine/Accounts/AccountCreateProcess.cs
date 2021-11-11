using System.Collections.Generic;
using Commons;
using OpAdminDomain;
using OpAdminDomain.Accounts;
using static Commons.BindCommandResultOptional;
using static Commons.RailWayOrientation;

namespace OpAdminEngine.Accounts
{
    public class AccountCreateProcess : IProcessAccountCreate
    {
        private readonly ICommandCreateAccount _command;
        private readonly IQueryCheckIfExist _queryCheckIfExist;
        private readonly IValidateModel<AccountCreateMessage> _validator;
        private readonly IGenerateIdentifiers<AccountCreateMessage> _generator;

        public AccountCreateProcess(ICommandCreateAccount command, IQueryCheckIfExist queryCheckIfExist,
            IValidateModel<AccountCreateMessage> validator, IGenerateIdentifiers<AccountCreateMessage> generator)
        {
            _command = command;
            _validator = validator;
            _generator = generator;
            _queryCheckIfExist = queryCheckIfExist;
        }

        public CommandResult<IEnumerable<ErrorCode>> ProcessAccountCreate(IParseRawRequest<AccountCreateMessage> parser) =>
            Railway(
                CommandResult<IEnumerable<ErrorCode>>.Create,
                ()=> parser.Parse().ToEnumerable(),
                Bind(parser.ReadParsed, _validator.IsValid, EngineErrors.UnableToReadParsed.ToEnumerable()),
                BindEnumerable(parser.ReadParsed, _generator.GenerateGuidsForModel, EngineErrors.UnableToReadParsed),
                BindEnumerable(_generator.ReadModelWithIds, CheckIfUserExist, EngineErrors.UnableToReadFromGenerator),
                BindEnumerable(_generator.ReadModelWithIds, _command.StoreAccount, EngineErrors.UnableToReadFromGenerator)
            );

        private CommandResult<ErrorCode> CheckIfUserExist(AccountCreateMessage accountCreateMessage) =>
            _queryCheckIfExist.CheckIfExistInStorage(accountCreateMessage.UserOperationManagerId, ModelTypeEnum.Users)
                .AndThen(() => _queryCheckIfExist.Found().CheckValidation(AccountProcessErrors.OperationManagerDoesNotExist), CommandResult<ErrorCode>.Create);
    }
}