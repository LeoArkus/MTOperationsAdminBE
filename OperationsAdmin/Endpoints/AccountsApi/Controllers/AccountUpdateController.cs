using System.Collections.Generic;
using Commons;
using Microsoft.AspNetCore.Mvc;
using OpAdminApi.Parsers;
using OpAdminApi.Requests;
using OpAdminApiBootstrap;
using OpAdminDomain;
using OpAdminDomain.Accounts;

namespace OpAdminApi.Controllers
{
    public class AccountUpdateController : Controller
    {
        private readonly IProcessAccountUpdate _process;
        private const string Success = "Success";

        public AccountUpdateController(IBootstrapAccounts bootstrapAccounts, ICommandCreateAccount commandCreateAccount, IQueryCheckIfExist queryCheckIfExist) => 
            _process = bootstrapAccounts.BootstrapAccountUpdate(commandCreateAccount, queryCheckIfExist);

        [Route("api/AccountUpdate")]
        [HttpPut]
        public IActionResult CreateAccount([FromBody] AccountUpdateRequest request) => 
            _process.ProcessAccountUpdate(new AccountUpdateParser(request)).AndThen(OnSuccess, OnError);

        private IActionResult OnError(IEnumerable<ErrorCode> errorCodes) => BadRequest(errorCodes);

        private IActionResult OnSuccess() => Ok(Success);
    }
}