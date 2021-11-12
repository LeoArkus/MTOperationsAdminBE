using System.Collections.Generic;
using Commons;
using Microsoft.AspNetCore.Mvc;
using OpAdminApi.Errors;
using OpAdminApi.Parsers;
using OpAdminApi.Requests;
using OpAdminApiBootstrap;
using OpAdminDomain;
using OpAdminDomain.Accounts;

namespace OpAdminApi.Controllers
{
    public class AccountCreateController : Controller
    {
        private readonly IProcessAccountCreate _process;

        public AccountCreateController(IBootstrapAccounts bootstrapAccounts, ICommandCreateAccount commandCreateAccount, IQueryCheckIfExist queryCheckIfExist) => 
            _process = bootstrapAccounts.BootstrapAccountCreate(commandCreateAccount, queryCheckIfExist);

        [Route("api/AccountCreate")]
        [HttpPost]
        public IActionResult CreateAccount([FromBody] AccountCreateRequest request) => 
            _process.ProcessAccountCreate(new AccountCreateParser(request)).AndThen(OnSuccess, OnError);

        private IActionResult OnError(IEnumerable<ErrorCode> errorCodes) => BadRequest(errorCodes);

        private IActionResult OnSuccess() => _process.ReadResult().AndThen(x => Ok(x), ()=> OnError(ApiErrors.UnableToLoadResult.ToEnumerable()));
    }
}