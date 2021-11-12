using System;
using Commons;
using Microsoft.AspNetCore.Mvc;
using OpAdminApi.Errors;
using OpAdminApiBootstrap;
using OpAdminDomain;
using OpAdminDomain.Accounts;

namespace OpAdminApi.Controllers
{
    public class AccountDetailController : Controller
    {
        private readonly IProcessAccountDetail _process;

        public AccountDetailController(IBootstrapAccounts bootstrapAccounts, IQueryGetAccountDetail queryGetAccountDetail, IQueryCheckIfExist queryCheckIfExist) => 
            _process = bootstrapAccounts.BootstrapAccountDetail(queryGetAccountDetail, queryCheckIfExist);

        [Route("api/AccountRead")]
        [HttpGet]
        public IActionResult ReadAccount(Guid accountId) => 
            _process.ProcessAccountDetail(accountId).AndThen(OnSuccess, OnError);

        private IActionResult OnError(ErrorCode errorCodes) => BadRequest(errorCodes);

        private IActionResult OnSuccess() => _process.ReadResult().AndThen(x => Ok(x), () => OnError(ApiErrors.UnableToLoadResult));
    }
}