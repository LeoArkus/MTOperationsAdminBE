using System;
using Commons;

namespace OpAdminDomain
{
    public interface ICommandDeleteModel
    {
        CommandResult<ErrorCode> TryDeleteModel(Guid modelId, ModelTypeEnum modelType);
    }
}