using System;
using System.Data;
using Commons;
using Dapper;
using OpAdminDomain.Accounts;
using static Commons.BindCommandResultOptional;
using static Commons.RailWayOrientation;
using static Commons.TryCommandResult;

namespace OpAdminStorageLibrary.Queries.Accounts
{
    public class AccountDetailQueryReader : IQueryGetAccountDetail
    {
        private readonly IQueryDbConnection _connection;
        private Optional<AccountDetailResponseMessage> _result = Optional<AccountDetailResponseMessage>.Create();
        
        public AccountDetailQueryReader(IQueryDbConnection connection) => _connection = connection;
        
        public CommandResult<ErrorCode> GetAccountDetail(Guid accountId) => 
            Railway(
                CommandResult<ErrorCode>.Create,
                _connection.CreateConnection,
                _connection.OpenConnection,
                Bind(_connection.ReadConnection, x => ExecuteQuery(x,accountId), StorageError.ErrorCodeUnableToReadConnection)
            ).Finally(_connection.Dispose);

        private CommandResult<ErrorCode> ExecuteQuery(IDbConnection conn, Guid accountId) =>
            TryEcEx(() => _result = Optional<AccountDetailResponseMessage>.Create(conn.QuerySingleOrDefault<AccountDetailResponseMessage>(_queryDetail, 
                new {id = accountId})), StorageError.ErrorCodeUnableToExecuteQuery.ErrorNum);

        public Optional<AccountDetailResponseMessage> ReadResult() => _result;

        private string _queryDetail = @"
            SELECT 
                id,
                accountname,
                clientfirstname,
                clientlastname,
                operationmanager as useroperationmanagerid
            FROM accounts.accounts
            WHERE id = @id
        ";
    }
}