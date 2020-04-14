// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license.  
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Novacta.Analytics.Infrastructure
{
    /// <summary>
    /// Converts a <see cref="DenseMatrixImplementor{T}"/> (where
    /// T is <see cref="Double"/>) or value to or from JSON.
    /// </summary>
    class JsonDoubleMatrixImplementorConverter :
        JsonConverter<MatrixImplementor<double>>
    {
        ///<inheritdoc/>
        public override bool CanConvert(Type typeToConvert) =>
            typeof(MatrixImplementor<double>).IsAssignableFrom(typeToConvert);

        ///<inheritdoc/>
        public override MatrixImplementor<double> Read(
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

            MatrixImplementor<double> matrixImplementor;

            switch (storageScheme)
            {
                case StorageScheme.Dense:
                    {
                        matrixImplementor =
                            new DenseDoubleMatrixImplementor(
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
                            if (reader.TokenType != JsonTokenType.Number)
                            {
                                throw new JsonException();
                            }

                            matrixImplementor[i] = reader.GetDouble();
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
                            new SparseCsr3DoubleMatrixImplementor(
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
                            if (reader.TokenType != JsonTokenType.Number)
                            {
                                throw new JsonException();
                            }

                            sparseMatrixImplementor.values[i] = reader.GetDouble();
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
            MatrixImplementor<double> value,
            JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            if (value is DenseDoubleMatrixImplementor denseDoubleMatrixImplementor)
            {
                writer.WriteNumber("StorageScheme", (int)StorageScheme.Dense);
                writer.WriteNumber("NumberOfRows", denseDoubleMatrixImplementor.NumberOfRows);
                writer.WriteNumber("NumberOfColumns", denseDoubleMatrixImplementor.NumberOfColumns);

                writer.WriteStartArray("Storage");
                for (int i = 0; i < denseDoubleMatrixImplementor.storage.Length; i++)
                {
                    writer.WriteNumberValue(denseDoubleMatrixImplementor.storage[i]);

                }
                writer.WriteEndArray();
            }
            else if (value is SparseCsr3DoubleMatrixImplementor sparseDoubleMatrixImplementor)
            {
                writer.WriteNumber("StorageScheme", (int)StorageScheme.CompressedRow);
                writer.WriteNumber("NumberOfRows", sparseDoubleMatrixImplementor.NumberOfRows);
                writer.WriteNumber("NumberOfColumns", sparseDoubleMatrixImplementor.NumberOfColumns);

                writer.WriteNumber("Capacity", (int)sparseDoubleMatrixImplementor.capacity);

                writer.WriteStartArray("Values");
                for (int i = 0; i < sparseDoubleMatrixImplementor.values.Length; i++)
                {
                    writer.WriteNumberValue(sparseDoubleMatrixImplementor.values[i]);

                }
                writer.WriteEndArray();

                writer.WriteStartArray("Columns");
                for (int i = 0; i < sparseDoubleMatrixImplementor.columns.Length; i++)
                {
                    writer.WriteNumberValue(sparseDoubleMatrixImplementor.columns[i]);

                }
                writer.WriteEndArray();

                writer.WriteStartArray("RowIndex");
                for (int i = 0; i < sparseDoubleMatrixImplementor.rowIndex.Length; i++)
                {
                    writer.WriteNumberValue(sparseDoubleMatrixImplementor.rowIndex[i]);

                }
                writer.WriteEndArray();
            }
            else if (value is ViewDoubleMatrixImplementor viewDoubleMatrixImplementor)
            {
                writer.WriteNumber("StorageScheme", (int)StorageScheme.Dense);
                writer.WriteNumber("NumberOfRows", viewDoubleMatrixImplementor.NumberOfRows);
                writer.WriteNumber("NumberOfColumns", viewDoubleMatrixImplementor.NumberOfColumns);

                writer.WriteStartArray("Storage");
                for (int i = 0; i < viewDoubleMatrixImplementor.Count; i++)
                {
                    writer.WriteNumberValue(viewDoubleMatrixImplementor[i]);

                }
                writer.WriteEndArray();
            }

            writer.WriteEndObject();
        }
    }
}
