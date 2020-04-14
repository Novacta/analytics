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
    /// Converts a <see cref="CategoricalEntailment"/> or value to or from JSON.
    /// </summary>
    class JsonCategoricalEntailmentConverter : JsonConverter<CategoricalEntailment>
    {
        ///<inheritdoc/>
        public override CategoricalEntailment Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            #region FeatureVariables

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            var propertyName = reader.GetString();
            if (propertyName != "FeatureVariables")
            {
                throw new JsonException();
            }

            var featureVariables =
                JsonSerializer.Deserialize<IReadOnlyList<CategoricalVariable>>(
                    ref reader,
                    options);

            #endregion

            #region ResponseVariable

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            propertyName = reader.GetString();
            if (propertyName != "ResponseVariable")
            {
                throw new JsonException();
            }

            var responseVariable =
                JsonSerializer.Deserialize<CategoricalVariable>(
                    ref reader,
                    options);

            #endregion

            #region FeaturePremises

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            propertyName = reader.GetString();
            if (propertyName != "FeaturePremises")
            {
                throw new JsonException();
            }

            var featurePremises =
                JsonSerializer.Deserialize<IReadOnlyList<SortedSet<double>>>(
                    ref reader,
                    options);

            #endregion

            #region ResponseConclusion

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            propertyName = reader.GetString();
            if (propertyName != "ResponseConclusion")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            var responseConclusion = reader.GetDouble();

            #endregion

            #region TruthValue

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            propertyName = reader.GetString();
            if (propertyName != "TruthValue")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            var truthValue = reader.GetDouble();

            #endregion

            reader.Read();
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return new CategoricalEntailment(
                    featureVariables,
                    responseVariable,
                    new List<SortedSet<double>>(featurePremises),
                    responseConclusion,
                    truthValue);
            }

            throw new JsonException();
        }

        ///<inheritdoc/>
        public override void Write(
            Utf8JsonWriter writer,
            CategoricalEntailment value,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WritePropertyName("FeatureVariables");
            JsonSerializer.Serialize<IReadOnlyList<CategoricalVariable>>(
                writer,
                value.FeatureVariables,
                options);

            writer.WritePropertyName("ResponseVariable");
            JsonSerializer.Serialize<CategoricalVariable>(
                writer,
                value.ResponseVariable,
                options);

            writer.WritePropertyName("FeaturePremises");
            JsonSerializer.Serialize<IReadOnlyList<SortedSet<double>>>(
                writer,
                value.FeaturePremises,
                options);

            writer.WriteNumber("ResponseConclusion", value.ResponseConclusion);

            writer.WriteNumber("TruthValue", value.TruthValue);

            writer.WriteEndObject();
        }
    }
}
