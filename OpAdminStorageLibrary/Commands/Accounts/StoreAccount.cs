using System;
using System.Data;
using Commons;
using Dapper;
using OpAdminDomain.Accounts;
using static Commons.BindCommandResultOptional;
using static Commons.RailWayOrientation;

namespace OpAdminStorageLibrary.Commands.Accounts
{
    public class StoreAccountCommand : ICommandCreateAccount
    {
        private readonly ICommandDbConnection _dbConnection;

        public StoreAccountCommand(ICommandDbConnection dbConnection) => _dbConnection = dbConnection;

        public CommandResult<ErrorCode> StoreAccount(AccountCreateMessage accountMessage) =>
            Railway(
                _dbConnection.RollbackOnError,
                _dbConnection.CreateConnection,
                _dbConnection.OpenConnection,
                _dbConnection.CreateTransaction,
                Bind(_dbConnection.ReadConnectionTransaction, x=> ExecuteUpsert(x, accountMessage), StorageError.ErrorCodeUnableToReadConnection),
                _dbConnection.Commit
            ).Finally(_dbConnection.Dispose);

        private CommandResult<ErrorCode> ExecuteUpsert(Tuple<IDbConnection, IDbTransaction> conn, AccountCreateMessage accountMessage) =>
            TryCommandResult.TryEcEx(() => conn.Item1.Execute(_upsertQuery, new
            {
                id = accountMessage.Id,
                accountname = accountMessage.AccountName.Value,
                clientfirstname = accountMessage.ClientFirstName.AndThen(x=> x.Value, ()=>(object) DBNull.Value),
                clientlastname = accountMessage.ClientLastName.AndThen(x=> x.Value, ()=>(object) DBNull.Value),
                operationmanager = accountMessage.UserOperationManagerId
            }, conn.Item2), StorageError.ErrorCodeUnableToExecuteQuery.ErrorNum);

        private string _upsertQuery = @"
            INSERT INTO accounts.accounts
            (
              id,
              accountname,
              clientfirstname,
              clientlastname,
              operationmanager
            )
            VALUES
            (
              @id,
              @accountname,
              @clientfirstname,
              @clientlastname,
              @operationmanager     
            )
        ";
    }
}