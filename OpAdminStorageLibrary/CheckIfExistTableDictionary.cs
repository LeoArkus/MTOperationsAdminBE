using System.Collections.Generic;
using OpAdminDomain;

namespace OpAdminStorageLibrary
{
    public static class CheckIfExistTable
    {
        public static Dictionary<ModelTypeEnum, string> DictionaryTableExist = new Dictionary<ModelTypeEnum, string>()
        {
            { ModelTypeEnum.Accounts , "accounts.accounts"},
            { ModelTypeEnum.Users , "users.users"}
        };
    }
}