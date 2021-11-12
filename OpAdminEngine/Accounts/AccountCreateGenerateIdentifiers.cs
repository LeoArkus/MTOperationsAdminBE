using System;
using Commons;
using OpAdminDomain;
using OpAdminDomain.Accounts;
using static Commons.TryCommandResult;

namespace OpAdminEngine.Accounts
{
    public class AccountCreateGenerateIdentifiers : IGenerateIdentifiers<AccountUpsertMessage>
    {
        private Optional<AccountUpsertMessage> _result = Optional<AccountUpsertMessage>.Create();

        public CommandResult<ErrorCode> GenerateGuidsForModel(AccountUpsertMessage model) =>
            TryEc(() =>
            {
                model.Id = model.Id.Equals(Guid.Empty) ? Guid.NewGuid() : model.Id;
                _result = Optional<AccountUpsertMessage>.Create(model);
            }, EngineErrors.UnableToGenerateIdentifiers);

        public Optional<AccountUpsertMessage> ReadModelWithIds() => _result;
    }
}