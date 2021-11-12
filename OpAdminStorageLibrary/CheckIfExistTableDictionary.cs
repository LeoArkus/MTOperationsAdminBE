using System.Collections.Generic;
using OpAdminDomain;

namespace OpAdminStorageLibrary
{
    public static class StorageModelTables
    {
        public static readonly Dictionary<ModelTypeEnum, string> DictionaryTable = new()
        {
            { ModelTypeEnum.Accounts , "accounts.accounts"},
            { ModelTypeEnum.Users , "users.users"}
        };
    }
}