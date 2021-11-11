using System.Collections.Generic;
using Commons;

namespace OpAdminDomain.Accounts
{
    public interface IProcessAccountCreate
    {
        CommandResult<IEnumerable<ErrorCode>> ProcessAccountCreate(IParseRawRequest<AccountCreateMessage> parser);
    }
}