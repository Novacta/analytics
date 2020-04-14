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
    /// Converts a <see cref="DoubleMatrix"/> or value to or from JSON.
    /// </summary>
    class JsonDoubleMatrixConverter : JsonConverter<DoubleMatrix>
    {
        ///<inheritdoc/>
        public override DoubleMatrix Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            #region Implementor

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string propertyName = reader.GetString();
            if (propertyName != "Implementor")
            {
                throw new JsonException();
            }

            var matrixImplementor = 
                JsonSerializer.Deserialize<MatrixImplementor<double>>(
                    ref reader, 
                    options);

            #endregion

            DoubleMatrix matrix = new DoubleMatrix(matrixImplementor);

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
                    matrix.Name = reader.GetString();
                    break;
                case JsonTokenType.Null:
                    matrix.Name = null;
                    break;
                default:
                    throw new JsonException();
            }

            #endregion

            #region Row names

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            propertyName = reader.GetString();
            if (propertyName != "RowNames")
            {
                throw new JsonException();
            }

            matrix.rowNames = 
                JsonSerializer.Deserialize<Dictionary<int,string>>(
                    ref reader, 
                    options);

            #endregion

            #region Column names

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            propertyName = reader.GetString();
            if (propertyName != "ColumnNames")
            {
                throw new JsonException();
            }

            matrix.columnNames =
                JsonSerializer.Deserialize<Dictionary<int, string>>(
                    ref reader,
                    options);

            #endregion

            reader.Read();
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return matrix;
            }

            throw new JsonException();
        }

        ///<inheritdoc/>
        public override void Write(
            Utf8JsonWriter writer,
            DoubleMatrix value,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("Implementor");
            JsonSerializer.Serialize<MatrixImplementor<double>>(
                writer,
                value.implementor,
                options);

            writer.WriteString("Name", value.Name);

            writer.WritePropertyName("RowNames");
            JsonSerializer.Serialize<Dictionary<int, string>>(
                writer,
                value.rowNames,
                options);

            writer.WritePropertyName("ColumnNames");
            JsonSerializer.Serialize<Dictionary<int, string>>(
                writer,
                value.columnNames,
                options);

            writer.WriteEndObject();
        }
    }
}
