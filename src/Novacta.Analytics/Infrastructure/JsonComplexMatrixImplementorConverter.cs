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
    /// Converts a <see cref="DenseMatrixImplementor{T}"/> (where
    /// T is <see cref="Complex"/>) or value to or from JSON.
    /// </summary>
    class JsonComplexMatrixImplementorConverter :
        JsonConverter<MatrixImplementor<Complex>>
    {
        ///<inheritdoc/>
        public override bool CanConvert(Type typeToConvert) =>
            typeof(MatrixImplementor<Complex>).IsAssignableFrom(typeToConvert);

        ///<inheritdoc/>
        public override MatrixImplementor<Complex> Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            #region StorageScheme

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            string propertyName = reader.GetString();
            if (propertyName != "StorageScheme")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            StorageScheme storageScheme = (StorageScheme)reader.GetInt32();

            #endregion

            #region NumberOfRows

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            propertyName = reader.GetString();
            if (propertyName != "NumberOfRows")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            int numberOfRows = reader.GetInt32();

            #endregion

            #region NumberOfColumns

            reader.Read();
            if (reader.TokenType != JsonTokenType.PropertyName)
            {
                throw new JsonException();
            }

            propertyName = reader.GetString();
            if (propertyName != "NumberOfColumns")
            {
                throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
            {
                throw new JsonException();
            }

            int numberOfColumns = reader.GetInt32();

            #endregion

            MatrixImplementor<Complex> matrixImplementor;

            switch (storageScheme)
            {
                case StorageScheme.Dense:
                    {
                        matrixImplementor =
                            new DenseComplexMatrixImplementor(
                                numberOfRows,
                                numberOfColumns);

                        #region Storage

                        reader.Read();
                        if (reader.TokenType != JsonTokenType.PropertyName)
                        {
                            throw new JsonException();
                        }

                        propertyName = reader.GetString();
                        if (propertyName != "Storage")
                        {
                            throw new JsonException();
                        }

                        reader.Read();
                        if (reader.TokenType != JsonTokenType.StartArray)
                        {
                            throw new JsonException();
                        }

                        for (int i = 0; i < matrixImplementor.Count; i++)
                        {
                            reader.Read();

                            matrixImplementor[i] = 
                                JsonSerializer.Deserialize<Complex>(
                                    ref reader,
                                    options);
                        }

                        reader.Read();
                        if (reader.TokenType != JsonTokenType.EndArray)
                        {
                            throw new JsonException();
                        }

                        #endregion
                    }
                    break;
                case StorageScheme.CompressedRow:
                    {
                        #region Capacity

                        reader.Read();
                        if (reader.TokenType != JsonTokenType.PropertyName)
                        {
                            throw new JsonException();
                        }

                        propertyName = reader.GetString();
                        if (propertyName != "Capacity")
                        {
                            throw new JsonException();
                        }

                        reader.Read();
                        if (reader.TokenType != JsonTokenType.Number)
                        {
                            throw new JsonException();
                        }

                        int capacity = reader.GetInt32();

                        #endregion

                        var sparseMatrixImplementor =
                            new SparseCsr3ComplexMatrixImplementor(
                                numberOfRows,
                                numberOfColumns,
                                capacity);

                        #region Values

                        reader.Read();
                        if (reader.TokenType != JsonTokenType.PropertyName)
                        {
                            throw new JsonException();
                        }

                        propertyName = reader.GetString();
                        if (propertyName != "Values")
                        {
                            throw new JsonException();
                        }

                        reader.Read();
                        if (reader.TokenType != JsonTokenType.StartArray)
                        {
                            throw new JsonException();
                        }

                        for (int i = 0; i < capacity; i++)
                        {
                            reader.Read();

                            sparseMatrixImplementor.values[i] =
                                JsonSerializer.Deserialize<Complex>(
                                    ref reader,
                                    options);
                        }

                        reader.Read();
                        if (reader.TokenType != JsonTokenType.EndArray)
                        {
                            throw new JsonException();
                        }

                        #endregion

                        #region Columns

                        reader.Read();
                        if (reader.TokenType != JsonTokenType.PropertyName)
                        {
                            throw new JsonException();
                        }

                        propertyName = reader.GetString();
                        if (propertyName != "Columns")
                        {
                            throw new JsonException();
                        }

                        reader.Read();
                        if (reader.TokenType != JsonTokenType.StartArray)
                        {
                            throw new JsonException();
                        }

                        for (int i = 0; i < capacity; i++)
                        {
                            reader.Read();
                            if (reader.TokenType != JsonTokenType.Number)
                            {
                                throw new JsonException();
                            }

                            sparseMatrixImplementor.columns[i] = reader.GetInt32();
                        }

                        reader.Read();
                        if (reader.TokenType != JsonTokenType.EndArray)
                        {
                            throw new JsonException();
                        }

                        #endregion

                        #region RowIndex

                        reader.Read();
                        if (reader.TokenType != JsonTokenType.PropertyName)
                        {
                            throw new JsonException();
                        }

                        propertyName = reader.GetString();
                        if (propertyName != "RowIndex")
                        {
                            throw new JsonException();
                        }

                        reader.Read();
                        if (reader.TokenType != JsonTokenType.StartArray)
                        {
                            throw new JsonException();
                        }

                        for (int i = 0; i < numberOfRows + 1; i++)
                        {
                            reader.Read();
                            if (reader.TokenType != JsonTokenType.Number)
                            {
                                throw new JsonException();
                            }

                            sparseMatrixImplementor.rowIndex[i] = reader.GetInt32();
                        }

                        reader.Read();
                        if (reader.TokenType != JsonTokenType.EndArray)
                        {
                            throw new JsonException();
                        }

                        #endregion

                        matrixImplementor = sparseMatrixImplementor;
                    }
                    break;
                default:
                    throw new JsonException();
            }

            reader.Read();
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return matrixImplementor;
            }

            throw new JsonException();
        }

        ///<inheritdoc/>
        public override void Write(
            Utf8JsonWriter writer,
            MatrixImplementor<Complex> value,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            if (value is DenseComplexMatrixImplementor denseComplexMatrixImplementor)
            {
                writer.WriteNumber("StorageScheme", (int)StorageScheme.Dense);
                writer.WriteNumber("NumberOfRows", denseComplexMatrixImplementor.NumberOfRows);
                writer.WriteNumber("NumberOfColumns", denseComplexMatrixImplementor.NumberOfColumns);

                writer.WriteStartArray("Storage");
                for (int i = 0; i < denseComplexMatrixImplementor.storage.Length; i++)
                {
                    JsonSerializer.Serialize<Complex>(
                        writer,
                        denseComplexMatrixImplementor.storage[i],
                        options);
                }
                writer.WriteEndArray();
            }
            else if (value is SparseCsr3ComplexMatrixImplementor sparseComplexMatrixImplementor)
            {
                writer.WriteNumber("StorageScheme", (int)StorageScheme.CompressedRow);
                writer.WriteNumber("NumberOfRows", sparseComplexMatrixImplementor.NumberOfRows);
                writer.WriteNumber("NumberOfColumns", sparseComplexMatrixImplementor.NumberOfColumns);

                writer.WriteNumber("Capacity", (int)sparseComplexMatrixImplementor.capacity);

                writer.WriteStartArray("Values");
                for (int i = 0; i < sparseComplexMatrixImplementor.values.Length; i++)
                {
                    JsonSerializer.Serialize<Complex>(
                        writer,
                        sparseComplexMatrixImplementor.values[i],
                        options);
                }
                writer.WriteEndArray();

                writer.WriteStartArray("Columns");
                for (int i = 0; i < sparseComplexMatrixImplementor.columns.Length; i++)
                {
                    writer.WriteNumberValue(sparseComplexMatrixImplementor.columns[i]);

                }
                writer.WriteEndArray();

                writer.WriteStartArray("RowIndex");
                for (int i = 0; i < sparseComplexMatrixImplementor.rowIndex.Length; i++)
                {
                    writer.WriteNumberValue(sparseComplexMatrixImplementor.rowIndex[i]);

                }
                writer.WriteEndArray();
            }

            writer.WriteEndObject();
        }
    }
}
