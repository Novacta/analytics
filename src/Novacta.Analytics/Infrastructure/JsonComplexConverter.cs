// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license.  
// See the LICENSE file in the project root for more information.

using System;
using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Novacta.Analytics.Infrastructure
{
    /// <summary>
    /// Converts a <see cref="Category"/> or value to or from JSON.
    /// </summary>
    class JsonComplexConverter : JsonConverter<Complex>
    {
        ///<inheritdoc/>
        public override Complex Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            #region Real

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            var propertyName = reader.GetString();
            if (propertyName != "Real")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            var real = reader.GetDouble();

            #endregion

            #region Imaginary

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            propertyName = reader.GetString();
            if (propertyName != "Imag")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            var imag = reader.GetDouble();

            #endregion

            reader.Read();
            if (reader.TokenType != JsonTokenType.EndObject)
            {
                throw new JsonException();
            }

            return new Complex(real, imag);
        }

        ///<inheritdoc/>
        public override void Write(
            Utf8JsonWriter writer,
            Complex value,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteNumber("Real", value.Real);

            writer.WriteNumber("Imag", value.Imaginary);

            writer.WriteEndObject();
        }
    }
}
