// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems;
using System;
using System.Numerics;

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
        #region Double left, Double right

        /// <summary>
        /// Provides methods to test that 
        /// binary operations whose operands
        /// have <see cref="DoubleMatrix"/>, or
        /// <see cref="ReadOnlyDoubleMatrix"/> have
        /// been properly implemented.
        /// </summary>
        public static class LeftDoubleMatrixRightDoubleMatrix
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
            public static void LeftIsNull(TestableDoubleMatrixDoubleMatrixOperation<ArgumentNullException> operation)
            {
                var exception = operation.Expected;

                // Dense
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: null,
                    right: operation.Right.AsDense,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: null,
                    right: operation.Right.AsDense,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: null,
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: null,
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // Sparse
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: null,
                    right: operation.Right.AsSparse,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: null,
                    right: operation.Right.AsSparse,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: null,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: null,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);
            }

            /// <summary>
            /// Tests an operation
            /// whose right operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void RightIsNull(TestableDoubleMatrixDoubleMatrixOperation<ArgumentNullException> operation)
            {
                var exception = operation.Expected;

                // Dense
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsDense,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsDense,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: null,
                    exception: exception);

                // Sparse
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsSparse,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsSparse,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: null,
                    exception: exception);
            }

            /// <summary>
            /// Determines whether the specified operation terminates successfully as expected.
            /// </summary>
            /// <param name="operation">The operation.</param>
            public static void Succeed(TestableDoubleMatrixDoubleMatrixOperation<DoubleMatrixState> operation)
            {
                var result = operation.Expected;

                #region (Dense, ...)

                // (Dense, Dense)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsDense,
                    expected: result);

                // (Dense, Sparse)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsSparse,
                    expected: result);

                // (Dense, Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsDense.AsReadOnly(),
                    expected: result);

                // (Dense, Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    expected: result);

                #endregion

                #region (Sparse, ...)

                // (Sparse, Dense)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsDense,
                    expected: result);

                // (Sparse, Sparse)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsSparse,
                    expected: result);

                // (Sparse, Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsDense.AsReadOnly(),
                    expected: result);

                // (Sparse, Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    expected: result);

                #endregion

                #region (Dense.AsReadOnly(), ...)

                // (Dense.AsReadOnly(), Dense)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsDense,
                    expected: result);

                // (Dense.AsReadOnly(), Sparse)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsSparse,
                    expected: result);

                // (Dense.AsReadOnly(), Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsDense.AsReadOnly(),
                    expected: result);

                // (Dense.AsReadOnly(), Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsSparse.AsReadOnly(),
                    expected: result);

                #endregion

                #region (Sparse.AsReadOnly(), ...)

                // (Sparse.AsReadOnly(), Dense)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsDense,
                    expected: result);

                // (Sparse.AsReadOnly(), Sparse)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsSparse,
                    expected: result);

                // (Sparse.AsReadOnly(), Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsDense.AsReadOnly(),
                    expected: result);

                // (Sparse.AsReadOnly(), Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsSparse.AsReadOnly(),
                    expected: result);

                #endregion
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(TestableDoubleMatrixDoubleMatrixOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                #region (Dense, ...)

                // (Dense, Dense)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsDense,
                    exception: exception);

                // (Dense, Sparse)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsSparse,
                    exception: exception);

                // (Dense, Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // (Dense, Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);

                #endregion

                #region (Sparse, ...)

                // (Sparse, Dense)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsDense,
                    exception: exception);

                // (Sparse, Sparse)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsSparse,
                    exception: exception);

                // (Sparse, Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // (Sparse, Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);

                #endregion

                #region (Dense.AsReadOnly(), ...)

                // (Dense.AsReadOnly(), Dense)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsDense,
                    exception: exception);

                // (Dense.AsReadOnly(), Sparse)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsSparse,
                    exception: exception);

                // (Dense.AsReadOnly(), Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // (Dense.AsReadOnly(), Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);

                #endregion

                #region (Sparse.AsReadOnly(), ...)

                // (Sparse.AsReadOnly(), Dense)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsDense,
                    exception: exception);

                // (Sparse.AsReadOnly(), Sparse)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsSparse,
                    exception: exception);

                // (Sparse.AsReadOnly(), Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // (Sparse.AsReadOnly(), Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsSparse.AsReadOnly(),
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
        public static class LeftDoubleScalarRightDoubleMatrix
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
            public static void RightIsNull(TestableDoubleScalarDoubleMatrixOperation<ArgumentNullException> operation)
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
            public static void Succeed(TestableDoubleScalarDoubleMatrixOperation<DoubleMatrixState> operation)
            {
                var result = operation.Expected;

                #region (double, ...)

                // (double, Dense)
                Succeed(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.AsDense,
                    expected: result);

                // (double, Sparse)
                Succeed(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.AsSparse,
                    expected: result);

                // (double, Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.AsDense.AsReadOnly(),
                    expected: result);

                // (double, Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    expected: result);

                #endregion
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(TestableDoubleScalarDoubleMatrixOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                #region (double, ...)

                // (double, Dense)
                Fail(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.AsDense,
                    exception: exception);

                // (double, Sparse)
                Fail(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.AsSparse,
                    exception: exception);

                // (double, Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // (double, Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.AsSparse.AsReadOnly(),
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
        public static class LeftDoubleMatrixRightDoubleScalar
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
            public static void LeftIsNull(TestableDoubleMatrixDoubleScalarOperation<ArgumentNullException> operation)
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
            public static void Succeed(TestableDoubleMatrixDoubleScalarOperation<DoubleMatrixState> operation)
            {
                var result = operation.Expected;

                #region (..., double)

                // (Dense, double)
                Succeed(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.AsDense,
                    right: operation.Right,
                    expected: result);

                // (Sparse, double)
                Succeed(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right,
                    expected: result);

                // (Dense.AsReadOnly(), double)
                Succeed(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right,
                    expected: result);

                // (Sparse.AsReadOnly(), double)
                Succeed(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right,
                    expected: result);

                #endregion
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(TestableDoubleMatrixDoubleScalarOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                #region (..., double)

                // (Dense, double)
                Fail(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.AsDense,
                    right: operation.Right,
                    exception: exception);

                // (Sparse, double)
                Fail(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right,
                    exception: exception);

                // (Dense.AsReadOnly(), double)
                Fail(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right,
                    exception: exception);

                // (Sparse.AsReadOnly(), double)
                Fail(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right,
                    exception: exception);

                #endregion
            }
        }

        #endregion

        #region Complex left, Complex right

        /// <summary>
        /// Provides methods to test that 
        /// binary operations whose operands
        /// have <see cref="ComplexMatrix"/>, or
        /// <see cref="ReadOnlyComplexMatrix"/> have
        /// been properly implemented.
        /// </summary>
        public static class LeftComplexMatrixRightComplexMatrix
        {
            static void Fail<TLeft, TRight, TException>(
                Func<TLeft, TRight, ComplexMatrix>[] operators,
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
                Func<TLeft, TRight, ComplexMatrix>[] operators,
                TLeft left,
                TRight right,
                ComplexMatrixState expected)
                where TLeft : class
                where TRight : class
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    var actual = operators[i](left, right);
                    ComplexMatrixAssert.IsStateAsExpected(
                        expectedState: expected,
                        actualMatrix: actual,
                        delta: ComplexMatrixTest.Accuracy);
                }
            }

            /// <summary>
            /// Tests an operation
            /// whose left operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void LeftIsNull(TestableComplexMatrixComplexMatrixOperation<ArgumentNullException> operation)
            {
                var exception = operation.Expected;

                // Dense
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: null,
                    right: operation.Right.AsDense,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: null,
                    right: operation.Right.AsDense,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: null,
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: null,
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // Sparse
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: null,
                    right: operation.Right.AsSparse,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: null,
                    right: operation.Right.AsSparse,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: null,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: null,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);
            }

            /// <summary>
            /// Tests an operation
            /// whose right operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void RightIsNull(TestableComplexMatrixComplexMatrixOperation<ArgumentNullException> operation)
            {
                var exception = operation.Expected;

                // Dense
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsDense,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsDense,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: null,
                    exception: exception);

                // Sparse
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsSparse,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsSparse,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: null,
                    exception: exception);
            }

            /// <summary>
            /// Determines whether the specified operation terminates successfully as expected.
            /// </summary>
            /// <param name="operation">The operation.</param>
            public static void Succeed(TestableComplexMatrixComplexMatrixOperation<ComplexMatrixState> operation)
            {
                var result = operation.Expected;

                #region (Dense, ...)

                // (Dense, Dense)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsDense,
                    expected: result);

                // (Dense, Sparse)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsSparse,
                    expected: result);

                // (Dense, Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsDense.AsReadOnly(),
                    expected: result);

                // (Dense, Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    expected: result);

                #endregion

                #region (Sparse, ...)

                // (Sparse, Dense)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsDense,
                    expected: result);

                // (Sparse, Sparse)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsSparse,
                    expected: result);

                // (Sparse, Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsDense.AsReadOnly(),
                    expected: result);

                // (Sparse, Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    expected: result);

                #endregion

                #region (Dense.AsReadOnly(), ...)

                // (Dense.AsReadOnly(), Dense)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsDense,
                    expected: result);

                // (Dense.AsReadOnly(), Sparse)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsSparse,
                    expected: result);

                // (Dense.AsReadOnly(), Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsDense.AsReadOnly(),
                    expected: result);

                // (Dense.AsReadOnly(), Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsSparse.AsReadOnly(),
                    expected: result);

                #endregion

                #region (Sparse.AsReadOnly(), ...)

                // (Sparse.AsReadOnly(), Dense)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsDense,
                    expected: result);

                // (Sparse.AsReadOnly(), Sparse)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsSparse,
                    expected: result);

                // (Sparse.AsReadOnly(), Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsDense.AsReadOnly(),
                    expected: result);

                // (Sparse.AsReadOnly(), Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsSparse.AsReadOnly(),
                    expected: result);

                #endregion
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(TestableComplexMatrixComplexMatrixOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                #region (Dense, ...)

                // (Dense, Dense)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsDense,
                    exception: exception);

                // (Dense, Sparse)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsSparse,
                    exception: exception);

                // (Dense, Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // (Dense, Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);

                #endregion

                #region (Sparse, ...)

                // (Sparse, Dense)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsDense,
                    exception: exception);

                // (Sparse, Sparse)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsSparse,
                    exception: exception);

                // (Sparse, Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // (Sparse, Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);

                #endregion

                #region (Dense.AsReadOnly(), ...)

                // (Dense.AsReadOnly(), Dense)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsDense,
                    exception: exception);

                // (Dense.AsReadOnly(), Sparse)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsSparse,
                    exception: exception);

                // (Dense.AsReadOnly(), Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // (Dense.AsReadOnly(), Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);

                #endregion

                #region (Sparse.AsReadOnly(), ...)

                // (Sparse.AsReadOnly(), Dense)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsDense,
                    exception: exception);

                // (Sparse.AsReadOnly(), Sparse)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsSparse,
                    exception: exception);

                // (Sparse.AsReadOnly(), Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // (Sparse.AsReadOnly(), Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);

                #endregion
            }
        }

        /// <summary>
        /// Provides methods to test that 
        /// binary operations having <see cref="Complex"/> left operands 
        /// and <see cref="ComplexMatrix"/> or
        /// <see cref="ReadOnlyComplexMatrix"/> right operands
        /// have been properly implemented.
        /// </summary>
        public static class LeftComplexScalarRightComplexMatrix
        {
            static void Fail<TRight, TException>(
                Func<Complex, TRight, ComplexMatrix>[] operators,
                Complex left,
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
                Func<Complex, TRight, ComplexMatrix>[] operators,
                Complex left,
                TRight right,
                ComplexMatrixState expected)
                where TRight : class
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    var actual = operators[i](left, right);
                    ComplexMatrixAssert.IsStateAsExpected(
                        expectedState: expected,
                        actualMatrix: actual,
                        delta: ComplexMatrixTest.Accuracy);
                }
            }

            /// <summary>
            /// Tests an operation
            /// whose right operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void RightIsNull(TestableComplexScalarComplexMatrixOperation<ArgumentNullException> operation)
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
            public static void Succeed(TestableComplexScalarComplexMatrixOperation<ComplexMatrixState> operation)
            {
                var result = operation.Expected;

                #region (Complex, ...)

                // (Complex, Dense)
                Succeed(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.AsDense,
                    expected: result);

                // (Complex, Sparse)
                Succeed(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.AsSparse,
                    expected: result);

                // (Complex, Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.AsDense.AsReadOnly(),
                    expected: result);

                // (Complex, Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    expected: result);

                #endregion
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(TestableComplexScalarComplexMatrixOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                #region (Complex, ...)

                // (Complex, Dense)
                Fail(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.AsDense,
                    exception: exception);

                // (Complex, Sparse)
                Fail(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.AsSparse,
                    exception: exception);

                // (Complex, Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // (Complex, Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);

                #endregion
            }
        }

        /// <summary>
        /// Provides methods to test that 
        /// binary operations having <see cref="ComplexMatrix"/> or
        /// <see cref="ReadOnlyComplexMatrix"/> left operands 
        /// and <see cref="Complex"/> right operands
        /// have been properly implemented.
        /// </summary>
        public static class LeftComplexMatrixRightComplexScalar
        {
            static void Fail<TLeft, TException>(
                Func<TLeft, Complex, ComplexMatrix>[] operators,
                TLeft left,
                Complex right,
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
                Func<TLeft, Complex, ComplexMatrix>[] operators,
                TLeft left,
                Complex right,
                ComplexMatrixState expected)
                where TLeft : class
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    var actual = operators[i](left, right);
                    ComplexMatrixAssert.IsStateAsExpected(
                        expectedState: expected,
                        actualMatrix: actual,
                        delta: ComplexMatrixTest.Accuracy);
                }
            }

            /// <summary>
            /// Tests an operation
            /// whose left operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void LeftIsNull(TestableComplexMatrixComplexScalarOperation<ArgumentNullException> operation)
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
            public static void Succeed(TestableComplexMatrixComplexScalarOperation<ComplexMatrixState> operation)
            {
                var result = operation.Expected;

                #region (..., Complex)

                // (Dense, Complex)
                Succeed(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.AsDense,
                    right: operation.Right,
                    expected: result);

                // (Sparse, Complex)
                Succeed(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right,
                    expected: result);

                // (Dense.AsReadOnly(), Complex)
                Succeed(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right,
                    expected: result);

                // (Sparse.AsReadOnly(), Complex)
                Succeed(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right,
                    expected: result);

                #endregion
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(TestableComplexMatrixComplexScalarOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                #region (..., Complex)

                // (Dense, Complex)
                Fail(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.AsDense,
                    right: operation.Right,
                    exception: exception);

                // (Sparse, Complex)
                Fail(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right,
                    exception: exception);

                // (Dense.AsReadOnly(), Complex)
                Fail(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right,
                    exception: exception);

                // (Sparse.AsReadOnly(), Complex)
                Fail(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right,
                    exception: exception);

                #endregion
            }
        }

        #endregion

        #region Double left, Complex right

        /// <summary>
        /// Provides methods to test  
        /// binary operations having left operands
        /// typed as <see cref="DoubleMatrix"/> or
        /// <see cref="ReadOnlyDoubleMatrix"/>,
        /// and right operands
        /// having types <see cref="ComplexMatrix"/> or
        /// <see cref="ReadOnlyComplexMatrix"/>.
        /// </summary>
        public static class LeftDoubleMatrixRightComplexMatrix
        {
            static void Fail<TLeft, TRight, TException>(
                Func<TLeft, TRight, ComplexMatrix>[] operators,
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
                Func<TLeft, TRight, ComplexMatrix>[] operators,
                TLeft left,
                TRight right,
                ComplexMatrixState expected)
                where TLeft : class
                where TRight : class
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    var actual = operators[i](left, right);
                    ComplexMatrixAssert.IsStateAsExpected(
                        expectedState: expected,
                        actualMatrix: actual,
                        delta: ComplexMatrixTest.Accuracy);
                }
            }

            /// <summary>
            /// Tests an operation
            /// whose left operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void LeftIsNull(TestableDoubleMatrixComplexMatrixOperation<ArgumentNullException> operation)
            {
                var exception = operation.Expected;

                // Dense
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: null,
                    right: operation.Right.AsDense,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: null,
                    right: operation.Right.AsDense,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: null,
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: null,
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // Sparse
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: null,
                    right: operation.Right.AsSparse,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: null,
                    right: operation.Right.AsSparse,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: null,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: null,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);
            }

            /// <summary>
            /// Tests an operation
            /// whose right operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void RightIsNull(TestableDoubleMatrixComplexMatrixOperation<ArgumentNullException> operation)
            {
                var exception = operation.Expected;

                // Dense
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsDense,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsDense,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: null,
                    exception: exception);

                // Sparse
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsSparse,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsSparse,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: null,
                    exception: exception);
            }

            /// <summary>
            /// Determines whether the specified operation terminates successfully as expected.
            /// </summary>
            /// <param name="operation">The operation.</param>
            public static void Succeed(TestableDoubleMatrixComplexMatrixOperation<ComplexMatrixState> operation)
            {
                var result = operation.Expected;

                #region (Dense, ...)

                // (Dense, Dense)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsDense,
                    expected: result);

                // (Dense, Sparse)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsSparse,
                    expected: result);

                // (Dense, Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsDense.AsReadOnly(),
                    expected: result);

                // (Dense, Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    expected: result);

                #endregion

                #region (Sparse, ...)

                // (Sparse, Dense)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsDense,
                    expected: result);

                // (Sparse, Sparse)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsSparse,
                    expected: result);

                // (Sparse, Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsDense.AsReadOnly(),
                    expected: result);

                // (Sparse, Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    expected: result);

                #endregion

                #region (Dense.AsReadOnly(), ...)

                // (Dense.AsReadOnly(), Dense)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsDense,
                    expected: result);

                // (Dense.AsReadOnly(), Sparse)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsSparse,
                    expected: result);

                // (Dense.AsReadOnly(), Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsDense.AsReadOnly(),
                    expected: result);

                // (Dense.AsReadOnly(), Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsSparse.AsReadOnly(),
                    expected: result);

                #endregion

                #region (Sparse.AsReadOnly(), ...)

                // (Sparse.AsReadOnly(), Dense)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsDense,
                    expected: result);

                // (Sparse.AsReadOnly(), Sparse)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsSparse,
                    expected: result);

                // (Sparse.AsReadOnly(), Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsDense.AsReadOnly(),
                    expected: result);

                // (Sparse.AsReadOnly(), Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsSparse.AsReadOnly(),
                    expected: result);

                #endregion
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(TestableDoubleMatrixComplexMatrixOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                #region (Dense, ...)

                // (Dense, Dense)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsDense,
                    exception: exception);

                // (Dense, Sparse)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsSparse,
                    exception: exception);

                // (Dense, Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // (Dense, Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);

                #endregion

                #region (Sparse, ...)

                // (Sparse, Dense)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsDense,
                    exception: exception);

                // (Sparse, Sparse)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsSparse,
                    exception: exception);

                // (Sparse, Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // (Sparse, Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);

                #endregion

                #region (Dense.AsReadOnly(), ...)

                // (Dense.AsReadOnly(), Dense)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsDense,
                    exception: exception);

                // (Dense.AsReadOnly(), Sparse)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsSparse,
                    exception: exception);

                // (Dense.AsReadOnly(), Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // (Dense.AsReadOnly(), Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);

                #endregion

                #region (Sparse.AsReadOnly(), ...)

                // (Sparse.AsReadOnly(), Dense)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsDense,
                    exception: exception);

                // (Sparse.AsReadOnly(), Sparse)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsSparse,
                    exception: exception);

                // (Sparse.AsReadOnly(), Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // (Sparse.AsReadOnly(), Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);

                #endregion
            }
        }

        /// <summary>
        /// Provides methods to test  
        /// binary operations having left operands
        /// typed as <see cref="Double"/>,
        /// and right operands
        /// having types <see cref="ComplexMatrix"/> or
        /// <see cref="ReadOnlyComplexMatrix"/>.
        /// </summary>
        public static class LeftDoubleScalarRightComplexMatrix
        {
            static void Fail<TRight, TException>(
                Func<double, TRight, ComplexMatrix>[] operators,
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
                Func<double, TRight, ComplexMatrix>[] operators,
                double left,
                TRight right,
                ComplexMatrixState expected)
                where TRight : class
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    var actual = operators[i](left, right);
                    ComplexMatrixAssert.IsStateAsExpected(
                        expectedState: expected,
                        actualMatrix: actual,
                        delta: ComplexMatrixTest.Accuracy);
                }
            }

            /// <summary>
            /// Tests an operation
            /// whose right operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void RightIsNull(TestableDoubleScalarComplexMatrixOperation<ArgumentNullException> operation)
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
            public static void Succeed(TestableDoubleScalarComplexMatrixOperation<ComplexMatrixState> operation)
            {
                var result = operation.Expected;

                #region (Complex, ...)

                // (Complex, Dense)
                Succeed(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.AsDense,
                    expected: result);

                // (Complex, Sparse)
                Succeed(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.AsSparse,
                    expected: result);

                // (Complex, Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.AsDense.AsReadOnly(),
                    expected: result);

                // (Complex, Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    expected: result);

                #endregion
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(TestableDoubleScalarComplexMatrixOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                #region (Complex, ...)

                // (Complex, Dense)
                Fail(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.AsDense,
                    exception: exception);

                // (Complex, Sparse)
                Fail(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.AsSparse,
                    exception: exception);

                // (Complex, Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // (Complex, Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);

                #endregion
            }
        }

        /// <summary>
        /// Provides methods to test  
        /// binary operations having left operands
        /// typed as <see cref="DoubleMatrix"/> or
        /// <see cref="ReadOnlyDoubleMatrix"/>,
        /// and right operands
        /// having type <see cref="Complex"/>.
        /// </summary>
        public static class LeftDoubleMatrixRightComplexScalar
        {
            static void Fail<TLeft, TException>(
                Func<TLeft, Complex, ComplexMatrix>[] operators,
                TLeft left,
                Complex right,
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
                Func<TLeft, Complex, ComplexMatrix>[] operators,
                TLeft left,
                Complex right,
                ComplexMatrixState expected)
                where TLeft : class
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    var actual = operators[i](left, right);
                    ComplexMatrixAssert.IsStateAsExpected(
                        expectedState: expected,
                        actualMatrix: actual,
                        delta: ComplexMatrixTest.Accuracy);
                }
            }

            /// <summary>
            /// Tests an operation
            /// whose left operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void LeftIsNull(TestableDoubleMatrixComplexScalarOperation<ArgumentNullException> operation)
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
            public static void Succeed(TestableDoubleMatrixComplexScalarOperation<ComplexMatrixState> operation)
            {
                var result = operation.Expected;

                #region (..., Complex)

                // (Dense, Complex)
                Succeed(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.AsDense,
                    right: operation.Right,
                    expected: result);

                // (Sparse, Complex)
                Succeed(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right,
                    expected: result);

                // (Dense.AsReadOnly(), Complex)
                Succeed(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right,
                    expected: result);

                // (Sparse.AsReadOnly(), Complex)
                Succeed(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right,
                    expected: result);

                #endregion
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(TestableDoubleMatrixComplexScalarOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                #region (..., Complex)

                // (Dense, Complex)
                Fail(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.AsDense,
                    right: operation.Right,
                    exception: exception);

                // (Sparse, Complex)
                Fail(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right,
                    exception: exception);

                // (Dense.AsReadOnly(), Complex)
                Fail(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right,
                    exception: exception);

                // (Sparse.AsReadOnly(), Complex)
                Fail(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right,
                    exception: exception);

                #endregion
            }
        }

        #endregion

        #region Complex left, Double right

        /// <summary>
        /// Provides methods to test  
        /// binary operations having left operands
        /// typed as <see cref="ComplexMatrix"/> or
        /// <see cref="ReadOnlyComplexMatrix"/>,
        /// and right operands
        /// having types <see cref="DoubleMatrix"/> or
        /// <see cref="ReadOnlyDoubleMatrix"/>.
        /// </summary>
        public static class LeftComplexMatrixRightDoubleMatrix
        {
            static void Fail<TLeft, TRight, TException>(
                Func<TLeft, TRight, ComplexMatrix>[] operators,
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
                Func<TLeft, TRight, ComplexMatrix>[] operators,
                TLeft left,
                TRight right,
                ComplexMatrixState expected)
                where TLeft : class
                where TRight : class
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    var actual = operators[i](left, right);
                    ComplexMatrixAssert.IsStateAsExpected(
                        expectedState: expected,
                        actualMatrix: actual,
                        delta: ComplexMatrixTest.Accuracy);
                }
            }

            /// <summary>
            /// Tests an operation
            /// whose left operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void LeftIsNull(TestableComplexMatrixDoubleMatrixOperation<ArgumentNullException> operation)
            {
                var exception = operation.Expected;

                // Dense
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: null,
                    right: operation.Right.AsDense,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: null,
                    right: operation.Right.AsDense,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: null,
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: null,
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // Sparse
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: null,
                    right: operation.Right.AsSparse,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: null,
                    right: operation.Right.AsSparse,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: null,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: null,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);
            }

            /// <summary>
            /// Tests an operation
            /// whose right operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void RightIsNull(TestableComplexMatrixDoubleMatrixOperation<ArgumentNullException> operation)
            {
                var exception = operation.Expected;

                // Dense
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsDense,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsDense,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: null,
                    exception: exception);

                // Sparse
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsSparse,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsSparse,
                    right: null,
                    exception: exception);
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: null,
                    exception: exception);
            }

            /// <summary>
            /// Determines whether the specified operation terminates successfully as expected.
            /// </summary>
            /// <param name="operation">The operation.</param>
            public static void Succeed(TestableComplexMatrixDoubleMatrixOperation<ComplexMatrixState> operation)
            {
                var result = operation.Expected;

                #region (Dense, ...)

                // (Dense, Dense)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsDense,
                    expected: result);

                // (Dense, Sparse)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsSparse,
                    expected: result);

                // (Dense, Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsDense.AsReadOnly(),
                    expected: result);

                // (Dense, Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    expected: result);

                #endregion

                #region (Sparse, ...)

                // (Sparse, Dense)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsDense,
                    expected: result);

                // (Sparse, Sparse)
                Succeed(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsSparse,
                    expected: result);

                // (Sparse, Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsDense.AsReadOnly(),
                    expected: result);

                // (Sparse, Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    expected: result);

                #endregion

                #region (Dense.AsReadOnly(), ...)

                // (Dense.AsReadOnly(), Dense)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsDense,
                    expected: result);

                // (Dense.AsReadOnly(), Sparse)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsSparse,
                    expected: result);

                // (Dense.AsReadOnly(), Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsDense.AsReadOnly(),
                    expected: result);

                // (Dense.AsReadOnly(), Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsSparse.AsReadOnly(),
                    expected: result);

                #endregion

                #region (Sparse.AsReadOnly(), ...)

                // (Sparse.AsReadOnly(), Dense)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsDense,
                    expected: result);

                // (Sparse.AsReadOnly(), Sparse)
                Succeed(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsSparse,
                    expected: result);

                // (Sparse.AsReadOnly(), Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsDense.AsReadOnly(),
                    expected: result);

                // (Sparse.AsReadOnly(), Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsSparse.AsReadOnly(),
                    expected: result);

                #endregion
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(TestableComplexMatrixDoubleMatrixOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                #region (Dense, ...)

                // (Dense, Dense)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsDense,
                    exception: exception);

                // (Dense, Sparse)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsSparse,
                    exception: exception);

                // (Dense, Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // (Dense, Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsDense,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);

                #endregion

                #region (Sparse, ...)

                // (Sparse, Dense)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsDense,
                    exception: exception);

                // (Sparse, Sparse)
                Fail(
                    operators: operation.LeftWritableRightWritableOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsSparse,
                    exception: exception);

                // (Sparse, Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // (Sparse, Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftWritableRightReadOnlyOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);

                #endregion

                #region (Dense.AsReadOnly(), ...)

                // (Dense.AsReadOnly(), Dense)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsDense,
                    exception: exception);

                // (Dense.AsReadOnly(), Sparse)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsSparse,
                    exception: exception);

                // (Dense.AsReadOnly(), Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // (Dense.AsReadOnly(), Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);

                #endregion

                #region (Sparse.AsReadOnly(), ...)

                // (Sparse.AsReadOnly(), Dense)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsDense,
                    exception: exception);

                // (Sparse.AsReadOnly(), Sparse)
                Fail(
                    operators: operation.LeftReadOnlyRightWritableOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsSparse,
                    exception: exception);

                // (Sparse.AsReadOnly(), Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // (Sparse.AsReadOnly(), Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftReadOnlyRightReadOnlyOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);

                #endregion
            }
        }

        /// <summary>
        /// Provides methods to test  
        /// binary operations having left operands
        /// typed as <see cref="Complex"/>,
        /// and right operands
        /// having types <see cref="DoubleMatrix"/> or
        /// <see cref="ReadOnlyDoubleMatrix"/>.
        /// </summary>
        public static class LeftComplexScalarRightDoubleMatrix
        {
            static void Fail<TRight, TException>(
                Func<Complex, TRight, ComplexMatrix>[] operators,
                Complex left,
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
                Func<Complex, TRight, ComplexMatrix>[] operators,
                Complex left,
                TRight right,
                ComplexMatrixState expected)
                where TRight : class
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    var actual = operators[i](left, right);
                    ComplexMatrixAssert.IsStateAsExpected(
                        expectedState: expected,
                        actualMatrix: actual,
                        delta: ComplexMatrixTest.Accuracy);
                }
            }

            /// <summary>
            /// Tests an operation
            /// whose right operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void RightIsNull(TestableComplexScalarDoubleMatrixOperation<ArgumentNullException> operation)
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
            }

            /// <summary>
            /// Determines whether the specified operation terminates successfully as expected.
            /// </summary>
            /// <param name="operation">The operation.</param>
            public static void Succeed(TestableComplexScalarDoubleMatrixOperation<ComplexMatrixState> operation)
            {
                var result = operation.Expected;

                #region (Complex, ...)

                // (Complex, Dense)
                Succeed(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.AsDense,
                    expected: result);

                // (Complex, Sparse)
                Succeed(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.AsSparse,
                    expected: result);

                // (Complex, Dense.AsReadOnly())
                Succeed(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.AsDense.AsReadOnly(),
                    expected: result);

                // (Complex, Sparse.AsReadOnly())
                Succeed(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    expected: result);

                #endregion
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(TestableComplexScalarDoubleMatrixOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                #region (Complex, ...)

                // (Complex, Dense)
                Fail(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.AsDense,
                    exception: exception);

                // (Complex, Sparse)
                Fail(
                    operators: operation.LeftScalarRightWritableOps,
                    left: operation.Left,
                    right: operation.Right.AsSparse,
                    exception: exception);

                // (Complex, Dense.AsReadOnly())
                Fail(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.AsDense.AsReadOnly(),
                    exception: exception);

                // (Complex, Sparse.AsReadOnly())
                Fail(
                    operators: operation.LeftScalarRightReadOnlyOps,
                    left: operation.Left,
                    right: operation.Right.AsSparse.AsReadOnly(),
                    exception: exception);

                #endregion
            }
        }

        /// <summary>
        /// Provides methods to test that 
        /// binary operations having <see cref="ComplexMatrix"/> or
        /// <see cref="ReadOnlyComplexMatrix"/> left operands 
        /// and <see cref="Double"/> right operands
        /// have been properly implemented.
        /// </summary>
        public static class LeftComplexMatrixRightDoubleScalar
        {
            static void Fail<TLeft, TException>(
                Func<TLeft, double, ComplexMatrix>[] operators,
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
                Func<TLeft, double, ComplexMatrix>[] operators,
                TLeft left,
                double right,
                ComplexMatrixState expected)
                where TLeft : class
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    var actual = operators[i](left, right);
                    ComplexMatrixAssert.IsStateAsExpected(
                        expectedState: expected,
                        actualMatrix: actual,
                        delta: ComplexMatrixTest.Accuracy);
                }
            }

            /// <summary>
            /// Tests an operation
            /// whose left operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void LeftIsNull(TestableComplexMatrixDoubleScalarOperation<ArgumentNullException> operation)
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
            }

            /// <summary>
            /// Determines whether the specified operation terminates successfully as expected.
            /// </summary>
            /// <param name="operation">The operation.</param>
            public static void Succeed(TestableComplexMatrixDoubleScalarOperation<ComplexMatrixState> operation)
            {
                var result = operation.Expected;

                #region (..., Complex)

                // (Dense, Complex)
                Succeed(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.AsDense,
                    right: operation.Right,
                    expected: result);

                // (Sparse, Complex)
                Succeed(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right,
                    expected: result);

                // (Dense.AsReadOnly(), Complex)
                Succeed(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right,
                    expected: result);

                // (Sparse.AsReadOnly(), Complex)
                Succeed(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right,
                    expected: result);

                #endregion
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(TestableComplexMatrixDoubleScalarOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                #region (..., Complex)

                // (Dense, Complex)
                Fail(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.AsDense,
                    right: operation.Right,
                    exception: exception);

                // (Sparse, Complex)
                Fail(
                    operators: operation.LeftWritableRightScalarOps,
                    left: operation.Left.AsSparse,
                    right: operation.Right,
                    exception: exception);

                // (Dense.AsReadOnly(), Complex)
                Fail(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.AsDense.AsReadOnly(),
                    right: operation.Right,
                    exception: exception);

                // (Sparse.AsReadOnly(), Complex)
                Fail(
                    operators: operation.LeftReadOnlyRightScalarOps,
                    left: operation.Left.AsSparse.AsReadOnly(),
                    right: operation.Right,
                    exception: exception);

                #endregion
            }
        }

        #endregion
    }
}
