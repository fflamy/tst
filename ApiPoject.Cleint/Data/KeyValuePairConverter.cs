using Newtonsoft.Json;

namespace ApiPoject.Cleint.Data
{
    public class KeyValuePairConverter : JsonConverter<List<KeyValuePair<int, string>>> { 

        public override void WriteJson(JsonWriter writer, List<KeyValuePair<int, string>> values, JsonSerializer serializer)
        {
        

            writer.WriteStartArray();
            foreach (var kvp in values)
            {
                writer.WriteStartObject();
                writer.WritePropertyName(kvp.Key.ToString());
                writer.WriteValue(kvp.Value);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }

        public override List<KeyValuePair<int, string>>? ReadJson(JsonReader reader, Type objectType, List<KeyValuePair<int, string>>? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
