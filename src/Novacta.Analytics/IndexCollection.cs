// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;

using Novacta.Analytics.Infrastructure;

namespace Novacta.Analytics
{
    /// <summary>
    /// Represents a collection of integers for zero-based matrix indexing.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <b>Instantiation</b>
    /// </para>
    /// <para>
    /// <see cref="IndexCollection"/> instances can be created using several 
    /// methods. Indexes ranging in a given interval are returned by 
    /// <see cref="Range"/>. A collection of evenly 
    /// spaced indexes can be initialized by 
    /// <see cref="Sequence"/>. 
    /// A collection ranging from zero to a specified last index
    /// is instead returned
    /// by <see cref="Default"/>.
    /// A collection can also be defined to contain elements copied from a 
    /// specified array by calling <see cref="FromArray(int[])"/>,
    /// or, eventually to prevent copying operations, by calling
    /// <see cref="FromArray(int[],bool)"/>.
    /// </para>              
    /// <para>
    /// <b>Matrix Indexing</b>
    /// </para>
    /// <para>                  
    /// The <see cref="DoubleMatrix"/> class
    /// defines zero-based indexers to
    /// get or set individual matrix entries, or entire sub matrices.
    /// Usually, a given datum is accessed by specifying a pair of row and 
    /// column indexes, but 
    /// indexers are overloaded to accept also a linear index, i.e. an index to 
    /// which an matrix element
    /// corresponds provided that matrix entries have been interpreted as well 
    /// ordered following a 
    /// column major ordering.
    /// All such indexers 
    /// accept <see cref="IndexCollection"/> instances as arguments, enabling the 
    /// simultaneous specification of multiple linear, row, or column indexes.    
    /// </para>
    /// <para><b>Comparison</b></para>
    /// <para id='quasi.lexicographic.order'>
    /// <see cref="IndexCollection"/> instances are quasi-lexicographically ordered. This means
    /// that instances are firstly ordered by their <see cref="Count"/>, and then, within
    /// collections having the same length, by lexicographic order.
    /// </para>
    /// <para id='quasi.lexicographic.equality'>
    /// This also means that when tested for equality, <see cref="IndexCollection"/> instances 
    /// are considered equal if and only if they have the same <see cref="Count"/> and, 
    /// for each index position, indexes corresponding to such position are equal, too.
    /// </para>
    /// </remarks>
    /// <seealso cref="DoubleMatrix">
    /// DoubleMatrix Class</seealso>
    /// <seealso href="http://en.wikipedia.org/wiki/Row-major_order#Column-major_order"/>
    [Serializable]
    public class IndexCollection :
        IList<int>,
        IReadOnlyList<int>,
        IEquatable<IndexCollection>,
        IComparable<IndexCollection>,
        IComparable,
        ISerializable
    {
        #region ISerializable

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="IndexCollection"/> class with serialized data.
        /// </summary>
        /// <param name="info">
        /// The object that holds the serialized object data.
        /// </param>
        /// <param name="context">
        /// The contextual information about the source or destination.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="info"/> is <b>null</b>.
        /// </exception>
        [SecurityPermission(
            SecurityAction.Demand,
            SerializationFormatter = true),
         System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Naming",
            "CA1801:ReviewUnusedParameters",
            Justification = "Constructor requires parameter context.")]
        protected IndexCollection(
            SerializationInfo info,
            StreamingContext context)
        {
            if (null == info)
                throw new ArgumentNullException(nameof(info));

            this.indexes =
                (int[])info.GetValue(
                    "indexes",
                    typeof(int[]));

            this.maxIndex =
                (int)info.GetValue(
                    "maxIndex",
                    typeof(int));
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="info"/> is <b>null</b>.
        /// </exception>
        [SecurityCritical]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (null == info)
                throw new ArgumentNullException(nameof(info));

            info.AddValue(
                "indexes",
                this.indexes,
                typeof(int[]));

            info.AddValue(
                "maxIndex",
                this.maxIndex,
                typeof(int));
        }

        #endregion

        #region State

        internal int[] indexes;
        internal int maxIndex;

        /// <summary>
        /// Gets the indexes of the <see cref="IndexCollection"/>.
        /// </summary>
        /// <remarks>
        /// The getter returns the internal array representation of the indexes. 
        /// <para>
        /// <note>
        /// This operation must be considered
        /// as read-only: no changes to the indexes should be executed using the returned array,
        /// otherwise the <see cref="Max"/> property becomes
        /// unreliable.
        /// </note>
        /// </para>
        /// </remarks>
        /// <value>The array of indexes internal to the <see cref="IndexCollection"/>.</value>
        internal int[] Indexes
        {
            get { return this.indexes; }
        }

