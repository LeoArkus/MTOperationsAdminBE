using System.Collections.Generic;
using Commons;

namespace OpAdminDomain
{
    public interface IParseRawRequest<T> where T : struct
    {
        CommandResult<ErrorCode> Parse();
        Optional<T> ReadParsed();
    }
    
    public interface IValidateModel<T> where T : struct
    {
        CommandResult<IEnumerable<ErrorCode>> IsValid(T toValidate);
    }
    
    public interface IGenerateIdentifiers<T> where T : struct
    {
        CommandResult<ErrorCode> GenerateGuidsForModel(T model);
        Optional<T> ReadModelWithIds();
    }
}