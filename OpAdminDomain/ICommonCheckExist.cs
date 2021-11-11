using System;
using Commons;

namespace OpAdminDomain
{
    public interface IQueryCheckIfExist
    {
        CommandResult<ErrorCode> CheckIfExistInStorage(Guid modelId, ModelTypeEnum modelType);
        bool Found();
    }
}