        /// <summary>
        /// Gets the maximum index in the <see cref="IndexCollection"/>.
        /// </summary>
        /// <value>The maximum index in the collection.</value>
        public int Max { get { return this.maxIndex; } }

        /// <summary>
        /// Updates the cached collection maximum index.
        /// </summary>
        /// <remarks>Updates field <c>this.m_maxIndex</c>, so that it
        /// stores the maximum index included in the collection.</remarks>
        private void UpdateCollectionMaxIndex()
        {
            int[] internalIndexes = this.indexes;
            int maxIndex = internalIndexes[0];
            int currentIndex;
            int numberOfIndexes = internalIndexes.Length;

            for (int i = 1; i < numberOfIndexes; i++) {
                currentIndex = internalIndexes[i];

                if (maxIndex < currentIndex)
                    maxIndex = currentIndex;
            }

            this.maxIndex = maxIndex;
        }

        /// <summary>
        /// Sorts the <see cref="IndexCollection"/> in ascending order.
        /// </summary>
        public void Sort()
        {
            Array.Sort(this.indexes);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="System.String" /> that represents this instance.</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            int numberOfIndexes = this.indexes.Length;

            if (numberOfIndexes == 1) {
                return this.maxIndex.ToString(
                    CultureInfo.InvariantCulture);
            }

            for (int Id = 0; Id < numberOfIndexes - 1; Id++) {
                stringBuilder.Append(this.indexes[Id]);
                stringBuilder.Append(", ");
            }

            stringBuilder.Append(this.indexes[numberOfIndexes - 1]);

            return stringBuilder.ToString();
        }

        #endregion

