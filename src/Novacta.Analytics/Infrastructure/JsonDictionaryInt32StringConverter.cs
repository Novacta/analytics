// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license.  
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Novacta.Analytics.Infrastructure
{
    /// <summary>
    /// Converts a dictionary of integer and string pairs or 
    /// value to or from JSON.
    /// </summary>
    class JsonDictionaryInt32StringConverter :
        JsonConverter<Dictionary<int, string>>
    {
        ///<inheritdoc/>
        public override Dictionary<int, string> Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var value = new Dictionary<int, string>();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return value;
                }

                string keyAsString = reader.GetString();
                if (!int.TryParse(keyAsString, out int keyAsInt))
                {
                    throw new JsonException();
                }

                reader.Read();
                string itemValue = reader.GetString();

                value.Add(keyAsInt, itemValue);
            }

            throw new JsonException();
        }

        ///<inheritdoc/>
        public override void Write(
            Utf8JsonWriter writer,
            Dictionary<int, string> value,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            foreach (KeyValuePair<int, string> item in value)
            {
                writer.WriteString(
                    item.Key.ToString(CultureInfo.InvariantCulture),
                    item.Value);
            }

            writer.WriteEndObject();
        }
    }
}