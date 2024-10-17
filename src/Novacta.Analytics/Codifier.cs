// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics
{
    /// <summary>
    /// Encapsulates a method that assigns a numerical code to a label
    /// having the specified string representation in a
    /// given culture.
    /// </summary>
    /// <param name="token">
    /// The string representing a label to which a code must be assigned.</param>
    /// <param name="provider">
    /// An object that provides formatting information to
    /// parse labels.
    /// </param>
    /// <returns>The code assigned to the label.</returns>
    /// <remarks>
    /// <para>
    /// Objects of type <see cref="Codifier" /> can be used to define how
    /// data from a stream of characters should be encoded into a <see cref="DoubleMatrix" /> instance.
    /// </para>
    /// </remarks>
    /// <seealso cref="DoubleMatrix.Encode(string, char, IndexCollection, bool, System.Collections.Generic.Dictionary{int, Codifier}, IFormatProvider)" />
    public delegate double Codifier(string token, IFormatProvider provider);
}
