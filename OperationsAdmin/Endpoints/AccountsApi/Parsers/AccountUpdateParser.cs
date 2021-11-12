using Commons;
using OpAdminApi.Errors;
using OpAdminApi.Requests;
using OpAdminDomain;
using OpAdminDomain.Accounts;
using static Commons.TryCommandResult;

namespace OpAdminApi.Parsers
{
    public class AccountUpdateParser : IParseRawRequest<AccountUpsertMessage>
    {
        private readonly AccountUpdateRequest _request;
        private Optional<AccountUpsertMessage> _parsed = Optional<AccountUpsertMessage>.Create();

        public AccountUpdateParser(AccountUpdateRequest request) => _request = request;

        public CommandResult<ErrorCode> Parse() =>
            TryEcEx(() => _parsed = Optional<AccountUpsertMessage>.Create(new AccountUpsertMessage()
            {
                Id = _request.Id,
                AccountName = new String50(_request.AccountName),
                UserOperationManagerId = _request.UserOperationManagerId,
                ClientFirstName = !string.IsNullOrWhiteSpace(_request.ClientFirstName) ? Optional<String50>.Create(new String50(_request.ClientFirstName)) : Optional<String50>.Create(),
                ClientLastName = !string.IsNullOrWhiteSpace(_request.ClientLastName) ? Optional<String50>.Create(new String50(_request.ClientLastName)) : Optional<String50>.Create(),
            }), ApiErrors.UnableToParseRequest.ErrorNum);

        public Optional<AccountUpsertMessage> ReadParsed() => _parsed;
    }
}