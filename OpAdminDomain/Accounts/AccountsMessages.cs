using System;
using Commons;

namespace OpAdminDomain.Accounts
{
    public struct AccountCreateMessage
    {
        public Guid Id { get; set; }
        public String50 AccountName { get; set; }
        public Optional<String50> ClientFirstName { get; set; }
        public Optional<String50> ClientLastName { get; set; }
        public Guid UserOperationManagerId { get; set; }
    }
}