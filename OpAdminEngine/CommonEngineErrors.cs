using Commons;

namespace OpAdminEngine
{
    public static class EngineErrors
    {
        public static ErrorCode UnableToReadParsed = new ErrorCode("Unable To Read Parsed Model", EngineErrorsEnum.UnableToReadParsed);
        public static ErrorCode UnableToReadFromGenerator = new ErrorCode("Unable To Read generated ids for Model", EngineErrorsEnum.UnableToReadFromGenerator);
        public static ErrorCode UnableToGenerateIdentifiers = new ErrorCode("Failed to generate identifiers", EngineErrorsEnum.UnableToGenerateIdentifiers);
        public static ErrorCode InvalidId = new ErrorCode("Invalid Id, cannot be empty", EngineErrorsEnum.InvalidId);

        private enum EngineErrorsEnum
        {
            UnableToReadParsed,
            InvalidId,
            UnableToReadFromGenerator,
            UnableToGenerateIdentifiers
        }

    }
}