// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics
{
    /// <summary>
    /// Defines an index/value pair.
    /// </summary>
    /// <remarks>
    /// <para>
    /// An <see cref="IndexValuePair"/> structure groups a zero based index 
    /// to a value. This is useful if the position of a particular value inside a 
    /// given matrix has to be returned. 
    /// </para>
    /// <para>
    /// For example, method <see cref="Stat.Max(DoubleMatrix)"/> 
    /// uses an <see cref="IndexValuePair"/> to return the maximum value  
    /// and its (say, first) linear position in the specified data matrix,
    /// where matrix entries are interpreted as linearly ordered following a 
    /// column major ordering.
    /// </para>
    /// </remarks>
    /// <seealso cref="Stat.Min(DoubleMatrix)"/>
    /// <seealso href="http://en.wikipedia.org/wiki/Row-major_order#Column-major_order"/>
    public struct IndexValuePair : IEquatable<IndexValuePair>
    {
        internal int index;
        internal double value;

        /// <summary>
        /// Gets the index in the index/value pair.
        /// </summary>
        /// <remarks>
        /// This property is read/only.
        /// </remarks>
        /// <value>The index of the <see cref="IndexValuePair"/>.</value>
        public int Index { get { return this.index; } }

        /// <summary>
        /// Gets the value in the index/value pair.
        /// </summary>
        /// <remarks>
        /// This property is read/only.
        /// </remarks>
        /// <value>The value of the <see cref="IndexValuePair"/>.</value>
        public double Value { get { return this.value; } }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj is not IndexValuePair)
                return false;

            return Equals((IndexValuePair)obj);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns><c>true</c> if the current object is equal to the <paramref name="other" /> 
        /// parameter; otherwise, <c>false</c>.</returns>
        public bool Equals(IndexValuePair other)
        {
            if (this.index != other.index)
                return false;

            return this.value == other.value;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return this.value.GetHashCode() ^ this.index;
        }

        /// <summary>
        /// Returns a value that indicates whether 
        /// an <see cref="IndexValuePair"/> instance is 
        /// equal to another <see cref="IndexValuePair"/> instance.
        /// </summary>
        /// <param name="left">The first instance to compare.</param>
        /// <param name="right">The second instance to compare.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is equal to <paramref name="right"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator ==(IndexValuePair left, IndexValuePair right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Returns a value that indicates whether 
        /// an <see cref="IndexValuePair"/> instance is not
        /// equal to another <see cref="IndexValuePair"/> instance.
        /// </summary>
        /// <param name="left">The first instance to compare.</param>
        /// <param name="right">The second instance to compare.</param>
        /// <returns><c>true</c> if <paramref name="left"/> is not equal to <paramref name="right"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool operator !=(IndexValuePair left, IndexValuePair right)
        {
            return !(left == right);
        }
    }
}
