using Commons;

namespace OpAdminStorageLibrary
{
    public static class StorageError
    {
        private static string UnavailableConnection = "Connection Not Available";
        private static string UnableToOpenConnection = "Unable To Open Connection";
        private static string UnableToReadConnection = "Unable To Read Connection";
        private static string UnavailableTransaction = "Transaction Not Available";
        private static string UnableToReadTransaction = "Unable To Read Transaction";
        private static string UnableToCommitTransaction = "Unable to commit transaction";
        private static string UnableToExecuteQuery = "Unable to execute Query";
        private static string UnableToReadQueryResult = "Unable to read/assign result from query execution";
        private static string UnableToDispose = "Unable to dispose connection, threw exception";
        private static string UnableToRollbackTransaction = "Unable to successfully rollback transaction";
        private static string UnableToCreateParameters = "Unable to Create Query parameters";
        private static string UnableToBuildQuery = "Unable to Build Query";
        private static string UnableToParse = "Unable to parse database result";


        public static ErrorCode ErrorCodeUnavailableConnection = new ErrorCode(UnavailableConnection, StorageErrorEnum.ConnectionUnavailable);
        public static ErrorCode ErrorCodeUnableToOpenConnection = new ErrorCode(UnableToOpenConnection, StorageErrorEnum.CannotOpenConnection);
        public static ErrorCode ErrorCodeUnableToReadConnection = new ErrorCode(UnableToReadConnection, StorageErrorEnum.UnableToReadConnection);
        public static ErrorCode ErrorCodeUnavailableTransaction = new ErrorCode(UnavailableTransaction, StorageErrorEnum.TransactionUnavailable);
        public static ErrorCode ErrorCodeUnableToReadTransaction = new ErrorCode(UnableToReadTransaction, StorageErrorEnum.UnableToReadTransaction);
        public static ErrorCode ErrorCodeUnableToCommitTransaction = new ErrorCode(UnableToCommitTransaction, StorageErrorEnum.UnableToCommitTransaction);
        public static ErrorCode ErrorCodeUnableToExecuteQuery = new ErrorCode(UnableToExecuteQuery, StorageErrorEnum.UnableToExecuteQuery);
        public static ErrorCode ErrorCodeUnableToReadQueryResult = new ErrorCode(UnableToReadQueryResult, StorageErrorEnum.UnableToReadQueryResult);
        public static ErrorCode ErrorCodeUnableToDisposeConnection = new ErrorCode(UnableToDispose, StorageErrorEnum.UnableToDispose);
        public static ErrorCode ErrorCodeUnableToRollbackTransaction = new ErrorCode(UnableToRollbackTransaction, StorageErrorEnum.UnableToRollbackTransaction);
        public static ErrorCode ErrorCodeUnableToCreateParameters = new ErrorCode(UnableToCreateParameters, StorageErrorEnum.UnableToCreateSqlParameters);
        public static ErrorCode ErrorCodeUnableToBuildQuery = new ErrorCode(UnableToBuildQuery, StorageErrorEnum.UnableToBuildQuery);
        public static ErrorCode ErrorCodeUnableToParseDatabaseResult = new ErrorCode(UnableToParse, StorageErrorEnum.UnableToParseDataBaseResult);

        private enum StorageErrorEnum
        {
            ConnectionUnavailable,
            CannotOpenConnection,
            UnableToReadConnection,
            TransactionUnavailable,
            UnableToReadTransaction,
            UnableToCommitTransaction,
            UnableToExecuteQuery,
            UnableToReadQueryResult,
            UnableToDispose,
            UnableToRollbackTransaction,
            UnableToCreateSqlParameters,
            UnableToBuildQuery,
            UnableToParseDataBaseResult
        }
    }
}