using System;
using System.Threading.Tasks;
using Commons;
using CommonStorageCommandBootstrap;
using Grpc.Core;
using GrpcCommonModelDeleteCommand;
using Microsoft.Extensions.Configuration;
using OpAdminDomain;

namespace CommonStorageCommand.Services
{
    public class ModelDeleteService : CommonModelDeleteCommand.CommonModelDeleteCommandBase
    {
        private readonly ICommandDeleteModel _command;

        public ModelDeleteService(IConfiguration configuration, IBootstrapDeleteModel  bootstrap)
            => _command = bootstrap.BootstrapDeleteModelCommand(ConfigurationReader.ReadCommandConnection(configuration));

        public override Task<ModelDeleteResult> CommonModelDeleteCommand(ModelDeleteRequest request, ServerCallContext context)
            => Task.FromResult(_command.TryDeleteModel(Guid.Parse(request.Id), request.ModelType.ToEnum<ModelTypeEnum>())
                .AndThen(() => new ModelDeleteResult()
                {
                    ErrorCode = string.Empty,
                    LoggedMessage = string.Empty,
                    IsSuccess = true
                }, (x) => new ModelDeleteResult()
                {
                    ErrorCode = x.ErrorNum,
                    LoggedMessage = x.LoggedMessage,
                    IsSuccess = false
                }));
    }
}