using System;
using System.Collections.Generic;
using Commons;
using static Commons.CommandResult<Commons.ErrorCode>;

namespace OpAdminEngine
{
    public static class ValidationModel
    {
        public static CommandResult<ErrorCode> ValidateDate(Optional<DateTime> value, ErrorCode errorCode) => value.AndThen(x => x.IsNull() ? Create(errorCode) : Create(), Create);

        public static CommandResult<ErrorCode> ValidateString(Optional<String50> value, ErrorCode errorCode) => value.AndThen(x => x.IsValid ? Create() : Create(errorCode), Create);
        
        public static CommandResult<ErrorCode> ValidateId(Guid id, ErrorCode errorCode) => id.Equals(Guid.Empty) ? Create(errorCode) : Create();

        public static CommandResult<ErrorCode> ValidateId(Optional<Guid> value, ErrorCode errorCode) => value.AndThen(x => x.Equals(Guid.Empty) ? Create(errorCode) : Create(), Create);
        
        public static CommandResult<ErrorCode> ValidateId(List<Guid> list, ErrorCode error) => list.TrueForAll(x => x != Guid.Empty) ? Create() : Create(error);
        
        public static CommandResult<ErrorCode> ValidateSingleId(Optional<Guid> firstId, Optional<Guid> secondId, ErrorCode errorCode) =>
            (firstId.IsNothing() ^ secondId.IsNothing()).CheckValidation(errorCode);
    }
}