        #region Constructors and factory methods

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexCollection" /> class
        /// that contains elements from the specified array, eventually copied.
        /// </summary>
        /// <param name="indexes">The indexes to store.</param>
        /// <param name="copyIndexes"><b>true</b> if <paramref name="indexes"/> has to be copied;
        /// otherwise <b>false</b>.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="indexes" /> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The number of elements of <paramref name="indexes" /> is zero. <br/>
        /// -or- <br/>
        /// <paramref name="indexes" /> contains a negative element.
        /// </exception>
        internal IndexCollection(int[] indexes, bool copyIndexes)
        {
            if (indexes is null)
                throw new ArgumentNullException(nameof(indexes));

            if (indexes.Length == 0)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_NON_EMPTY"),
                    nameof(indexes));
            }

            this.maxIndex = -1;

            if (copyIndexes) {
                var destinationArray = new int[indexes.Length];
                for (int i = 0; i < indexes.Length; i++) {
                    int index = indexes[i];
                    if (index < 0) {
                        throw new ArgumentException(
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_ENTRIES_MUST_BE_NON_NEGATIVE"),
                            nameof(indexes));
                    }
                    if (index > this.maxIndex)
                    {
                        this.maxIndex = index;
                    }
                    destinationArray[i] = index;
                }
                this.indexes = destinationArray;
            }
            else {
                for (int i = 0; i < indexes.Length; i++)
                {
                    int index = indexes[i];
                    if (index < 0)
                    {
                        throw new ArgumentException(
                            ImplementationServices.GetResourceString(
                                "STR_EXCEPT_PAR_ENTRIES_MUST_BE_NON_NEGATIVE"),
                            nameof(indexes));
                    }
                    if (index > this.maxIndex)
                    {
                        this.maxIndex = index;
                    }
                }
                this.indexes = indexes;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexCollection" /> class
        /// that contains elements copied from the specified array.
        /// </summary>
        /// <param name="indexes">
        /// The array that stores the elements 
        /// of the new <see cref="IndexCollection" /> instance.
        /// </param>
        /// <returns>
        /// A collection containing the indexes in the specified array.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="indexes" /> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <see cref="Array.Length"/> of <paramref name="indexes" /> is zero. <br/>
        /// -or- <br/>
        /// <paramref name="indexes" /> contains a negative element.
        /// </exception>
        public static IndexCollection FromArray(int[] indexes)
        {
            return new IndexCollection(indexes, true);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexCollection" /> class
        /// whose elements are stored in the specified array.
        /// </summary>
        /// <param name="indexes">
        /// The array that stores the elements 
        /// of the new <see cref="IndexCollection" /> instance.
        /// </param>
        /// <param name="copyIndexes">
        /// <b>true</b> if <paramref name="indexes"/> 
        /// must be copied before instantiation; otherwise <b>false</b>.
        /// </param>
        /// <returns>
        /// A collection using the specified array to store its indexes.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The <see cref="FromArray(int[],bool)"/> method prevents 
        /// the copy of the elements in <paramref name="indexes"/> before 
        /// instantiation if <paramref name="copyIndexes"/> evaluates 
        /// to <b>false</b>: the 
        /// returned <see cref="IndexCollection"/> instance will use a direct 
        /// reference to <paramref name="indexes"/> to manipulate its indexes.
        /// </para>
        /// <para>
        /// <note type="caution">
        /// This method is intended for advanced users and must always be used 
        /// carefully. 
        /// If the value of <paramref name="copyIndexes"/> is <b>false</b>, do not 
        /// use this method if you do not have complete control of 
        /// the <paramref name="indexes"/> array.
        /// Once the array is passed to the method as an argument, it must be 
        /// treated as a read-only object outside the returned instance: 
        /// you can set elements via the <see cref="IndexCollection"/> API, but
        /// you shouldn't manipulate its entries via 
        /// a direct reference to <paramref name="indexes"/>: otherwise, the 
        /// behavior of the returned <see cref="IndexCollection"/> instance 
        /// must be considered as undefined and  
        /// almost surely prone to errors.
        /// </note>
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="indexes" /> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The <see cref="Array.Length"/> of <paramref name="indexes" /> is zero. <br/>
        /// -or- <br/>
        /// <paramref name="indexes" /> contains a negative element.
        /// </exception>
        public static IndexCollection FromArray(int[] indexes, bool copyIndexes)
        {
            return new IndexCollection(indexes, copyIndexes);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexCollection"/> class
        /// that contains elements in the specified closed range.
        /// </summary>
        /// <param name="firstIndex">The first index of the range.</param>
        /// <param name="lastIndex">The last index of the range.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="firstIndex"/> is negative.  <br/>
        /// -or- <br/>
        /// <paramref name="lastIndex"/> is less than <paramref name="firstIndex"/>.
        /// </exception>
        private IndexCollection(int firstIndex, int lastIndex)
        {
            int numOfIndexes = (lastIndex + 1) - firstIndex;
            this.indexes = new int[numOfIndexes];
            for (int i = 0; i < numOfIndexes; i++) {
                this.indexes[i] = firstIndex + i;
            }

            this.maxIndex = lastIndex;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexCollection"/> class
        /// that contains a sequence of evenly spaced elements.
        /// </summary>
        /// <param name="firstIndex">The first index of the sequence.</param>
        /// <param name="increment">The amount which increments the elements in the sequence.</param>
        /// <param name="indexBound">The value by which the sequence is bounded.</param>
        /// <remarks>
        /// <para>
        /// The <paramref name="firstIndex"/> is always added 
        /// to the collection.
        /// Additional
        /// indexes are included by repeatedly adding <paramref name="increment"/> 
        /// to <paramref name="firstIndex"/>. 
        /// </para>
        /// <para>
        /// How the sequence stops growing depends upon the sign of <paramref name="increment"/>.
        /// If <paramref name="increment"/> is positive, then
        /// the sequence stops if an incremented value is greater than <paramref name="indexBound"/>.
        /// If <paramref name="increment"/> is negative, additional
        /// indexes are included in the sequence if they are less than or equal to <paramref name="indexBound"/>.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="firstIndex"/> is negative. <br/>
        /// -or- <br/> 
        /// <paramref name="indexBound"/> is negative. <br/>
        /// -or- <br/>
        /// <paramref name="increment"/> is zero. <br/>
        /// -or- <br/>
        /// <paramref name="increment"/> is positive and <paramref name="indexBound"/> is less than 
        /// <paramref name="firstIndex"/>. <br/>
        /// -or- <br/>
        /// <paramref name="increment"/> is negative and <paramref name="firstIndex"/> is less than 
        /// <paramref name="indexBound"/>.
        /// </exception>
        private IndexCollection(int firstIndex, int increment, int indexBound)
        {
            int i, quotient, numberOfIndexes, nextStep;

            #region Negative step

            if (increment < 0) {
                int firstMinusLastIndex = firstIndex - indexBound;
                quotient = Convert.ToInt32(Math.Floor(Convert.ToDouble(firstMinusLastIndex / increment)));

                numberOfIndexes = Math.Abs(quotient) + 1;
                this.indexes = new int[numberOfIndexes];
                nextStep = 0;
                i = 0;
                while (i < numberOfIndexes) {
                    this.indexes[i] = firstIndex + nextStep;
                    i++;
                    nextStep += increment;
                }

                this.maxIndex = firstIndex;
                return;
            }

            #endregion

            int indexesDiff = indexBound - firstIndex;
            quotient = Convert.ToInt32(Math.Floor(Convert.ToDouble(indexesDiff / increment)));

            numberOfIndexes = quotient + 1;
            this.indexes = new int[numberOfIndexes];
            nextStep = 0;
            i = 0;
            while (i < numberOfIndexes) {
                this.indexes[i] = firstIndex + nextStep;
                i++;
                nextStep += increment;
            }

            this.maxIndex = this.indexes[numberOfIndexes - 1];
        }

        /// <summary>
        /// Creates an <see cref="IndexCollection"/> which ranges from zero to 
        /// the specified last index.
        /// </summary>
        /// <param name="lastIndex">The last index of the range.</param>
        /// <returns>A collection of the specified number of indexes,
        /// all equal to zero.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="lastIndex"/> is negative.
        /// </exception>
        public static IndexCollection Default(int lastIndex)
        {
            if (lastIndex < 0) {
                throw new ArgumentOutOfRangeException(nameof(lastIndex),
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE"));
            }

            return new IndexCollection(0, lastIndex);
        }

        /// <summary>
        /// Creates an <see cref="IndexCollection"/> which ranges from the specified 
        /// first index to the specified last index.
        /// </summary>
        /// <param name="firstIndex">The first index of the range.</param>
        /// <param name="lastIndex">The last index of the range.</param>
        /// <returns>The collection of indexes ranging from the first index to 
        /// the last one.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="firstIndex"/> is negative. <br/>
        /// -or- <br/>
        /// <paramref name="lastIndex"/> is less than <paramref name="firstIndex"/>.
        /// </exception>
        public static IndexCollection Range(int firstIndex, int lastIndex)
        {
            if (firstIndex < 0) {
                throw new ArgumentOutOfRangeException(nameof(firstIndex),
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE"));
            }

            if (firstIndex > lastIndex) {
                throw new ArgumentOutOfRangeException(nameof(lastIndex),
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_IND_LAST_LESS_THAN_FIRST"));
            }

            return new IndexCollection(firstIndex, lastIndex);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexCollection"/> class
        /// that contains a sequence of evenly spaced elements.
        /// </summary>
        /// <param name="firstIndex">The first index of the sequence.</param>
        /// <param name="increment">The amount which increments the elements in 
        /// the sequence.</param>
        /// <param name="indexBound">The value by which the sequence is bounded.</param>
        /// <returns>The collection of indexes in the specified sequence.</returns>
        /// <remarks>
        /// <para>
        /// The <paramref name="firstIndex"/> is always added 
        /// to the collection.
        /// Additional
        /// indexes are included by repeatedly adding <paramref name="increment"/> 
        /// to <paramref name="firstIndex"/>. 
        /// </para>
        /// <para>
        /// How the sequence stops growing depends upon the sign of 
        /// <paramref name="increment"/>.
        /// If <paramref name="increment"/> is positive, then
        /// the sequence stops if an incremented value is greater than 
        /// <paramref name="indexBound"/>.
        /// If <paramref name="increment"/> is negative, additional
        /// indexes are included in the sequence if they are less than or 
        /// equal to <paramref name="indexBound"/>.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="firstIndex"/> is negative. <br/>
        /// -or- <br/>
        /// <paramref name="indexBound"/> is negative. <br/>
        /// -or- <br/> 
        /// <paramref name="increment"/> is zero. <br/>
        /// -or- <br/> 
        /// <paramref name="increment"/> is positive and <paramref name="indexBound"/> 
        /// is less than <paramref name="firstIndex"/>. <br/>
        /// -or- <br/>
        /// <paramref name="increment"/> is negative and <paramref name="firstIndex"/> is
        /// less than <paramref name="indexBound"/>.
        /// </exception>
        public static IndexCollection Sequence(int firstIndex, int increment,
            int indexBound)
        {
            if (firstIndex < 0) {
                throw new ArgumentOutOfRangeException(nameof(firstIndex),
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE"));
            }

            if (increment == 0) {
                throw new ArgumentOutOfRangeException(nameof(increment),
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_NONZERO"));
            }

            if (indexBound < 0) {
                throw new ArgumentOutOfRangeException(nameof(indexBound),
                    ImplementationServices.GetResourceString(
                    "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE"));
            }

            if (increment < 0) {
                if (firstIndex < indexBound) {
                    throw new ArgumentOutOfRangeException(nameof(indexBound),
                        ImplementationServices.GetResourceString(
                        "STR_EXCEPT_IND_NEGATIVE_INCREMENT_FIRST_LESS_THAN_LAST"));
                }
            }
            else {
                if (firstIndex > indexBound) {
                    throw new ArgumentOutOfRangeException(nameof(indexBound),
                        ImplementationServices.GetResourceString(
                        "STR_EXCEPT_IND_POSITIVE_INCREMENT_LAST_LESS_THAN_FIRST"));
                }
            }

            return new IndexCollection(firstIndex, increment, indexBound);
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="IndexCollection"/> class 
        /// from being created.
        /// </summary>
        /// <remarks>
        /// Needed for best performance in cloning instances.
        /// </remarks>
        /// <seealso cref="Clone"/>
        private IndexCollection()
        { }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <remarks>
        /// A deep copy of the instance is executed.
        /// </remarks>
        /// <returns>A new object that is a copy of this instance.</returns>
        internal IndexCollection Clone()
        {
            int[] clonedIndexes = new int[this.indexes.Length];
            this.indexes.CopyTo(clonedIndexes, 0);

            var clone = new IndexCollection
            {
                indexes = clonedIndexes,
                maxIndex = this.maxIndex
            };

            return clone;
        }

        #endregion

        #region Additional indexers

        /// <summary>
        /// Gets the indexes in the <see cref="IndexCollection" /> corresponding to
        /// the specified positions.
        /// </summary>
        /// <param name="positions">The positions of the indexes to get.</param>
        /// <value>The collection of indexes at the specified positions.</value>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="positions"/> is <b>null</b>.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="positions"/> contains a value which is negative or greater than or equal to
        /// <see cref="Count"/>.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Microsoft.Design",
            "CA1043:UseIntegralOrStringArgumentForIndexers",
            Justification = "Index collections can be sub referenced by index collections.")]
        public IndexCollection this[IndexCollection positions]
        {
            get
            {
                if (null == positions)
                    throw new ArgumentNullException(nameof(positions));

                int numberOfIndexes = this.indexes.Length;

                if (positions.maxIndex >= numberOfIndexes) {
                    throw new ArgumentOutOfRangeException(nameof(positions),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_IND_POSITION_EXCEEDS_DIMS"));
                }

                int[] subIndexes = new int[positions.Count];

                for (int i = 0; i < positions.Count; i++)
                    subIndexes[i] = this.indexes[positions[i]];

                return new IndexCollection(subIndexes, false);
            }
        }

        #endregion

        #region IEnumerable

        /// <inheritdoc/>
        public IEnumerator<int> GetEnumerator()
        {
            return new IndexCollectionEnumerator(this);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new IndexCollectionEnumerator(this);
        }
        
        #endregion

        #region IEquatable, IComparable

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing 
        /// algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            int hashCode = this.Count.GetHashCode();

            for (int i = 0; i < this.Count; i++)
                hashCode ^= this[i].GetHashCode();

            return hashCode;
        }

        /// <summary>
        /// Compares the current object with another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <remarks>
        /// <inheritdoc cref="IndexCollection" 
        /// path="para[@id='quasi.lexicographic.order']"/>
        /// <inheritdoc cref="IndexCollection" 
        /// path="para[@id='quasi.lexicographic.equality']"/>
        /// </remarks>
        /// <returns>A value that indicates the relative order of the objects 
        /// being compared. The return value has the following meanings: 
        ///      <list type="table">
        ///         <listheader>
        ///            <term>Value</term>
        ///            <term>Meaning</term>
        ///         </listheader>
        ///         <item>
        ///            <term>Less than zero</term>
        ///            <term>This object is less than the <paramref name="other" /> parameter.</term>
        ///         </item>        
        ///         <item>
        ///            <term>Zero</term>
        ///            <term>This object is equal to <paramref name="other" />.</term>
        ///         </item>        
        ///         <item>
        ///            <term>Greater than zero</term>
        ///            <term>This object is greater than <paramref name="other" />.</term>
        ///         </item>        
        ///    </list>
        /// </returns>
        /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
        public int CompareTo(IndexCollection other)
        {
            if (other is null)
                return 1;

            //  A value that indicates the relative order of the objects being compared. 
            //  The return value has the following meanings: 
            //
            //  Value                    Meaning
            //
            //  Less than zero          This object is less than the other parameter.
            //  Zero                    This object is equal to other.
            //  Greater than zero       This object is greater than other.

            // Quasi-lexicographic order

            // Order by length first
            int thisLength = this.Count;
            int lengthDifference = other.Count - thisLength;

            if (lengthDifference > 0)
                return -1;

            if (lengthDifference < 0)
                return 1;

            // Here if and only if lengthDifference == 0

            int result = 0;
            double thisValue, otherValue;
            for (int j = 0; j < thisLength; j++) {
                thisValue = this[j];
                otherValue = other[j];
                if (thisValue < otherValue) {
                    result = -1;
                    break;
                }
                else if (thisValue > otherValue) {
                    result = 1;
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and 
        /// returns an integer that indicates whether the current instance precedes, follows, 
        /// or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <remarks>
        /// <inheritdoc cref="IndexCollection" 
        /// path="para[@id='quasi.lexicographic.order']"/>
        /// <inheritdoc cref="IndexCollection" 
        /// path="para[@id='quasi.lexicographic.equality']"/>
        /// </remarks>
        /// <returns>A value that indicates the relative order of the objects being compared. 
        /// The return value has these meanings: 
        ///      <list type="table">
        ///         <listheader>
        ///            <term>Value</term>
        ///            <term>Meaning</term>
        ///         </listheader>
        ///         <item>
        ///            <term>Less than zero</term>
        ///            <term>This instance precedes <paramref name="obj" /> in the sort order.</term>
        ///         </item>        
        ///         <item>
        ///            <term>Zero</term>
        ///            <term>This instance occurs in the same position in the sort order as <paramref name="obj" />.</term>
        ///         </item>        
        ///         <item>
        ///            <term>Greater than zero</term>
        ///            <term>This instance follows <paramref name="obj" /> in the sort order.</term>
        ///         </item>        
        ///    </list>
        /// </returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="obj"/>
        /// is not the same type as this instance.
        /// </exception>
        /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
        public int CompareTo(Object obj)
        {
            if (obj is null)
                return 1;

            if (this.GetType() == obj.GetType()) {
                return this.CompareTo((IndexCollection)(obj));
            }

            throw new ArgumentException(
                string.Format(
                    CultureInfo.InvariantCulture,
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_OBJ_HAS_WRONG_TYPE"), 
                    "IndexCollection"),
               nameof(obj));
        }

        /// <summary>
        /// Returns a value that indicates whether 
        /// an <see cref="IndexCollection"/> instance is 
        /// less than another <see cref="IndexCollection"/> instance.
        /// </summary>
        /// <param name="left">The first instance to compare.</param>
        /// <param name="right">The second instance to compare.</param>
        /// <returns><b>true</b> if <paramref name="left"/> is less 
        /// than <paramref name="right"/>;
        /// otherwise, <b>false</b>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="IndexCollection" 
        /// path="para[@id='quasi.lexicographic.order']"/>      
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
        public static bool operator <(IndexCollection left, IndexCollection right)
        {
            if (left is null) {
                return right is null ? false : true;
            }

            return left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Returns a value that indicates whether 
        /// an <see cref="IndexCollection"/> instance is 
        /// less than or equal to another <see cref="IndexCollection"/> instance.
        /// </summary>
        /// <param name="left">The first instance to compare.</param>
        /// <param name="right">The second instance to compare.</param>
        /// <returns><b>true</b> if <paramref name="left"/> is less than 
        /// or equal to <paramref name="right"/>;
        /// otherwise, <b>false</b>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="IndexCollection" 
        /// path="para[@id='quasi.lexicographic.order']"/>
        /// <inheritdoc cref="IndexCollection" 
        /// path="para[@id='quasi.lexicographic.equality']"/>
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
        public static bool operator <=(IndexCollection left, IndexCollection right)
        {
            if (left is null) {
                return true;
            }

            return left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Returns a value that indicates whether 
        /// an <see cref="IndexCollection"/> instance is 
        /// greater than another <see cref="IndexCollection"/> instance.
        /// </summary>
        /// <param name="left">The first instance to compare.</param>
        /// <param name="right">The second instance to compare.</param>
        /// <returns><b>true</b> if <paramref name="left"/> is greater 
        /// than <paramref name="right"/>;
        /// otherwise, <b>false</b>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="IndexCollection" 
        /// path="para[@id='quasi.lexicographic.order']"/>
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
        public static bool operator >(IndexCollection left, IndexCollection right)
        {
            if (right is null) {
                return left is null ? false : true;
            }

            return right.CompareTo(left) < 0;
        }

        /// <summary>
        /// Returns a value that indicates whether 
        /// an <see cref="IndexCollection"/> instance is 
        /// greater than or equal to another <see cref="IndexCollection"/> instance.
        /// </summary>
        /// <param name="left">The first instance to compare.</param>
        /// <param name="right">The second instance to compare.</param>
        /// <returns><b>true</b> if <paramref name="left"/> is greater than 
        /// or equal to <paramref name="right"/>;
        /// otherwise, <b>false</b>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="IndexCollection" 
        /// path="para[@id='quasi.lexicographic.order']"/>
        /// <inheritdoc cref="IndexCollection" 
        /// path="para[@id='quasi.lexicographic.equality']"/>
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
        public static bool operator >=(IndexCollection left, IndexCollection right)
        {
            if (right is null) {
                return true;
            }

            return right.CompareTo(left) <= 0;
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns><b>true</b> if the current object is equal to the <paramref name="other" /> 
        /// parameter; otherwise, <b>false</b>.</returns>
        /// <remarks>
        /// <inheritdoc cref="IndexCollection" 
        /// path="para[@id='quasi.lexicographic.order']"/>
        /// <inheritdoc cref="IndexCollection" 
        /// path="para[@id='quasi.lexicographic.equality']"/>
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
        public bool Equals(IndexCollection other)
        {
            return (0 == this.CompareTo(other));
        }

        /// <summary>
        /// Determines whether the specified <see cref="object" /> 
        /// is equal to the current <see cref="object" />.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns><b>true</b> if the specified object is equal to the current object; 
        /// otherwise, <b>false</b>.</returns>
        /// <remarks>
        /// <inheritdoc cref="IndexCollection" 
        /// path="para[@id='quasi.lexicographic.order']"/>
        /// <inheritdoc cref="IndexCollection" 
        /// path="para[@id='quasi.lexicographic.equality']"/>
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
        public override bool Equals(Object obj)
        {
            if (obj is null)
                return false;

            if (this.GetType() != obj.GetType())
                return false;

            return this.Equals((IndexCollection)obj);
        }

        /// <summary>
        /// Returns a value that indicates whether 
        /// an <see cref="IndexCollection"/> instance is 
        /// equal to another <see cref="IndexCollection"/> instance.
        /// </summary>
        /// <param name="left">The first instance to compare.</param>
        /// <param name="right">The second instance to compare.</param>
        /// <returns><b>true</b> if <paramref name="left"/> is equal to <paramref name="right"/>;
        /// otherwise, <b>false</b>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="IndexCollection" 
        /// path="para[@id='quasi.lexicographic.order']"/>
        /// <inheritdoc cref="IndexCollection" 
        /// path="para[@id='quasi.lexicographic.equality']"/>
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
        public static bool operator ==(IndexCollection left, IndexCollection right)
        {
            if (left is null) {
                return right is null ? true : false;
            }

            return left.Equals(right);
        }

        /// <summary>
        /// Returns a value that indicates whether 
        /// an <see cref="IndexCollection"/> instance is not
        /// equal to another <see cref="IndexCollection"/> instance.
        /// </summary>
        /// <param name="left">The first instance to compare.</param>
        /// <param name="right">The second instance to compare.</param>
        /// <returns><b>true</b> if <paramref name="left"/> is not equal to <paramref name="right"/>;
        /// otherwise, <b>false</b>.
        /// </returns>
        /// <remarks>
        /// <inheritdoc cref="IndexCollection" 
        /// path="para[@id='quasi.lexicographic.order']"/>
        /// <inheritdoc cref="IndexCollection" 
        /// path="para[@id='quasi.lexicographic.equality']"/>
        /// </remarks>
        /// <seealso href="https://en.wikipedia.org/wiki/Lexicographical_order"/>
        public static bool operator !=(IndexCollection left, IndexCollection right)
        {
            return !(left == right);
        }

        #endregion

        #region IList

        /// <summary>
        /// Gets the number of elements in this instance.
        /// </summary>
        /// <value>The number of elements in this instance.</value>
        public int Count
        {
            get { return this.indexes.Length; }
        }

        /// <summary>
        /// Gets or sets the index in the <see cref="IndexCollection" /> corresponding
        /// the specified position.
        /// </summary>
        /// <param name="position">The position of the index to get or set.</param>
        /// <value>The index at the specified position.</value>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="value"/> is negative.<br/>
        /// -or-<br/>
        /// <paramref name="position"/> is negative or greater than or equal to
        /// <see cref="Count"/>.
        /// </exception>
        public int this[int position]
        {
            get
            {
                if (position < 0 || position >= this.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(position),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_IND_POSITION_EXCEEDS_DIMS"));
                }

                return this.indexes[position];
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE"));
                }

                if (position < 0 || position >= this.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(position),
                       ImplementationServices.GetResourceString(
                           "STR_EXCEPT_IND_POSITION_EXCEEDS_DIMS"));
                }

                int positionIndex = this.indexes[position];

                if (positionIndex != value)
                {
                    this.indexes[position] = value;

                    if (this.maxIndex < value)
                    {
                        this.maxIndex = value;
                    }
                    else
                    {

                        // Here, if maxIndex == value then maxIndex is unchanged

                        if (value < this.maxIndex)
                        {

                            // Here, if positionIndex != maxIndex then maxIndex is unchanged

                            if (positionIndex == this.maxIndex)
                            {
                                this.UpdateCollectionMaxIndex();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is read-only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool IsReadOnly { get => false; }

        /// <inheritdoc/>
        public int IndexOf(int item)
        {            
            return Array.IndexOf(this.indexes, item); ;
        }

        /// <summary>
        /// Inserts an item to the <see cref="IList{T}"></see> at the specified index.
        /// This implementation always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="index">The zero-based index at which item should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="IList{T}"></see>.</param>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        void IList<int>.Insert(int index, int item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Removes the <see cref="IList{T}"></see> item at the specified index.
        /// This implementation always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        void IList<int>.RemoveAt(int index)
        {
            throw new NotSupportedException();
        }

        #endregion

        #region ICollection

        /// <summary>
        /// Adds an item to the <see cref="ICollection{T}"></see>.
        /// This implementation always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="ICollection{T}"></see>.</param>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        void ICollection<int>.Add(int item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Removes all items from the <see cref="ICollection{T}"></see>.
        /// This implementation always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        void ICollection<int>.Clear()
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc/>
        public bool Contains(int item)
        {
            return -1 != this.IndexOf(item);
        }

        /// <inheritdoc/>
        public void CopyTo(int[] array, int arrayIndex)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(arrayIndex),
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_MUST_BE_NON_NEGATIVE"));
            }

            if (array.Length - arrayIndex < this.Count)
            {
                throw new ArgumentException(
                    ImplementationServices.GetResourceString(
                        "STR_EXCEPT_PAR_NOT_ENOUGH_SPACE_IN_ARRAY"));
            }

            this.indexes.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="ICollection{T}"></see>.
        /// This implementation always throws a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="ICollection{T}"></see>.</param>
        /// <returns>true if <paramref name="item">item</paramref> was successfully removed from 
        /// the <see cref="ICollection{T}"></see>; otherwise, false. 
        /// This method also returns false if <paramref name="item">item</paramref> is not found 
        /// in the original <see cref="ICollection{T}"></see>.</returns>
        /// <exception cref="NotSupportedException">Always thrown.</exception>
        bool ICollection<int>.Remove(int item)
        {
            throw new NotSupportedException();
        }

        #endregion

        /// <summary>
        /// Converts the specified linear index to its corresponding row and column indexes
        /// in a column major ordered matrix having the specified leading dimension.
        /// </summary>
        /// <param name="linearIndex">the linear index to convert.</param>
        /// <param name="leadingDimension">The leading dimension of the column major ordered matrix.</param>
        /// <param name="rowIndex">The resulting row index.</param>
        /// <param name="columnIndex">The resulting column index.</param>
        internal static void ConvertToTabularIndexes(
            int linearIndex, 
            int leadingDimension,
            out int rowIndex,
            out int columnIndex)
        {
            int quotient = Convert.ToInt32(Math.Floor(Convert.ToDouble(linearIndex / leadingDimension)));
            int remainder = linearIndex - (leadingDimension * quotient);
            rowIndex = remainder;
            columnIndex = quotient;
        }
    }
}

