using Commons;

namespace OpAdminEngine.Accounts
{
     public static class AccountProcessErrors
    {
        public static ErrorCode InvalidAccountName = new ("Invalid account name length, up to 50 characters", AccountProcessEnum.InvalidAccountName);
        public static ErrorCode InvalidClientFirstName = new ("Invalid client first name length, up to 50 characters", AccountProcessEnum.InvalidClientFirstName);
        public static ErrorCode InvalidClientLastName = new ("Invalid client last name length, up to 50 characters", AccountProcessEnum.InvalidClientLastName);
        public static ErrorCode OperationManagerDoesNotExist = new ("Operation manager user does not exist in storage", AccountProcessEnum.OperationManagerDoesNotExist);


        private enum AccountProcessEnum
        {
            InvalidAccountName,
            OperationManagerDoesNotExist,
            InvalidClientFirstName,
            InvalidClientLastName
        }
        
    }
}