// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Novacta.Analytics.Tests.Tools
{
    class UnknownClass
    {
    }

    /// <summary>
    /// Provides methods to test that the <see cref="IComparable{T}"/> interface 
    /// and related interfaces have been properly implemented when dealing 
    /// with <b>null</b> values.
    /// </summary>
    static class ComparableObjectTest
    {
        /// <summary>
        /// Tests the <see cref="IEquatable{T}.Equals(T)"/> implementation
        /// when dealing with nulls.
        /// </summary>
        /// <param name="obj">An object not <b>null</b>.</param>
        /// <remarks>
        /// <para>
        /// Objects being <b>null</b> are only equal to other <b>null</b> 
        /// objects.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj"/> is <b>null</b>.
        /// </exception>
        public static void EqualsWithNulls<T>(T obj)
            where T : class,
            IEquatable<T>,
            IComparable<T>,
            IComparable
        {
            if (obj is null) {
                throw new ArgumentNullException(nameof(obj));
            }

            Assert.IsFalse(obj.Equals((T)null));

            UnknownClass unknownClass = null;
            Assert.IsFalse(obj.Equals(unknownClass));

            Assert.IsTrue((T)null == (T)null);

            Assert.IsFalse((T)null == obj);

            Assert.IsFalse(obj == (T)null);

            Assert.IsFalse((T)null != (T)null);

            Assert.IsTrue((T)null != obj);

            Assert.IsTrue(obj != (T)null);
        }

        /// <summary>
        /// Tests the <see cref="IComparable{T}.CompareTo(T)"/> implementation
        /// of the <see cref="IndexCollection"/> class when dealing with nulls.
        /// </summary>
        /// <param name="obj">An object not <b>null</b>.</param>
        /// <remarks>
        /// <para>
        /// Objects being <b>null</b> are only equal to other <b>null</b> 
        /// objects. They are less than every other non <b>null</b> object.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj"/> is <b>null</b>.
        /// </exception>
        public static void CompareToWithNulls(IndexCollection obj)
        {
            if (obj is null) {
                throw new ArgumentNullException(nameof(obj));
            }

            // <=

            Assert.IsTrue((IndexCollection)null <= (IndexCollection)null);

            Assert.IsTrue((IndexCollection)null <= obj);

            Assert.IsFalse(obj <= (IndexCollection)null);

            // <

            Assert.IsFalse((IndexCollection)null < (IndexCollection)null);

            Assert.IsTrue((IndexCollection)null < obj);

            Assert.IsFalse(obj < (IndexCollection)null);

            // >=

            Assert.IsTrue((IndexCollection)null >= (IndexCollection)null);

            Assert.IsFalse((IndexCollection)null >= obj);

            Assert.IsTrue(obj >= (IndexCollection)null);

            // >

            Assert.IsFalse((IndexCollection)null > (IndexCollection)null);

            Assert.IsFalse((IndexCollection)null > obj);

            Assert.IsTrue(obj > (IndexCollection)null);
        }


        /// <summary>
        /// Tests the <see cref="IComparable{T}.CompareTo(T)"/> implementation
        /// of the <see cref="DoubleMatrixRow"/> class when dealing with nulls.
        /// </summary>
        /// <param name="obj">An object not <b>null</b>.</param>
        /// <remarks>
        /// <para>
        /// Objects being <b>null</b> are only equal to other <b>null</b> 
        /// objects. They are less than every other non <b>null</b> object.
        /// </para>
        /// </remarks>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="obj"/> is <b>null</b>.
        /// </exception>
        public static void CompareToWithNulls(DoubleMatrixRow obj)
        {
            if (obj is null) {
                throw new ArgumentNullException(nameof(obj));
            }

            // <=

            Assert.IsTrue((DoubleMatrixRow)null <= (DoubleMatrixRow)null);

            Assert.IsTrue((DoubleMatrixRow)null <= obj);

            Assert.IsFalse(obj <= (DoubleMatrixRow)null);

            // <

            Assert.IsFalse((DoubleMatrixRow)null < (DoubleMatrixRow)null);

            Assert.IsTrue((DoubleMatrixRow)null < obj);

            Assert.IsFalse(obj < (DoubleMatrixRow)null);

            // >=

            Assert.IsTrue((DoubleMatrixRow)null >= (DoubleMatrixRow)null);

            Assert.IsFalse((DoubleMatrixRow)null >= obj);

            Assert.IsTrue(obj >= (DoubleMatrixRow)null);

            // >

            Assert.IsFalse((DoubleMatrixRow)null > (DoubleMatrixRow)null);

            Assert.IsFalse((DoubleMatrixRow)null > obj);

            Assert.IsTrue(obj > (DoubleMatrixRow)null);
        }
    }
}
