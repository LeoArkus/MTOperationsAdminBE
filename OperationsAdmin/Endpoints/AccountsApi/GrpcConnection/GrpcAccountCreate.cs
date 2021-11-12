using System;
using Commons;
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

        private void CallGrpc(AccountUpsertMessage request)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(_url);
            var client = new CommandUpsertAccountService.CommandUpsertAccountServiceClient(channel);
            _reply = client.CommandAccountStore(new AccountRequest
            {
                Id = request.Id.ToString(),
                AccountName = request.AccountName.Value,
                ClientFirstName = request.ClientFirstName.AndThen(x=> x.Value, ()=> string.Empty),
                ClientLastName = request.ClientLastName.AndThen(x=> x.Value, ()=> string.Empty),
                UserOperationManagerId = request.UserOperationManagerId.ToString()
            });
        }

        public CommandResult<ErrorCode> StoreAccount(AccountUpsertMessage toStore)
            => Railway(
                CommandResult<ErrorCode>.Create,
                TryEcExFunc(() => CallGrpc(toStore), GrpcConnectionErrors.FailWhenTryingCommandAgent.ErrorNum),
                () => _reply.IsSuccess ? CommandResult<ErrorCode>.Create() : CommandResult<ErrorCode>.Create(new ErrorCode(_reply.LoggedMessage, _reply.ErrorCode))
            );
    }
}