using Commons;

namespace OpAdminEngine.Accounts
{
     public static class AccountProcessErrors
    {
        public static ErrorCode InvalidAccountName = new ErrorCode("Invalid account name length, up to 50 characters", AccountProcessEnum.InvalidAccountName);
        public static ErrorCode InvalidClientFirstName = new ErrorCode("Invalid client first name length, up to 50 characters", AccountProcessEnum.InvalidAccountName);
        public static ErrorCode InvalidClientLastName = new ErrorCode("Invalid client last name length, up to 50 characters", AccountProcessEnum.InvalidAccountName);


        private enum AccountProcessEnum
        {
            InvalidAccountName
        }
        
    }
}