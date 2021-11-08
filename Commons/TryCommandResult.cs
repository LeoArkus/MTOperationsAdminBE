using System;

namespace Commons
{
    public static class TryCommandResult
    {
        public static CommandResult<ErrorCode> TryEc(Action toTry, ErrorCode code)
        {
            try
            {
                toTry();
                return CommandResult<ErrorCode>.Create();
            }
            catch (Exception)
            {
                return CommandResult<ErrorCode>.Create(code);
            }
        }
        
        public static CommandResult<ErrorCode> TryEcEx(Action toTry, Enum code)
        {
            try
            {
                toTry();
                return CommandResult<ErrorCode>.Create();
            }
            catch (Exception ex)
            {
                return CommandResult<ErrorCode>.Create(new ErrorCode(ex.Message, code.ToString()));
            }
        }
        
        public static CommandResult<ErrorCode> TryEcEx(Action toTry, string code)
        {
            try
            {
                toTry();
                return CommandResult<ErrorCode>.Create();
            }
            catch (Exception ex)
            {
                return CommandResult<ErrorCode>.Create(new ErrorCode(ex.Message, code));
            }
        }

        public static Func<CommandResult<ErrorCode>> TryEcExFunc(Action toTry, string code) =>
            () => TryEcEx(toTry, code);
        
        public static Func<CommandResult<ErrorCode>> TryEcExFunc(Action toTry, Enum code) =>
            () => TryEcEx(toTry, code);

        public static Func<CommandResult<ErrorCode>> TryEcFunc(Action toTry, ErrorCode code) => () => TryEc(toTry, code);

    }
}