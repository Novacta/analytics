// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license.  
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Novacta.Analytics.Infrastructure
{
    /// <summary>
    /// Converts a <see cref="Category"/> or value to or from JSON.
    /// </summary>
    class JsonCategoryConverter : JsonConverter<Category>
    {
        ///<inheritdoc/>
        public override Category Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            #region Code

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            var propertyName = reader.GetString();
            if (propertyName != "Code")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            var code = reader.GetDouble();

            #endregion

            #region Label

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            propertyName = reader.GetString();
            if (propertyName != "Label")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            var label = reader.GetString();

            #endregion

            reader.Read();
            if (reader.TokenType != JsonTokenType.EndObject)
            {
                throw new JsonException();
            }
                
            return new Category(code, label);
        }

        ///<inheritdoc/>
        public override void Write(
            Utf8JsonWriter writer,
            Category value,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteNumber("Code", value.Code);

            writer.WriteString("Label", value.Label);

            writer.WriteEndObject();
        }
    }
}
