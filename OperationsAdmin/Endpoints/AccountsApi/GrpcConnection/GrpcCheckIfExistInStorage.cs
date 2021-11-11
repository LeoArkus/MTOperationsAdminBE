using System;
using Commons;
using Grpc.Net.Client;
using GrpcCommonCheckIfExistInStorage;
using OpAdminApi.Errors;
using OpAdminDomain;
using static Commons.RailWayOrientation;
using static Commons.TryCommandResult;

namespace OpAdminApi.GrpcConnection
{
    public class GrpcCheckIfExistInStorage : IQueryCheckIfExist
    {
        private readonly string _url;
        private CheckIfExistResult _result = new();
        private bool _found = false;

        public GrpcCheckIfExistInStorage(string url) => _url = url;

        private void CallGrpc(Guid modelId, ModelTypeEnum modelType)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(_url);
            var client = new CommonCheckIfExistQueryService.CommonCheckIfExistQueryServiceClient(channel);
            _result = client.CheckIfExistInStorage(new CheckIfExistRequest()
            {
                Id = modelId.ToString(),
                ModelType = modelType.ToString()
            });
        }
        
        public CommandResult<ErrorCode> CheckIfExistInStorage(Guid modelId, ModelTypeEnum modelType) => 
            Railway(
                CommandResult<ErrorCode>.Create,
                TryEcExFunc(() => CallGrpc(modelId,modelType), GrpcConnectionErrors.FailWhenTryingQueryAgent.ErrorNum),
                ParseResult
            );

        private CommandResult<ErrorCode> ParseResult() 
            =>  _result.IsSuccess ? 
                ExecuteSuccess<ErrorCode>(() => _found = _result.Found)
                : CommandResult<ErrorCode>.Create(new ErrorCode(_result.LoggedMessage, _result.ErrorCode));

        public bool Found() => _found;
    }
}