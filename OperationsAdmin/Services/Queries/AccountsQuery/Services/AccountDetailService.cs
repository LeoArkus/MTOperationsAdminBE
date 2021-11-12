using System;
using System.Threading.Tasks;
using Commons;
using Grpc.Core;
using GrpcAccountDetailQuery;
using Microsoft.Extensions.Configuration;
using OpAdminDomain.Accounts;
using OpAdminQueryBootstrap;

namespace AccountsQuery.Services
{
    public class AccountDetailService : AccountDetailQueryService.AccountDetailQueryServiceBase
    {
        private readonly IQueryGetAccountDetail _query;

        public AccountDetailService(IConfiguration configuration, IBootstrapAccountsQuery bootstrapCheckExist) =>
            _query = bootstrapCheckExist.BootstrapAccountsDetail(ConfigurationReader.ReadQueryConnection(configuration));

        public override Task<AccountDetailResult> GetAccountDetail(AccountDetailRequest request, ServerCallContext context = null) =>
            Task.FromResult(_query.GetAccountDetail(Guid.Parse(request.Id)).AndThen(ResponseSuccess, ResponseFailure));

        private AccountDetailResult ResponseSuccess() =>
            _query.ReadResult().AndThen(
                x => new AccountDetailResult()
                {
                    IsSuccess = true,
                    ErrorCode = string.Empty,
                    LoggedMessage = string.Empty,
                    Report = new AccountDetailResponse()
                    {
                        Id = x.Id.ToString(),
                        AccountName = x.AccountName,
                        ClientFirstName = x.ClientFirstName,
                        ClientLastName = x.ClientLastName,
                        OperationManager = x.UserOperationManagerId.ToString()
                    }
                },
                () => ResponseFailure(ServicesError.UnableToReadReport));

        private AccountDetailResult ResponseFailure(ErrorCode errorCode) =>
            new()
            {
                IsSuccess = false,
                ErrorCode = errorCode.ErrorNum,
                LoggedMessage = errorCode.LoggedMessage,
                Report = null
            };
    }
}