// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems;
using System;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Provides methods to test that 
    /// binary operations whose operands
    /// have types <see cref="Double"/>, <see cref="DoubleMatrix"/>, or
    /// <see cref="ReadOnlyDoubleMatrix"/> have
    /// been properly implemented.
    /// </summary>
    static class BinaryOperationTest
    {
        /// <summary>
        /// Provides methods to test that 
        /// binary operations whose operands
        /// have <see cref="DoubleMatrix"/>, or
        /// <see cref="ReadOnlyDoubleMatrix"/> have
        /// been properly implemented.
        /// </summary>
        public static class LeftMatrixRightMatrix
        {
            static void Fail<TLeft, TRight, TException>(
                Func<TLeft, TRight, DoubleMatrix>[] operators,
                TLeft left,
                TRight right,
                TException exception)
                where TLeft : class
                where TRight : class
                where TException : Exception
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    ExceptionAssert.Throw(
                        () =>
                        {
                            operators[i](left, right);
                        },
                        expectedType: exception.GetType(),
                        expectedMessage: exception.Message);
                }
            }

            static void Succeed<TLeft, TRight>(
                Func<TLeft, TRight, DoubleMatrix>[] operators,
                TLeft left,
                TRight right,
                DoubleMatrixState expected)
                where TLeft : class
                where TRight : class
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    var actual = operators[i](left, right);
                    DoubleMatrixAssert.IsStateAsExpected(
                        expectedState: expected,
                        actualMatrix: actual,
                        delta: DoubleMatrixTest.Accuracy);
                }
            }

            /// <summary>
            /// Tests an operation
            /// whose left operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void LeftIsNull(TestableMatrixMatrixOperation<ArgumentNullException> operation)
            {
                var exception = operation.Expected;

                // Dense
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: null,
                    right: operation.Right.Dense,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: null,
                    right: operation.Right.Dense,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: null,
                    right: operation.Right.Dense.AsReadOnly(),
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: null,
                    right: operation.Right.Dense.AsReadOnly(),
                    exception: exception);

                // Sparse
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: null,
                    right: operation.Right.Sparse,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: null,
                    right: operation.Right.Sparse,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: null,
                    right: operation.Right.Sparse.AsReadOnly(),
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: null,
                    right: operation.Right.Sparse.AsReadOnly(),
                    exception: exception);

                // View
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: null,
                    right: operation.Right.View,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: null,
                    right: operation.Right.View,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: null,
                    right: operation.Right.View.AsReadOnly(),
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: null,
                    right: operation.Right.View.AsReadOnly(),
                    exception: exception);
            }

            /// <summary>
            /// Tests an operation
            /// whose right operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void RightIsNull(TestableMatrixMatrixOperation<ArgumentNullException> operation)
            {
                var exception = operation.Expected;

                // Dense
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.Dense,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.Dense.AsReadOnly(),
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.Dense,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.Dense.AsReadOnly(),
                    right: null,
                    exception: exception);

                // Sparse
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.Sparse,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.Sparse.AsReadOnly(),
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.Sparse,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.Sparse.AsReadOnly(),
                    right: null,
                    exception: exception);

                // View
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.View,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.View.AsReadOnly(),
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.View,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.View.AsReadOnly(),
                    right: null,
                    exception: exception);
            }

            /// <summary>
            /// Determines whether the specified operation terminates successfully as expected.
            /// </summary>
            /// <param name="operation">The operation.</param>
            public static void Succeed(TestableMatrixMatrixOperation<DoubleMatrixState> operation)
            {
                var result = operation.Expected;

                #region (Dense, ...)

                // (Dense, Dense)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.Dense,
                    right: operation.Right.Dense,
                    expected: result);

                // (Dense, Sparse)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.Dense,
                    right: operation.Right.Sparse,
                    expected: result);

                // (Dense, View)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.Dense,
                    right: operation.Right.View,
                    expected: result);

                // (Dense, Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.Dense,
                    right: operation.Right.Dense.AsReadOnly(),
                    expected: result);

                // (Dense, Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.Dense,
                    right: operation.Right.Sparse.AsReadOnly(),
                    expected: result);

                // (Dense, View.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.Dense,
                    right: operation.Right.View.AsReadOnly(),
                    expected: result);

                #endregion

                #region (Sparse, ...)

                // (Sparse, Dense)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.Sparse,
                    right: operation.Right.Dense,
                    expected: result);

                // (Sparse, Sparse)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.Sparse,
                    right: operation.Right.Sparse,
                    expected: result);

                // (Sparse, View)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.Sparse,
                    right: operation.Right.View,
                    expected: result);

                // (Sparse, Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.Sparse,
                    right: operation.Right.Dense.AsReadOnly(),
                    expected: result);

                // (Sparse, Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.Sparse,
                    right: operation.Right.Sparse.AsReadOnly(),
                    expected: result);

                // (Sparse, View.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.Sparse,
                    right: operation.Right.View.AsReadOnly(),
                    expected: result);

                #endregion

                #region (View, ...)

                // (View, Dense)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.View,
                    right: operation.Right.Dense,
                    expected: result);

                // (View, Sparse)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.View,
                    right: operation.Right.Sparse,
                    expected: result);

                // (View, View)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.View,
                    right: operation.Right.View,
                    expected: result);

                // (View, Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.View,
                    right: operation.Right.Dense.AsReadOnly(),
                    expected: result);

                // (View, Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.View,
                    right: operation.Right.Sparse.AsReadOnly(),
                    expected: result);

                // (View, View.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.View,
                    right: operation.Right.View.AsReadOnly(),
                    expected: result);

                #endregion

                #region (Dense.AsReadOnly(), ...)

                // (Dense.AsReadOnly(), Dense)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.Dense.AsReadOnly(),
                    right: operation.Right.Dense,
                    expected: result);

                // (Dense.AsReadOnly(), Sparse)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.Dense.AsReadOnly(),
                    right: operation.Right.Sparse,
                    expected: result);

                // (Dense.AsReadOnly(), View)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.Dense.AsReadOnly(),
                    right: operation.Right.View,
                    expected: result);

                // (Dense.AsReadOnly(), Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.Dense.AsReadOnly(),
                    right: operation.Right.Dense.AsReadOnly(),
                    expected: result);

                // (Dense.AsReadOnly(), Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.Dense.AsReadOnly(),
                    right: operation.Right.Sparse.AsReadOnly(),
                    expected: result);

                // (Dense.AsReadOnly(), View.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.Dense.AsReadOnly(),
                    right: operation.Right.View.AsReadOnly(),
                    expected: result);

                #endregion

                #region (Sparse.AsReadOnly(), ...)

                // (Sparse.AsReadOnly(), Dense)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.Sparse.AsReadOnly(),
                    right: operation.Right.Dense,
                    expected: result);

                // (Sparse.AsReadOnly(), Sparse)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.Sparse.AsReadOnly(),
                    right: operation.Right.Sparse,
                    expected: result);

                // (Sparse.AsReadOnly(), View)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.Sparse.AsReadOnly(),
                    right: operation.Right.View,
                    expected: result);

                // (Sparse.AsReadOnly(), Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.Sparse.AsReadOnly(),
                    right: operation.Right.Dense.AsReadOnly(),
                    expected: result);

                // (Sparse.AsReadOnly(), Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.Sparse.AsReadOnly(),
                    right: operation.Right.Sparse.AsReadOnly(),
                    expected: result);

                // (Sparse.AsReadOnly(), View.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.Sparse.AsReadOnly(),
                    right: operation.Right.View.AsReadOnly(),
                    expected: result);

                #endregion

                #region (View.AsReadOnly(), ...)

                // (View.AsReadOnly(), Dense)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.View.AsReadOnly(),
                    right: operation.Right.Dense,
                    expected: result);

                // (View.AsReadOnly(), Sparse)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.View.AsReadOnly(),
                    right: operation.Right.Sparse,
                    expected: result);

                // (View.AsReadOnly(), View)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.View.AsReadOnly(),
                    right: operation.Right.View,
                    expected: result);

                // (View.AsReadOnly(), Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.View.AsReadOnly(),
                    right: operation.Right.Dense.AsReadOnly(),
                    expected: result);

                // (View.AsReadOnly(), Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.View.AsReadOnly(),
                    right: operation.Right.Sparse.AsReadOnly(),
                    expected: result);

                // (View.AsReadOnly(), View.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.View.AsReadOnly(),
                    right: operation.Right.View.AsReadOnly(),
                    expected: result);

                #endregion
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(TestableMatrixMatrixOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                #region (Dense, ...)

                // (Dense, Dense)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.Dense,
                    right: operation.Right.Dense,
                    exception: exception);

                // (Dense, Sparse)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.Dense,
                    right: operation.Right.Sparse,
                    exception: exception);

                // (Dense, View)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.Dense,
                    right: operation.Right.View,
                    exception: exception);

                // (Dense, Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.Dense,
                    right: operation.Right.Dense.AsReadOnly(),
                    exception: exception);

                // (Dense, Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.Dense,
                    right: operation.Right.Sparse.AsReadOnly(),
                    exception: exception);

                // (Dense, View.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.Dense,
                    right: operation.Right.View.AsReadOnly(),
                    exception: exception);

                #endregion

                #region (Sparse, ...)

                // (Sparse, Dense)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.Sparse,
                    right: operation.Right.Dense,
                    exception: exception);

                // (Sparse, Sparse)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.Sparse,
                    right: operation.Right.Sparse,
                    exception: exception);

                // (Sparse, View)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.Sparse,
                    right: operation.Right.View,
                    exception: exception);

                // (Sparse, Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.Sparse,
                    right: operation.Right.Dense.AsReadOnly(),
                    exception: exception);

                // (Sparse, Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.Sparse,
                    right: operation.Right.Sparse.AsReadOnly(),
                    exception: exception);

                // (Sparse, View.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.Sparse,
                    right: operation.Right.View.AsReadOnly(),
                    exception: exception);

                #endregion

                #region (View, ...)

                // (View, Dense)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.View,
                    right: operation.Right.Dense,
                    exception: exception);

                // (View, Sparse)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.View,
                    right: operation.Right.Sparse,
                    exception: exception);

                // (View, View)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.View,
                    right: operation.Right.View,
                    exception: exception);

                // (View, Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.View,
                    right: operation.Right.Dense.AsReadOnly(),
                    exception: exception);

                // (View, Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.View,
                    right: operation.Right.Sparse.AsReadOnly(),
                    exception: exception);

                // (View, View.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.View,
                    right: operation.Right.View.AsReadOnly(),
                    exception: exception);

                #endregion

                #region (Dense.AsReadOnly(), ...)

                // (Dense.AsReadOnly(), Dense)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.Dense.AsReadOnly(),
                    right: operation.Right.Dense,
                    exception: exception);

                // (Dense.AsReadOnly(), Sparse)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.Dense.AsReadOnly(),
                    right: operation.Right.Sparse,
                    exception: exception);

                // (Dense.AsReadOnly(), View)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.Dense.AsReadOnly(),
                    right: operation.Right.View,
                    exception: exception);

                // (Dense.AsReadOnly(), Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.Dense.AsReadOnly(),
                    right: operation.Right.Dense.AsReadOnly(),
                    exception: exception);

                // (Dense.AsReadOnly(), Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.Dense.AsReadOnly(),
                    right: operation.Right.Sparse.AsReadOnly(),
                    exception: exception);

                // (Dense.AsReadOnly(), View.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.Dense.AsReadOnly(),
                    right: operation.Right.View.AsReadOnly(),
                    exception: exception);

                #endregion

                #region (Sparse.AsReadOnly(), ...)

                // (Sparse.AsReadOnly(), Dense)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.Sparse.AsReadOnly(),
                    right: operation.Right.Dense,
                    exception: exception);

                // (Sparse.AsReadOnly(), Sparse)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.Sparse.AsReadOnly(),
                    right: operation.Right.Sparse,
                    exception: exception);

                // (Sparse.AsReadOnly(), View)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.Sparse.AsReadOnly(),
                    right: operation.Right.View,
                    exception: exception);

                // (Sparse.AsReadOnly(), Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.Sparse.AsReadOnly(),
                    right: operation.Right.Dense.AsReadOnly(),
                    exception: exception);

                // (Sparse.AsReadOnly(), Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.Sparse.AsReadOnly(),
                    right: operation.Right.Sparse.AsReadOnly(),
                    exception: exception);

                // (Sparse.AsReadOnly(), View.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.Sparse.AsReadOnly(),
                    right: operation.Right.View.AsReadOnly(),
                    exception: exception);

                #endregion

                #region (View.AsReadOnly(), ...)

                // (View.AsReadOnly(), Dense)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.View.AsReadOnly(),
                    right: operation.Right.Dense,
                    exception: exception);

                // (View.AsReadOnly(), Sparse)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.View.AsReadOnly(),
                    right: operation.Right.Sparse,
                    exception: exception);

                // (View.AsReadOnly(), View)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.View.AsReadOnly(),
                    right: operation.Right.View,
                    exception: exception);

                // (View.AsReadOnly(), Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.View.AsReadOnly(),
                    right: operation.Right.Dense.AsReadOnly(),
                    exception: exception);

                // (View.AsReadOnly(), Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.View.AsReadOnly(),
                    right: operation.Right.Sparse.AsReadOnly(),
                    exception: exception);

                // (View.AsReadOnly(), View.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.View.AsReadOnly(),
                    right: operation.Right.View.AsReadOnly(),
                    exception: exception);

                #endregion
            }
        }

        /// <summary>
        /// Provides methods to test that 
        /// binary operations having <see cref="Double"/> left operands 
        /// and <see cref="DoubleMatrix"/> or
        /// <see cref="ReadOnlyDoubleMatrix"/> right operands
        /// have been properly implemented.
        /// </summary>
        public static class LeftScalarRightMatrix
        {
            static void Fail<TRight, TException>(
                Func<double, TRight, DoubleMatrix>[] operators,
                double left,
                TRight right,
                TException exception)
                where TRight : class
                where TException : Exception
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    ExceptionAssert.Throw(
                        () =>
                        {
                            operators[i](left, right);
                        },
                        expectedType: exception.GetType(),
                        expectedMessage: exception.Message);
                }
            }

            static void Succeed<TRight>(
                Func<double, TRight, DoubleMatrix>[] operators,
                double left,
                TRight right,
                DoubleMatrixState expected)
                where TRight : class
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    var actual = operators[i](left, right);
                    DoubleMatrixAssert.IsStateAsExpected(
                        expectedState: expected,
                        actualMatrix: actual,
                        delta: DoubleMatrixTest.Accuracy);
                }
            }

            /// <summary>
            /// Tests an operation
            /// whose right operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void RightIsNull(TestableScalarMatrixOperation<ArgumentNullException> operation)
            {
                var exception = operation.Expected;

                // Dense
                Fail(
                    operators: operation.LeftScalarRightWritableOps,
                    left: 0.0,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: 0.0,
                    right: null,
                    exception: exception);

                // Sparse
                Fail(
                    operators: operation.LeftScalarRightWritableOps,
                    left: 0.0,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: 0.0,
                    right: null,
                    exception: exception);

                // View
                Fail(
                    operators: operation.LeftScalarRightWritableOps,
                    left: 0.0,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: 0.0,
                    right: null,
                    exception: exception);
            }

            /// <summary>
            /// Determines whether the specified operation terminates successfully as expected.
            /// </summary>
            /// <param name="operation">The operation.</param>
            public static void Succeed(TestableScalarMatrixOperation<DoubleMatrixState> operation)
            {
                var result = operation.Expected;

                #region (double, ...)

                // (double, Dense)
                Succeed(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.Dense,
                    expected: result);

                // (double, Sparse)
                Succeed(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.Sparse,
                    expected: result);

                // (double, View)
                Succeed(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.View,
                    expected: result);

                // (double, Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.Dense.AsReadOnly(),
                    expected: result);

                // (double, Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.Sparse.AsReadOnly(),
                    expected: result);

                // (double, View.AsReadOnly())
                Succeed(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.View.AsReadOnly(),
                    expected: result);

                #endregion
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(TestableScalarMatrixOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                #region (double, ...)

                // (double, Dense)
                Fail(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.Dense,
                    exception: exception);

                // (double, Sparse)
                Fail(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.Sparse,
                    exception: exception);

                // (double, View)
                Fail(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.View,
                    exception: exception);

                // (double, Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.Dense.AsReadOnly(),
                    exception: exception);

                // (double, Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.Sparse.AsReadOnly(),
                    exception: exception);

                // (double, View.AsReadOnly())
                Fail(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.View.AsReadOnly(),
                    exception: exception);

                #endregion
            }
        }

        /// <summary>
        /// Provides methods to test that 
        /// binary operations having <see cref="DoubleMatrix"/> or
        /// <see cref="ReadOnlyDoubleMatrix"/> left operands 
        /// and <see cref="Double"/> right operands
        /// have been properly implemented.
        /// </summary>
        public static class LeftMatrixRightScalar
        {
            static void Fail<TLeft, TException>(
                Func<TLeft, double, DoubleMatrix>[] operators,
                TLeft left,
                double right,
                TException exception)
                where TLeft : class
                where TException : Exception
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    ExceptionAssert.Throw(
                        () =>
                        {
                            operators[i](left, right);
                        },
                        expectedType: exception.GetType(),
                        expectedMessage: exception.Message);
                }
            }

            static void Succeed<TLeft>(
                Func<TLeft, double, DoubleMatrix>[] operators,
                TLeft left,
                double right,
                DoubleMatrixState expected)
                where TLeft : class
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    var actual = operators[i](left, right);
                    DoubleMatrixAssert.IsStateAsExpected(
                        expectedState: expected,
                        actualMatrix: actual,
                        delta: DoubleMatrixTest.Accuracy);
                }
            }

            /// <summary>
            /// Tests an operation
            /// whose left operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void LeftIsNull(TestableMatrixScalarOperation<ArgumentNullException> operation)
            {
                var exception = operation.Expected;

                // Dense
                Fail(
                    operators: operation.LeftWritableRightScalarOps,
                    left: null,
                    right: 0.0,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: null,
                    right: 0.0,
                    exception: exception);

                // Sparse
                Fail(
                    operators: operation.LeftWritableRightScalarOps,
                    left: null,
                    right: 0.0,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: null,
                    right: 0.0,
                    exception: exception);

                // View
                Fail(
                    operators: operation.LeftWritableRightScalarOps,
                    left: null,
                    right: 0.0,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: null,
                    right: 0.0,
                    exception: exception);
            }

            /// <summary>
            /// Determines whether the specified operation terminates successfully as expected.
            /// </summary>
            /// <param name="operation">The operation.</param>
            public static void Succeed(TestableMatrixScalarOperation<DoubleMatrixState> operation)
            {
                var result = operation.Expected;

                #region (..., double)

                // (Dense, double)
                Succeed(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.Dense,
                    right: operation.Right,
                    expected: result);

                // (Sparse, double)
                Succeed(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.Sparse,
                    right: operation.Right,
                    expected: result);

                // (View, double)
                Succeed(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.View,
                    right: operation.Right,
                    expected: result);

                // (Dense.AsReadOnly(), double)
                Succeed(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.Dense.AsReadOnly(),
                    right: operation.Right,
                    expected: result);

                // (Sparse.AsReadOnly(), double)
                Succeed(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.Sparse.AsReadOnly(),
                    right: operation.Right,
                    expected: result);

                // (View.AsReadOnly(), double)
                Succeed(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.View.AsReadOnly(),
                    right: operation.Right,
                    expected: result);

                #endregion
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(TestableMatrixScalarOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                #region (..., double)

                // (Dense, double)
                Fail(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.Dense,
                    right: operation.Right,
                    exception: exception);

                // (Sparse, double)
                Fail(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.Sparse,
                    right: operation.Right,
                    exception: exception);

                // (View, double)
                Fail(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.View,
                    right: operation.Right,
                    exception: exception);

                // (Dense.AsReadOnly(), double)
                Fail(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.Dense.AsReadOnly(),
                    right: operation.Right,
                    exception: exception);

                // (Sparse.AsReadOnly(), double)
                Fail(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.Sparse.AsReadOnly(),
                    right: operation.Right,
                    exception: exception);

                // (View.AsReadOnly(), double)
                Fail(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.View.AsReadOnly(),
                    right: operation.Right,
                    exception: exception);

                #endregion
            }
        }
    }
}
