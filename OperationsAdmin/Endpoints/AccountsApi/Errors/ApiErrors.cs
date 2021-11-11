using Commons;

namespace OpAdminApi.Errors
{
    public static class ApiErrors
    {
        public static ErrorCode UnableToLoadResult = new ErrorCode("Unable to load read result", ApiErrorsEnum.UnableToLoadResult);
        public static ErrorCode UnableToParseRequest = new ErrorCode("Unable to parse received request", ApiErrorsEnum.UnableToParseRequest);

        private enum ApiErrorsEnum
        {
            UnableToLoadResult,
            UnableToParseRequest
        }
    }
}