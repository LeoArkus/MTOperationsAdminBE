using System;
using System.Text.Json;

namespace Commons
{
    public static class Binary
    {
        public static string Serialize<T>(T toSerialize) => JsonSerializer.Serialize(toSerialize);

        public static T Deserialize<T>(this string serializedValue) => JsonSerializer.Deserialize<T>(serializedValue);
    }
}