using System;
using System.Data;
using Commons;
using Npgsql;
using static Commons.Nothing;
using static Commons.TryCommandResult;
using static OpAdminStorageLibrary.StorageError;

namespace OpAdminStorageLibrary
{
    public class CommandConnection : ICommandDbConnection
    {
        private readonly string _connectionString;
        private Optional<IDbConnection> _connection = Optional<IDbConnection>.Create();
        private Optional<IDbTransaction> _transaction = Optional<IDbTransaction>.Create();

        public CommandConnection(string connectionString) =>
            _connectionString = connectionString;

        public Optional<IDbTransaction> ReadTransaction() => _transaction;

        public CommandResult<ErrorCode> CreateTransaction()
        {
            void Create()
            {
                _connection.AndThen(
                    x => _transaction = Optional<IDbTransaction>.Create(x.BeginTransaction())
                    , () => throw new Exception(ErrorCodeUnavailableConnection.ErrorNum));
            }

            return TryEc(Create, ErrorCodeUnavailableTransaction);
        }

        public CommandResult<ErrorCode> OpenConnection()
        {
            void Open() => _connection.AndThen(x => x.Open(),
                () => throw new Exception(ErrorCodeUnavailableConnection.ErrorNum));

            return TryEcEx(Open, ErrorCodeUnableToOpenConnection.ErrorNum);
        }

        public CommandResult<ErrorCode> CreateConnection()
        {
            void Connect() => _connection = Optional<IDbConnection>.Create(new NpgsqlConnection(_connectionString));
            return TryEc(Connect, ErrorCodeUnavailableConnection);
        }

        public CommandResult<ErrorCode> Commit() =>
            TryEc(() => _transaction.AndThen((x) => x.Commit(), () => throw new Exception("Exception")),
                ErrorCodeUnableToCommitTransaction);


        public Optional<IDbConnection> ReadConnection() => _connection;
        public Optional<Tuple<IDbConnection, IDbTransaction>> ReadConnectionTransaction()
        {
            var result = Optional<Tuple<IDbConnection, IDbTransaction>>.Create();
            _connection.AndThen(x =>
            {
                _transaction.AndThen(y =>
                        result = Optional<Tuple<IDbConnection, IDbTransaction>>.Create(
                            new Tuple<IDbConnection, IDbTransaction>(x, y))
                    , DoNothing);
            }, DoNothing);
            return result;
        }

        public CommandResult<ErrorCode> RollbackOnError(ErrorCode error)
            =>  ReadTransaction().AndThen(
                    (tran) => TryEc(tran.Rollback, ErrorCodeUnableToRollbackTransaction),
                    () => CommandResult<ErrorCode>.Create(ErrorCodeUnableToReadTransaction)
                ).AndThen(
                    () => CommandResult<ErrorCode>.Create(error),
                    (x) => CommandResult<ErrorCode>.Create(error)
                );

        public void Dispose()
        {
            _transaction.AndThen(x => x.Dispose(), () => { });
            _connection.AndThen(x => x.Dispose(), () => { });
        }
    }

    public class QueryConnection : IQueryDbConnection
    {
        private readonly string _connectionString;
        private Optional<IDbConnection> _connection = Optional<IDbConnection>.Create();

        public QueryConnection(string connectionString) =>
            _connectionString = connectionString;

        public void Dispose() => _connection.AndThen(x => x.Dispose(), DoNothing);

        public CommandResult<ErrorCode> OpenConnection()
        {
            void Open() => _connection.AndThen(x => x.Open(),
                () => throw new Exception(ErrorCodeUnableToOpenConnection.ErrorNum));

            return TryEc(Open, ErrorCodeUnableToOpenConnection);
        }

        public CommandResult<ErrorCode> CreateConnection()
        {
            void Connect() => _connection = Optional<IDbConnection>.Create(new NpgsqlConnection(_connectionString));
            return TryEc(Connect, ErrorCodeUnavailableConnection);
        }

        public Optional<IDbConnection> ReadConnection() => _connection;
        public object Clone()
        {
            return new QueryConnection(_connectionString);
        }
    }
}
