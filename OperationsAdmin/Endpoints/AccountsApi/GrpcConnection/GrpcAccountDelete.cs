using System;
using Commons;
using Grpc.Net.Client;
using OpAdminApi.Errors;
using GrpcCommonModelDeleteCommand;
using OpAdminDomain;
using static Commons.RailWayOrientation;
using static Commons.TryCommandResult;

namespace OpAdminApi.GrpcConnection
{
    public class GrpcAccountDelete : ICommandDeleteModel
    {
        private readonly string _url;
        private ModelDeleteResult _reply = new();

        public GrpcAccountDelete(string url) => _url = url;

        private void CallGrpc(Guid modelId, ModelTypeEnum modelType)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(_url);
            var client = new CommonModelDeleteCommand.CommonModelDeleteCommandClient(channel);
            _reply = client.CommonModelDeleteCommand(new ModelDeleteRequest()
            {
                Id = modelId.ToString(),
                ModelType = modelType.ToString()
            });
        }

        public CommandResult<ErrorCode> TryDeleteModel(Guid modelId, ModelTypeEnum modelType)
            => Railway(
                CommandResult<ErrorCode>.Create,
                TryEcExFunc(() => CallGrpc(modelId, modelType), GrpcConnectionErrors.FailWhenTryingCommandAgent.ErrorNum),
                () => _reply.IsSuccess ? CommandResult<ErrorCode>.Create() : CommandResult<ErrorCode>.Create(new ErrorCode(_reply.LoggedMessage, _reply.ErrorCode))
            );
    }
}