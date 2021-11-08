using System;

namespace Commons
{
    [Serializable]
    public abstract class Optional<T>
    {
        public abstract bool IsNothing();
        public abstract M AndThen<M>(Func<T, M> OnValue, Func<M> OnNothing);
        public abstract void AndThen(Action<T> OnValue, Action OnNothing);
        
        [Serializable]
        private class Some : Optional<T>
        {
            private readonly T value;
            public Some(T value) => this.value = value;
            public override M AndThen<M>(Func<T, M> OnValue, Func<M> OnNothing) => OnValue(value);
            public override void AndThen(Action<T> OnValue, Action OnNothing) => OnValue(value);
            public T GetValue() => value;
            public override bool IsNothing() => false;
        }
        [Serializable]
        private class None : Optional<T>
        {
            public override M AndThen<M>(Func<T, M> OnValue, Func<M> OnNothing) => OnNothing();
            public override void AndThen(Action<T> OnValue, Action OnNothing) => OnNothing();
            public override bool IsNothing() => true;
        }

        public static Optional<T> Create(T value)
        {
            if (value == null)
                return new None();
            return new Some(value);
        }

        public static Optional<T> Create() => new None();
    }
}