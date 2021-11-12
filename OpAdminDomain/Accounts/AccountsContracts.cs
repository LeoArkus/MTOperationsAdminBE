using System;
using System.Collections.Generic;
using Commons;

namespace OpAdminDomain.Accounts
{
    public interface IProcessAccountCreate
    {
        CommandResult<IEnumerable<ErrorCode>> ProcessAccountCreate(IParseRawRequest<AccountUpsertMessage> parser);
        Optional<Guid> ReadResult();
    }
    
    public interface IProcessAccountUpdate
    {
        CommandResult<IEnumerable<ErrorCode>> ProcessAccountUpdate(IParseRawRequest<AccountUpsertMessage> parser);
    }

    public interface IProcessAccountDelete
    {
        CommandResult<ErrorCode> ProcessAccountDelete(Guid accountId);
    }

    public interface IProcessAccountDetail
    {
        CommandResult<ErrorCode> ProcessAccountDetail(Guid accountId);
        Optional<AccountDetailResponseMessage> ReadResult();
    }
}