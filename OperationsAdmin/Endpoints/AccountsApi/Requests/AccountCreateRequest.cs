#nullable enable
using System;

namespace OpAdminApi.Requests
{
    public struct AccountCreateRequest
    {
        public string AccountName { get; set; }
        public string? ClientFirstName { get; set; }
        public string? ClientLastName { get; set; }
        public Guid UserOperationManagerId { get; set; }
    }
}