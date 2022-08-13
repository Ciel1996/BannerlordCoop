using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common
{
    public class CommonSerializer
    {
        public static byte[] Serialize(object obj)
        {
            using(MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        private static object DeserializeBytes(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(ms);
            }
        }

        public static T Deserialize<T>(ArraySegment<byte> bytes)
        {
            return (T)Deserialize(bytes.Array);
        }

        public static T Deserialize<T>(byte[] bytes)
        {
            return (T)DeserializeBytes(bytes);
        }

        public static object Deserialize(ArraySegment<byte> bytes)
        {
            return Deserialize(bytes.Array);
        }

        public static object Deserialize(byte[] bytes)
        {
            return DeserializeBytes(bytes);
        }
    }
}
