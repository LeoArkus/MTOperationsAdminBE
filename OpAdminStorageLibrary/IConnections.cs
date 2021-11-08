using System;
using System.Data;
using Commons;

namespace OpAdminStorageLibrary
{
    public interface ICommandDbConnection : IDisposable
    {
        Optional<IDbTransaction> ReadTransaction();
        CommandResult<ErrorCode> CreateTransaction();
        CommandResult<ErrorCode> OpenConnection();
        CommandResult<ErrorCode> CreateConnection();
        CommandResult<ErrorCode> Commit();
        Optional<IDbConnection> ReadConnection();
        Optional<Tuple<IDbConnection, IDbTransaction>> ReadConnectionTransaction();
        CommandResult<ErrorCode> RollbackOnError(ErrorCode error);
    }
    
    public interface IQueryDbConnection : IDisposable , ICloneable
    {
        CommandResult<ErrorCode> OpenConnection();
        CommandResult<ErrorCode> CreateConnection();
        Optional<IDbConnection> ReadConnection();
    }
}