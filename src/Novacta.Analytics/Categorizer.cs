// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics
{
    /// <summary>
    /// Encapsulates a method that assigns a category label to a numerical value
    /// having the specified string representation in a
    /// specific culture.
    /// </summary>
    /// <param name="token">
    /// The string representing a value to be categorized.</param>
    /// <param name="provider">
    /// An object that provides formatting information to
    /// parse numeric values.</param>
    /// <returns>The category label assigned to the value.</returns>
    /// <remarks>
    /// <para>
    /// Objects of type <see cref="Categorizer" /> can be used to define how
    /// a <see cref="CategoricalDataSet" /> should be encoded when
    /// reading numerical data from a stream of characters.
    /// </para>
    /// </remarks>
    /// <seealso cref="CategoricalDataSet.CategorizeByEntropyMinimization(string, char, IndexCollection, bool, int, IFormatProvider)" />
    /// <seealso cref="CategoricalDataSet.Encode(string, char, IndexCollection, bool, System.Collections.Generic.Dictionary{int, Categorizer}, IFormatProvider)" />
    public delegate string Categorizer(string token, IFormatProvider provider);
}
