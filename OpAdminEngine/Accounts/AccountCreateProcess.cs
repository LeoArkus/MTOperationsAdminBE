using System;
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
        private readonly IValidateModel<AccountUpsertMessage> _validator;
        private readonly IGenerateIdentifiers<AccountUpsertMessage> _generator;
        private Optional<Guid> _result = Optional<Guid>.Create();

        public AccountCreateProcess(ICommandCreateAccount command, IQueryCheckIfExist queryCheckIfExist,
            IValidateModel<AccountUpsertMessage> validator, IGenerateIdentifiers<AccountUpsertMessage> generator)
        {
            _command = command;
            _validator = validator;
            _generator = generator;
            _queryCheckIfExist = queryCheckIfExist;
        }

        public CommandResult<IEnumerable<ErrorCode>> ProcessAccountCreate(IParseRawRequest<AccountUpsertMessage> parser) =>
            Railway(
                CommandResult<IEnumerable<ErrorCode>>.Create,
                ()=> parser.Parse().ToEnumerable(),
                BindEnumerable(parser.ReadParsed, _generator.GenerateGuidsForModel, EngineErrors.UnableToReadParsed),
                Bind(_generator.ReadModelWithIds, _validator.IsValid, EngineErrors.UnableToReadFromGenerator.ToEnumerable()),
                BindEnumerable(_generator.ReadModelWithIds, CheckUserExist, EngineErrors.UnableToReadFromGenerator),
                BindEnumerable(_generator.ReadModelWithIds, _command.StoreAccount, EngineErrors.UnableToReadFromGenerator),
                BindEnumerable(_generator.ReadModelWithIds, x=> 
                    ExecuteSuccess<ErrorCode>(()=> _result = Optional<Guid>.Create(x.Id)), EngineErrors.UnableToReadFromGenerator)
            );

        public Optional<Guid> ReadResult() => _result;

        private CommandResult<ErrorCode> CheckUserExist(AccountUpsertMessage accountCreateMessage) =>
            _queryCheckIfExist.CheckIfExistInStorage(accountCreateMessage.UserOperationManagerId, ModelTypeEnum.Users)
                .AndThen(() => _queryCheckIfExist.Found().CheckValidation(AccountProcessErrors.OperationManagerDoesNotExist), CommandResult<ErrorCode>.Create);
    }
}