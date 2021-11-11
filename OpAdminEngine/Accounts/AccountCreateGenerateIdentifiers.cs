using System;
using Commons;
using OpAdminDomain;
using OpAdminDomain.Accounts;
using static Commons.TryCommandResult;

namespace OpAdminEngine.Accounts
{
    public class AccountCreateGenerateIdentifiers : IGenerateIdentifiers<AccountCreateMessage>
    {
        private Optional<AccountCreateMessage> _result = Optional<AccountCreateMessage>.Create();

        public CommandResult<ErrorCode> GenerateGuidsForModel(AccountCreateMessage model) =>
            TryEc(() =>
            {
                model.Id = model.Id.Equals(Guid.Empty) ? Guid.NewGuid() : model.Id;
                _result = Optional<AccountCreateMessage>.Create(model);
            }, EngineErrors.UnableToGenerateIdentifiers);

        public Optional<AccountCreateMessage> ReadModelWithIds() => _result;
    }
}