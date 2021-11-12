using System;
using System.Data;
using Commons;
using Dapper;
using OpAdminDomain;
using static Commons.BindCommandResultOptional;
using static Commons.RailWayOrientation;
using static Commons.TryCommandResult;

namespace OpAdminStorageLibrary.Commands.CommonCommand
{
    public class DeleteModelCommand : ICommandDeleteModel
    {
        private readonly ICommandDbConnection _dbConnection;

        public DeleteModelCommand(ICommandDbConnection dbConnection) => _dbConnection = dbConnection;

        public CommandResult<ErrorCode> TryDeleteModel(Guid modelId, ModelTypeEnum modelType) => 
            Railway(
                _dbConnection.RollbackOnError,
                _dbConnection.CreateConnection,
                _dbConnection.OpenConnection,
                _dbConnection.CreateTransaction,
                Bind(_dbConnection.ReadConnectionTransaction, x=> ExecuteUpsert(x, modelId, modelType), StorageError.ErrorCodeUnableToReadConnection),
                _dbConnection.Commit
            ).Finally(_dbConnection.Dispose);

        private CommandResult<ErrorCode> ExecuteUpsert(Tuple<IDbConnection, IDbTransaction> conn, Guid modelId, ModelTypeEnum modelType) =>
            TryEcEx(() => conn.Item1.Execute(_queryDelete(modelType), new {id = modelId}, conn.Item2), StorageError.ErrorCodeUnableToExecuteQuery.ErrorNum);

        private string _queryDelete(ModelTypeEnum modelTypeEnum) => $@"
            DELETE FROM {StorageModelTables.DictionaryTable[modelTypeEnum]}
            WHERE id = @id
        ";
    }
}