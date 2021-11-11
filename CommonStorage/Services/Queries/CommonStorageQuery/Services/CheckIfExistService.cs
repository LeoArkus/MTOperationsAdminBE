using System;
using System.Threading.Tasks;
using Commons;
using CommonStorageQueryBootstrap;
using Grpc.Core;
using GrpcCommonCheckIfExistInStorage;
using Microsoft.Extensions.Configuration;
using OpAdminDomain;

namespace CommonStorageQuery.Services
{
    public class CheckIfExistService : CommonCheckIfExistQueryService.CommonCheckIfExistQueryServiceBase
    {
        private readonly IQueryCheckIfExist _query;

        public CheckIfExistService( IConfiguration configuration, IBootstrapCheckExist bootstrapCheckExist) => 
            _query = bootstrapCheckExist.BootstrapCheckIfExist(ConfigurationReader.ReadQueryConnection(configuration));

        public override Task<CheckIfExistResult> CheckIfExistInStorage(CheckIfExistRequest request, ServerCallContext context = null) =>
            Task.FromResult(_query.CheckIfExistInStorage(Guid.Parse(request.Id), request.ModelType.ToEnum<ModelTypeEnum>()).AndThen(
                ()=> new CheckIfExistResult()
                {
                    ErrorCode = string.Empty,
                    LoggedMessage = string.Empty,
                    Found = _query.Found()
                },
                x=> new CheckIfExistResult()
                {
                    ErrorCode = x.ErrorNum,
                    LoggedMessage = x.LoggedMessage,
                    Found = false
                }
            ));
    }
}