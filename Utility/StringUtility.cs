using System.Text.Json;

namespace Utility
{
    public class StringUtility
    {
        public static string[] ConvertStringToStringArray(string input)
        {
            return new[] { input };
        }

        public static string ConvertStringArrayToString(string input, char charSplit)
        {
            return string.Join(charSplit, input);
        }

        public static string SerializeObjectToJson(object objectData)
        {
            return JsonSerializer.Serialize(objectData);
        }

        public static T DeserializeJsonToObject<T>(string jsonValue)
        {
            return (T)JsonSerializer.Deserialize(jsonValue, typeof(T));
        }
    }
}
