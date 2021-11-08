using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Commons.Nothing;

namespace Commons
{
    public abstract class CommandResult<T>
    {
        public abstract bool IsSuccess();
        public abstract Optional<T> Errors();
        public abstract void AndThen(Action onSuccess, Action<T> onError);
        public abstract M AndThen<M>(Func<M> onSuccess, Func<T, M> onError);
        private class Success : CommandResult<T>
        {
            public override bool IsSuccess() => true;
            public override Optional<T> Errors() => Optional<T>.Create();
            public override void AndThen(Action onSuccess, Action<T> onError) => onSuccess();
            public override M AndThen<M>(Func<M> onSuccess, Func<T, M> onError) => onSuccess();
        }
        private class Failure : CommandResult<T>
        {
            private readonly T _error;
            public Failure(T error)
            {
                _error = error;
            }
            public override bool IsSuccess() => false;
            public override Optional<T> Errors() => Optional<T>.Create(_error);
            public override void AndThen(Action onSuccess, Action<T> onError) => onError(_error);
            public override M AndThen<M>(Func<M> onSuccess, Func<T, M> onError) => onError(_error);
        }
        public static CommandResult<T> Create() => new Success();
        public static CommandResult<T> Create(T error) => new Failure(error);
        
    }
    
    public static class RailWayOrientation {
        public static CommandResult<IEnumerable<T>> ToEnumerable<T>(this CommandResult<T> value)
            => value.AndThen(CommandResult<IEnumerable<T>>.Create,
                (x) => CommandResult<IEnumerable<T>>.Create(new List<T>() {x}));

        public static async Task<CommandResult<IEnumerable<T>>> RailFoldAsync<T>(params Func<CommandResult<T>>[] toRail)
        {
            var results = await Task.WhenAll(toRail.Select(Task.Run)).ConfigureAwait(false);
            var errors = results.Aggregate(new List<T>(), (state, command) =>
            { 
                command.AndThen(DoNothing, state.Add); 
                return state; 
            }, list => list);
            return errors.Count > 0 ? CommandResult<IEnumerable<T>>.Create(errors) : CommandResult<IEnumerable<T>>.Create();
        }

        public static CommandResult<T> CheckValidation<T>(this bool isValid, T error) => isValid ? CommandResult<T>.Create() : CommandResult<T>.Create(error);
        public static Func<CommandResult<T>> CheckValidationFunc<T>(this bool isValid, T error) => () => isValid.CheckValidation(error);

        public static CommandResult<T> Finally<T>(this CommandResult<T> result, Action execute)
        {
            execute();
            return result;
        }
        
        public static CommandResult<IEnumerable<T>> AndThenCombine<T>(this CommandResult<IEnumerable<T>> result, CommandResult<IEnumerable<T>> execute) =>
            result.AndThen(()=> execute, (x) => 
                execute.AndThen(() => CommandResult<IEnumerable<T>>.Create(x), y => CommandResult<IEnumerable<T>>.Create(x.Concat(y))));

        public static CommandResult<T> Railway<T>(Func<T, CommandResult<T>> onError, params Func<CommandResult<T>>[] toRail)
        {
            if(toRail.Length > 0)
                return toRail[0]().AndThen(
                    () => Railway(onError, toRail.Skip(1).ToArray()), 
                    onError);
            return CommandResult<T>.Create();
        }

        public static CommandResult<IEnumerable<T>> RailFold<T>(params Func<CommandResult<T>>[] toRail)
        {
            var result = toRail.Aggregate(new List<T>(),
                (state, next) =>
                {
                    next().AndThen(DoNothing, state.Add);
                    return state;
                }, list => list);
            return result.Count > 0 ? CommandResult<IEnumerable<T>>.Create(result) : CommandResult<IEnumerable<T>>.Create();
        }

        public static CommandResult<T> ExecuteSuccess<T>(Action toExecute)
        {
            toExecute();
            return CommandResult<T>.Create();
        }
        
        public static Func<CommandResult<T>> ExecuteSuccessFunc<T>(Action toExecute)
            => () => ExecuteSuccess<T>(toExecute);
    }
}