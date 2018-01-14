using System;
using Newtonsoft.Json;

namespace BoolCustomConvertJsonNet
{
    class Program
    {
        static void Main(string[] args)
        {
            var test1 = new TestClass
            {
                Success = 1
            };
            var test2 = new TestClass
            {
                Success = 0
            };
            var ser1 = JsonConvert.SerializeObject(test1);
            var ser2 = JsonConvert.SerializeObject(test2);
            Console.WriteLine(ser1);
            Console.WriteLine(ser2);
            var des1 = JsonConvert.DeserializeObject<TestClass>(ser1).Success;
            Console.WriteLine(des1);
            var des2 = JsonConvert.DeserializeObject<TestClass>(ser2).Success;
            Console.WriteLine(des2);
            Console.ReadKey();
        }
    }

    public class TestClass
    {
        [JsonConverter(typeof(BoolConverter))]
        public byte Success { get; set; }
    }

    public class BoolConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue((byte)value == 1 ? true : (byte)value == 0 ? false : value);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value.ToString().ToLowerInvariant() == "true")
            {
                return Convert.ChangeType(1, objectType);
            }
            if (reader.Value.ToString().ToLowerInvariant() == "false")
            {
                return Convert.ChangeType(0, objectType);
            }
            return -1;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(byte) || objectType == typeof(int);
        }
    }
}
