using Commons;
using OpAdminApi.Requests;
using OpAdminDomain;
using OpAdminDomain.Accounts;

namespace OpAdminApi.Parsers
{
    public class AccountCreateParser : IParseRawRequest<AccountCreateMessage>
    {
        private readonly AccountCreateRequest _request;
        private Optional<AccountCreateMessage> _parsed = Optional<AccountCreateMessage>.Create();
        private readonly ErrorCode _unableToParse = new ErrorCode("Unable to parse request", "UnableToParseRequest");

        public AccountCreateParser(AccountCreateRequest request) => _request = request;

        public CommandResult<ErrorCode> Parse() =>
            TryCommandResult.TryEcEx(() => _parsed = Optional<AccountCreateMessage>.Create(new AccountCreateMessage()
            {
                AccountName = new String50(_request.AccountName),
                UserOperationManagerId = _request.UserOperationManagerId,
                ClientFirstName = !string.IsNullOrWhiteSpace(_request.ClientFirstName) ? Optional<String50>.Create(new String50(_request.ClientFirstName)) : Optional<String50>.Create(),
                ClientLastName = !string.IsNullOrWhiteSpace(_request.ClientLastName) ? Optional<String50>.Create(new String50(_request.ClientLastName)) : Optional<String50>.Create(),
            }), _unableToParse.ErrorNum);

        public Optional<AccountCreateMessage> ReadParsed() => _parsed;
    }
}