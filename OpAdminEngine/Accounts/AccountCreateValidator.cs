using System.Collections.Generic;
using Commons;
using OpAdminDomain;
using OpAdminDomain.Accounts;
using static Commons.RailWayOrientation;
using static OpAdminEngine.ValidationModel;

namespace OpAdminEngine.Accounts
{
    public class AccountCreateValidator : IValidateModel<AccountCreateMessage>
    {
        public CommandResult<IEnumerable<ErrorCode>> IsValid(AccountCreateMessage toValidate) =>
            RailFold(
                ()=> ValidateId(toValidate.Id, EngineErrors.InvalidId),
                ()=> toValidate.AccountName.IsValid.CheckValidation(AccountProcessErrors.InvalidAccountName),
                ()=> ValidateString(toValidate.ClientFirstName, AccountProcessErrors.InvalidClientFirstName),
                ()=> ValidateString(toValidate.ClientLastName, AccountProcessErrors.InvalidClientLastName)
            );
    }
}