// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license.  
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Novacta.Analytics.Infrastructure
{
    /// <summary>
    /// Converts a <see cref="CategoricalVariable"/> or value to or from JSON.
    /// </summary>
    class JsonCategoricalVariableConverter : JsonConverter<CategoricalVariable>
    {
        ///<inheritdoc/>
        public override CategoricalVariable Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            #region Name

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            var propertyName = reader.GetString();
            if (propertyName != "Name")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            var categoricalVariable =
                new CategoricalVariable(reader.GetString());
            
            #endregion

            #region Categories

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            propertyName = reader.GetString();
            if (propertyName != "Categories")
            {
                throw new JsonException();
            }

            var categories =
                JsonSerializer.Deserialize<IReadOnlyList<Category>>(
                    ref reader,
                    options);

            foreach (var category in categories)
            {
                categoricalVariable.Add(category.Code, category.Label);
            }

            #endregion

            #region IsReadOnly

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            propertyName = reader.GetString();
            if (propertyName != "IsReadOnly")
            {
                throw new JsonException();
            }

            reader.Read();
            switch (reader.TokenType)
            {
                case JsonTokenType.True:
                    categoricalVariable.SetAsReadOnly();
                    break;
                case JsonTokenType.False:
                    break;
                default:
                    throw new JsonException();
            }

            #endregion

            reader.Read();
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return categoricalVariable;
            }

            throw new JsonException();
        }

        ///<inheritdoc/>
        public override void Write(
            Utf8JsonWriter writer,
            CategoricalVariable value,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("Name", value.Name);

            writer.WritePropertyName("Categories");
            JsonSerializer.Serialize<IReadOnlyList<Category>>(
                writer,
                value.Categories,
                options);

            writer.WriteBoolean("IsReadOnly", value.IsReadOnly);

            writer.WriteEndObject();
        }
    }
}
