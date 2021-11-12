using System;
using Commons;
using Grpc.Net.Client;
using GrpcAccountDetailQuery;
using OpAdminApi.Errors;
using OpAdminDomain.Accounts;
using static Commons.RailWayOrientation;
using static Commons.TryCommandResult;

namespace OpAdminApi.GrpcConnection
{
    public class GrpcAccountDetail : IQueryGetAccountDetail
    {
        private readonly string _url;
        private AccountDetailResult _reply;
        private Optional<AccountDetailResponseMessage> _result = Optional<AccountDetailResponseMessage>.Create();

        public GrpcAccountDetail(string url) => _url = url;

        private void CallGrpc(Guid accountId)
        {
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            using var channel = GrpcChannel.ForAddress(_url);
            var client = new AccountDetailQueryService.AccountDetailQueryServiceClient(channel);
            _reply = client.GetAccountDetail(new AccountDetailRequest()
            {
                Id = accountId.ToString()
            });
        }

        public CommandResult<ErrorCode> GetAccountDetail(Guid accountId) =>
            Railway(
                CommandResult<ErrorCode>.Create,
                TryEcExFunc(() => CallGrpc(accountId), GrpcConnectionErrors.UnableToParseResponse.ErrorNum),
                () => _reply.IsSuccess ? CommandResult<ErrorCode>.Create() : CommandResult<ErrorCode>.Create(new ErrorCode(_reply.ErrorCode, _reply.LoggedMessage)),
                ExecuteSuccessFunc<ErrorCode>(() => _result = Optional<AccountDetailResponseMessage>.Create(ParseReportResponse()))
            );

        private AccountDetailResponseMessage ParseReportResponse() =>
            new()
            {
                Id = Guid.Parse(_reply.Report.Id),
                AccountName = _reply.Report.AccountName,
                ClientFirstName = _reply.Report.ClientFirstName,
                ClientLastName = _reply.Report.ClientLastName,
                UserOperationManagerId = Guid.Parse(_reply.Report.OperationManager)
            };

        public Optional<AccountDetailResponseMessage> ReadResult() => _result;
    }
}