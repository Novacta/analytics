// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license.  
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Infrastructure;
using System;
using System.Text.Json;

namespace Novacta.Analytics
{
    /// <summary>
    /// Provides methods to support the JSON serialization of types
    /// defined in the <see cref="Analytics"/> namespace.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This class provides custom converters for the JSON serialization classes that 
    /// are defined in the <see cref="System.Text.Json"/> namespace. 
    /// </para>
    /// <para>
    /// In particular, method <see cref="AddDataConverters(JsonSerializerOptions)"/> 
    /// adds to a given <see cref="JsonSerializerOptions"/> instance
    /// the converters required for the JSON serialization of the following types: 
    /// <see cref="DoubleMatrix"/>, <see cref="ReadOnlyDoubleMatrix"/>, 
    /// <see cref="ComplexMatrix"/>, <see cref="ReadOnlyComplexMatrix"/>, 
    /// <see cref="Category"/>,
    /// <see cref="CategoricalVariable"/>, <see cref="CategoricalEntailment"/>, 
    /// and <see cref="CategoricalDataSet"/>.
    /// </para>
    /// </remarks>
    /// <example>
    /// <para>
    /// In the following example, a matrix instance is serialized by writing to
    /// a JSON value. Hence the matrix is deserialized by reading from
    /// such value.
    /// </para>
    /// <para>
    /// <code title="Conversion of a matrix to and from a JSON value."
    /// source="..\Novacta.Analytics.CodeExamples\JsonSerializationExample0.cs.txt" 
    /// language="cs" />
    /// </para> 
    /// <para>
    /// In the following example, a categorical data set is converted to
    /// a JSON value. Hence the data set is instantiated by reading from
    /// such value.
    /// </para>
    /// <para>
    /// <code title="Conversion of categorical data to and from a JSON value."
    /// source="..\Novacta.Analytics.CodeExamples\JsonSerializationExample1.cs.txt" 
    /// language="cs" />
    /// </para> 
    /// </example>
    public static class JsonSerialization
    {
        /// <summary>
        /// Adds support to the JSON conversion of data types in the
        /// <see cref="Analytics"/> namespace.
        /// </summary>
        /// <param name="options">
        /// Options to control the conversion behavior.
        /// </param>
        /// <remarks>
        /// <para>
        /// This method provides support for the JSON conversion of types
        /// <see cref="DoubleMatrix"/>, <see cref="ReadOnlyDoubleMatrix"/>, 
        /// <see cref="Category"/>,
        /// <see cref="CategoricalVariable"/>, <see cref="CategoricalEntailment"/>, 
        /// and <see cref="CategoricalDataSet"/>.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="options"/> is <b>null</b>.
        /// </exception>
        public static void AddDataConverters(JsonSerializerOptions options)
        {
            if (options is null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            options.Converters.Add(new JsonDictionaryInt32StringConverter());

            options.Converters.Add(new JsonDoubleMatrixImplementorConverter());
            options.Converters.Add(new JsonReadOnlyDoubleMatrixConverter());
            options.Converters.Add(new JsonDoubleMatrixConverter());

            options.Converters.Add(new JsonComplexConverter());
            options.Converters.Add(new JsonComplexMatrixImplementorConverter());
            options.Converters.Add(new JsonReadOnlyComplexMatrixConverter());
            options.Converters.Add(new JsonComplexMatrixConverter());
            options.Converters.Add(new JsonCategoryConverter());
            options.Converters.Add(new JsonCategoricalVariableConverter());
            options.Converters.Add(new JsonCategoricalEntailmentConverter());
            options.Converters.Add(new JsonCategoricalDataSetConverter());
        }
    }
}
