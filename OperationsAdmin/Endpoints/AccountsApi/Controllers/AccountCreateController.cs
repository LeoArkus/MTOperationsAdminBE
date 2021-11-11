using System.Collections.Generic;
using Commons;
using Microsoft.AspNetCore.Mvc;
using OpAdminApi.Parsers;
using OpAdminApi.Requests;
using OpAdminApiBootstrap;
using OpAdminDomain.Accounts;

namespace OpAdminApi.Controllers
{
    public class AccountCreateController : Controller
    {
        private readonly IProcessAccountCreate _process;
        private const string Success = "Success";

        public AccountCreateController(IBootstrapAccounts bootstrapAccounts, ICommandCreateAccount commandCreateAccount) => 
            _process = bootstrapAccounts.BootstrapAccountCreate(commandCreateAccount);

        [Route("api/AccountCreate")]
        [HttpPost]
        public IActionResult CreateAccount([FromBody] AccountCreateRequest request) => 
            _process.ProcessAccountCreate(new AccountCreateParser(request)).AndThen(OnSuccess, OnError);

        private IActionResult OnError(IEnumerable<ErrorCode> errorCodes) => BadRequest(errorCodes);

        private IActionResult OnSuccess() => Ok(Success);
    }
}