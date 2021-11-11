using System;
using System.Collections.Generic;

namespace Commons
{
    public static class BindCommandResultOptional
    {
        public static Func<CommandResult<T>> Bind<T, TM>(Func<Optional<TM>> readFunction,
            Func<TM, CommandResult<T>> next, T onFailure)
            => () => readFunction().AndThen(next, () => CommandResult<T>.Create(onFailure));
        
        public static Func<CommandResult<IEnumerable<T>>> BindEnumerable<T, TM>(Func<Optional<TM>> readFunction,
            Func<TM, CommandResult<T>> next, T onFailure) => 
            () => readFunction().AndThen(next, () => CommandResult<T>.Create(onFailure)).ToEnumerable();
    }
}