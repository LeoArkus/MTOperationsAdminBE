using System;
using Commons;
using Google.Protobuf;
using Grpc.Net.Client;
using OpAdminApi.Errors;
using OpAdminDomain.Accounts;
using GrpcCommandAccountUpsertService;
using static Commons.RailWayOrientation;
using static Commons.TryCommandResult;

namespace OpAdminApi.GrpcConnection
{
    public class GrpcAccountCreate : ICommandCreateAccount
    {
        private readonly string _url;
        private AccountResult _reply = new();

        public GrpcAccountCreate(string url) => _url = url;

        private void CallGrpc(AccountCreateMessage request)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(_url);
            var client = new CommandUpsertAccountService.CommandUpsertAccountServiceClient(channel);
            _reply = client.CommandAccountStore(new AccountRequest
            {
                Parameters = ByteString.CopyFrom(Binary.Serialize(request))
            });
        }

        public CommandResult<ErrorCode> StoreAccount(AccountCreateMessage toStore)
            => Railway(
                CommandResult<ErrorCode>.Create,
                TryEcExFunc(() => CallGrpc(toStore), GrpcConnectionErrors.FailWhenTryingCommandAgent.ErrorNum),
                () => _reply.IsSuccess ? CommandResult<ErrorCode>.Create() : CommandResult<ErrorCode>.Create(new ErrorCode(_reply.LoggedMessage, _reply.ErrorCode))
            );
    }
}