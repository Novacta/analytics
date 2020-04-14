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
    /// Converts a <see cref="CategoricalDataSet"/> or value to or from JSON.
    /// </summary>
    class JsonCategoricalDataSetConverter : JsonConverter<CategoricalDataSet>
    {
        ///<inheritdoc/>
        public override CategoricalDataSet Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            #region Variables

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string propertyName = reader.GetString();
            if (propertyName != "Variables")
            {
                throw new JsonException();
            }

            var variables =
                JsonSerializer.Deserialize<List<CategoricalVariable>>(
                    ref reader,
                    options);

            #endregion

            #region Data

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            propertyName = reader.GetString();
            if (propertyName != "Data")
            {
                throw new JsonException();
            }

            var data =
                JsonSerializer.Deserialize<ReadOnlyDoubleMatrix>(
                    ref reader,
                    options);

            #endregion

            var categoricalDataSet = 
                new CategoricalDataSet(
                    variables, 
                    data.matrix);

            #region Name

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            propertyName = reader.GetString();
            if (propertyName != "Name")
            {
                throw new JsonException();
            }

            reader.Read();
            switch (reader.TokenType)
            {
                case JsonTokenType.String:
                    categoricalDataSet.Name = reader.GetString();
                    break;
                case JsonTokenType.Null:
                    categoricalDataSet.Name = null;
                    break;
                default:
                    throw new JsonException();
            }

            #endregion

            reader.Read();
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return categoricalDataSet;
            }

            throw new JsonException();
        }

        ///<inheritdoc/>
        public override void Write(
            Utf8JsonWriter writer,
            CategoricalDataSet value,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("Variables");
            JsonSerializer.Serialize<IReadOnlyList<CategoricalVariable>>(
                writer,
                value.Variables,
                options);

            writer.WritePropertyName("Data");
            JsonSerializer.Serialize<ReadOnlyDoubleMatrix>(
                writer,
                value.Data,
                options);

            writer.WriteString("Name", value.Name);

            writer.WriteEndObject();
        }
    }
}
