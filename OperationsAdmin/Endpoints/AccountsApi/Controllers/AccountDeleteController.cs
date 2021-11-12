using System;
using Commons;
using Microsoft.AspNetCore.Mvc;
using OpAdminApiBootstrap;
using OpAdminDomain;
using OpAdminDomain.Accounts;

namespace OpAdminApi.Controllers
{
    public class AccountDeleteController : Controller
    {
        private readonly IProcessAccountDelete _process;
        private const string Success = "Success";

        public AccountDeleteController(IBootstrapAccounts bootstrapAccounts, ICommandDeleteModel commandDeleteModel, IQueryCheckIfExist queryCheckIfExist) => 
            _process = bootstrapAccounts.BootstrapAccountDelete(commandDeleteModel, queryCheckIfExist);

        [Route("api/AccountDelete")]
        [HttpDelete]
        public IActionResult CreateAccount(Guid accountId) => 
            _process.ProcessAccountDelete(accountId).AndThen(OnSuccess, OnError);

        private IActionResult OnError(ErrorCode errorCodes) => BadRequest(errorCodes);

        private IActionResult OnSuccess() => Ok(Success);
    }
}