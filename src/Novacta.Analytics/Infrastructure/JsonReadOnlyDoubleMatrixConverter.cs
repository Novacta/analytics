// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license.  
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Novacta.Analytics.Infrastructure
{
    /// <summary>
    /// Converts a <see cref="ReadOnlyDoubleMatrix"/> or value to or from JSON.
    /// </summary>
    class JsonReadOnlyDoubleMatrixConverter : JsonConverter<ReadOnlyDoubleMatrix>
    {
        ///<inheritdoc/>
        public override ReadOnlyDoubleMatrix Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            #region DoubleMatrix

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string propertyName = reader.GetString();
            if (propertyName != "Matrix")
            {
                throw new JsonException();
            }

            var matrix =
                JsonSerializer.Deserialize<DoubleMatrix>(
                    ref reader,
                    options);

            #endregion

            var readOnlyMatrix = new ReadOnlyDoubleMatrix(matrix);

            reader.Read();
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return readOnlyMatrix;
            }

            throw new JsonException();
        }

        ///<inheritdoc/>
        public override void Write(
            Utf8JsonWriter writer,
            ReadOnlyDoubleMatrix value,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("Matrix");
            JsonSerializer.Serialize<DoubleMatrix>(
                writer,
                value.matrix,
                options);

            writer.WriteEndObject();
        }
    }
}
