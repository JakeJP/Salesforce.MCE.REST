using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Yokinsoft.Salesforce.MCE
{
    internal class CaseInsensitiveDictionaryConverter : JsonConverter<Dictionary<string, string>>
    {
        public override Dictionary<string, string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token.");
            }

            var dictionary = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return dictionary;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected PropertyName token.");
                }

                string key = reader.GetString();

                reader.Read(); // Advance to the value
                string value = reader.GetString();

                dictionary.Add(key, value);
            }

            throw new JsonException("Unexpected end of JSON.");
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<string, string> value, JsonSerializerOptions options)
        {
            // For serialization, you might choose to write keys as they are,
            // or apply a specific casing if needed.
            // This example simply writes the dictionary as a standard JSON object.
            writer.WriteStartObject();
            foreach (var item in value)
            {
                writer.WriteString(item.Key, item.Value);
            }
            writer.WriteEndObject();
        }
    }
}
