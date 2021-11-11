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
            => Task.FromResult(_command.StoreAccount(request.Parameters.Deserialize<AccountCreateMessage>())
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