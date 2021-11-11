using Commons;

namespace OpAdminApi.Errors
{
    public static class GrpcConnectionErrors
    {
        public static ErrorCode FailWhenTryingCommandAgent = new ErrorCode("Something went wrong when contacting command agent", ErrorsOfGrpc.FailWhenTryingCommandAgent);
        public static ErrorCode UnableToLoadResult = new ErrorCode("Unable to load read result", ErrorsOfGrpc.UnableToLoadResponse);
        public static ErrorCode FailWhenTryingQueryAgent = new ErrorCode("Something went wrong when contacting query agent", ErrorsOfGrpc.FailWhenTryingQueryAgent);
        public static ErrorCode UnableToParseResponse = new ErrorCode("Unable to parse Grpc response", ErrorsOfGrpc.UnableToParseResponse);
    }

    public enum ErrorsOfGrpc
    {
        FailWhenTryingCommandAgent,
        UnableToLoadResponse,
        FailWhenTryingQueryAgent,
        UnableToParseResponse
    }
}