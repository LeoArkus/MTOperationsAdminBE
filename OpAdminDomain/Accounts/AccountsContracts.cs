using System;
using System.Collections.Generic;
using Commons;

namespace OpAdminDomain.Accounts
{
    public interface IProcessAccountCreate
    {
        CommandResult<IEnumerable<ErrorCode>> ProcessAccountCreate(IParseRawRequest<AccountUpsertMessage> parser);
    }
    
    public interface IProcessAccountUpdate
    {
        CommandResult<IEnumerable<ErrorCode>> ProcessAccountUpdate(IParseRawRequest<AccountUpsertMessage> parser);
    }

    public interface IProcessAccountDetail
    {
        CommandResult<ErrorCode> ProcessAccountDetail(Guid accountId);
        Optional<AccountDetailResponseMessage> ReadResult();
    }
}