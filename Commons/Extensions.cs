using System;

namespace Commons
{
    public static class CommonExtensions
    {
        public static T ToEnum<T>(this string value, bool ignoreCase = true) where T : Enum
            => !Enum.IsDefined(typeof(T), value)
                ? throw new Exception($"Value {value} of enum {typeof(T).Name} is not supported")
                : (T) Enum.Parse(typeof(T), value, ignoreCase);

        public static T ToEnum<T>(this string value, T valueWhenNotDefined, bool ignoreCase = true) where T : Enum =>
            Enum.IsDefined(typeof(T), value) ? (T) Enum.Parse(typeof(T), value, ignoreCase) : valueWhenNotDefined;

        public static bool IsNull<T>(this T obj) => object.ReferenceEquals(obj, null);
        public static bool IsStructNull<T>(this T source) where T : struct => source.Equals(default(T));
    }
}