using System;
using System.Collections.Generic;

namespace Commons
{
    [Serializable]
    public struct ErrorCode
    {
        public string LoggedMessage { get; set; }
        public string ErrorNum { get; set; }

        public ErrorCode(string loggedMessage, string code)
        {
            LoggedMessage = loggedMessage;
            ErrorNum = code;
        }
        
        public ErrorCode(string loggedMessage, Enum code)
        {
            LoggedMessage = loggedMessage;
            ErrorNum = code.ToString();
        }
    }

    public static class ErrorCodeToEnum
    {
        public static IEnumerable<ErrorCode> ToEnumerable(this ErrorCode errorCode) => new List<ErrorCode>() { errorCode };
    }
}