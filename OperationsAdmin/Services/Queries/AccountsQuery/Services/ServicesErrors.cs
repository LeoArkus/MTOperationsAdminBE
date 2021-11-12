using Commons;

namespace AccountsQuery.Services
{
    public static class ServicesError
    {
        public static ErrorCode UnableToReadReport = new ErrorCode("Unable to read report service", ServiceErrorEnum.UnableToReadReport);

        private enum ServiceErrorEnum
        {
            UnableToReadReport
        }
    }
}