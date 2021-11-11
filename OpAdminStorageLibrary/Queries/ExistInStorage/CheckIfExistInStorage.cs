using System;
using System.Data;
using Commons;
using Dapper;
using OpAdminDomain;
using static Commons.BindCommandResultOptional;
using static Commons.RailWayOrientation;
using static Commons.TryCommandResult;

namespace OpAdminStorageLibrary.Queries.ExistInStorage
{
    public class CheckIfExistInStorageQuery : IQueryCheckIfExist
    {
        private readonly IQueryDbConnection _connection;
        private bool _found;
        public CheckIfExistInStorageQuery(IQueryDbConnection connection) => _connection = connection;

        public CommandResult<ErrorCode> CheckIfExistInStorage(Guid modelId, ModelTypeEnum modelType) =>
            Railway(
                CommandResult<ErrorCode>.Create,
                _connection.CreateConnection,
                _connection.OpenConnection,
                Bind(_connection.ReadConnection, x => ExecuteQuery(x, modelId, modelType), StorageError.ErrorCodeUnableToReadConnection)
            ).Finally(_connection.Dispose);

        private CommandResult<ErrorCode> ExecuteQuery(IDbConnection conn, Guid modelId, ModelTypeEnum modelType) =>
            TryEcEx(() => _found = conn.ExecuteScalar<bool>(_queryExist(modelType), new { id = modelId }), StorageError.ErrorCodeUnableToExecuteQuery.ErrorNum);

        public bool Found() => _found;

        private string _queryExist(ModelTypeEnum modelType) => $@"
            SELECT 1 = 1 FROM {CheckIfExistTable.DictionaryTableExist[modelType]} WHERE id = @id
        ";

    }
}