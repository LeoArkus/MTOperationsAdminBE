using System;
using System.Threading.Tasks;
using Commons;
using Grpc.Core;
using OpAdminDomain.Accounts;
using GrpcCommandAccountUpsertService;
using Microsoft.Extensions.Configuration;
using OpAdminCommandBootstrap;

namespace OpAdminCommand.Services
{
    public class AccountCreateService : CommandUpsertAccountService.CommandUpsertAccountServiceBase
    {
        private readonly ICommandCreateAccount _command;

        public AccountCreateService(IConfiguration configuration, IBootstrapAccountServices bootstrap)
            => _command = bootstrap.BootstrapCreateAccount(ConfigurationReader.ReadCommandConnection(configuration));

        public override Task<AccountResult> CommandAccountStore(AccountRequest request, ServerCallContext context)
            => Task.FromResult(_command.StoreAccount(new AccountCreateMessage()
                {
                    Id = Guid.Parse(request.Id),
                    AccountName = new String50(request.AccountName),
                    ClientFirstName = !string.IsNullOrWhiteSpace(request.ClientFirstName) ? Optional<String50>.Create(new String50(request.ClientFirstName)) : Optional<String50>.Create(),
                    ClientLastName = !string.IsNullOrWhiteSpace(request.ClientLastName) ? Optional<String50>.Create(new String50(request.ClientLastName)) : Optional<String50>.Create(),
                    UserOperationManagerId = Guid.Parse(request.UserOperationManagerId)
                })
                .AndThen(() => new AccountResult()
                {
                    ErrorCode = string.Empty,
                    LoggedMessage = string.Empty,
                    IsSuccess = true
                }, (x) => new AccountResult()
                {
                    ErrorCode = x.ErrorNum,
                    LoggedMessage = x.LoggedMessage,
                    IsSuccess = false
                }));
    }
}