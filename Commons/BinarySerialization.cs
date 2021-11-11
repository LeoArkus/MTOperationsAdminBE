using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Commons
{
    public static class Binary
    {
        public static byte[] Serialize<T>(T toSerialize)
        {
            using var memoryStream = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(memoryStream, toSerialize);
            return memoryStream.ToArray();
        }

        public static T Deserialize<T>(this byte[] serializedValue)
        {
            using var memoryStream = new MemoryStream(serializedValue);
            BinaryFormatter bfd = new BinaryFormatter();
            return (T)bfd.Deserialize(memoryStream);
        }
    }
}