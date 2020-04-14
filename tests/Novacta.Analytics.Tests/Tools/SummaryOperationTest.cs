// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Novacta.Analytics.Tests.TestableItems;
using System;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Provides methods to test that 
    /// summary operations have
    /// been properly implemented.
    /// </summary>
    static class SummaryOperationTest
    {
        /// <summary>
        /// Provides methods to test that 
        /// summary operations adjustable for bias have
        /// been properly implemented.
        /// </summary>
        public static class AdjustableForBias
        {
            static void Fail<TData, TException>(
                Func<TData, bool, double>[] operators,
                TData data,
                bool adjustForBias,
                TException exception)
                where TData : class
                where TException : Exception
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    ExceptionAssert.Throw(
                        () =>
                        {
                            operators[i](data, adjustForBias);
                        },
                        expectedType: exception.GetType(),
                        expectedMessage: exception.Message);
                }
            }

            static void Fail<TData, TException>(
                Func<TData, bool, DataOperation, DoubleMatrix>[] operators,
                TData data,
                bool adjustForBias,
                DataOperation dataOperation,
                TException exception)
                where TData : class
                where TException : Exception
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    ExceptionAssert.Throw(
                        () =>
                        {
                            operators[i](data, adjustForBias, dataOperation);
                        },
                        expectedType: exception.GetType(),
                        expectedMessage: exception.Message);
                }
            }

            static void Succeed<TData>(
                Func<TData, bool, double>[] operators,
                TData data,
                bool adjustForBias,
                double expected)
                where TData : class
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    var actual = operators[i](data, adjustForBias);
                    if (Double.IsNaN(expected))
                    {
                        Assert.IsTrue(Double.IsNaN(actual));
                    }
                    else
                    {
                        Assert.AreEqual(
                            expected: expected,
                            actual: actual,
                            delta: DoubleMatrixTest.Accuracy);
                    }
                }
            }

            static void Succeed<TData>(
                Func<TData, bool, DataOperation, DoubleMatrix>[] operators,
                TData data,
                bool adjustForBias,
                DataOperation dataOperation,
                DoubleMatrixState expected)
                where TData : class
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    var actual = operators[i](data, adjustForBias, dataOperation);
                    DoubleMatrixAssert.IsStateAsExpected(
                        expectedState: expected,
                        actualMatrix: actual,
                        delta: DoubleMatrixTest.Accuracy);
                }
            }

            /// <summary>
            /// Tests an operation
            /// whose data operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void DataIsNull(
                OnAllAdjustableForBiasSummaryOperation<ArgumentNullException> operation)
            {
                var exception = operation.Expected;

                Fail(
                    operators: operation.DataWritableOps,
                    data: null,
                    adjustForBias: operation.AdjustForBias,
                    exception: exception);
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: null,
                    adjustForBias: operation.AdjustForBias,
                    exception: exception);
            }

            /// <summary>
            /// Tests an operation
            /// whose data operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void DataIsNull(
                AlongDimensionAdjustableForBiasSummaryOperation<ArgumentNullException> operation)
            {
                var exception = operation.Expected;

                Fail(
                    operators: operation.DataWritableOps,
                    data: null,
                    adjustForBias: operation.AdjustForBias,
                    dataOperation: operation.DataOperation,
                    exception: exception);
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: null,
                    adjustForBias: operation.AdjustForBias,
                    dataOperation: operation.DataOperation,
                    exception: exception);
            }

            /// <summary>
            /// Determines whether the specified operation terminates successfully as expected.
            /// </summary>
            /// <param name="operation">The operation.</param>
            public static void Succeed(
                OnAllAdjustableForBiasSummaryOperation<double> operation)
            {
                var result = operation.Expected;

                // Dense
                Succeed(
                    operators: operation.DataWritableOps,
                    data: operation.Data.Dense,
                    adjustForBias: operation.AdjustForBias,
                    expected: result);

                // Sparse
                Succeed(
                    operators: operation.DataWritableOps,
                    data: operation.Data.Sparse,
                    adjustForBias: operation.AdjustForBias,
                    expected: result);

                // View
                Succeed(
                    operators: operation.DataWritableOps,
                    data: operation.Data.View,
                    adjustForBias: operation.AdjustForBias,
                    expected: result);

                // Dense.AsReadOnly()
                Succeed(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.Dense.AsReadOnly(),
                    adjustForBias: operation.AdjustForBias,
                    expected: result);

                // Sparse.AsReadOnly()
                Succeed(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.Sparse.AsReadOnly(),
                    adjustForBias: operation.AdjustForBias,
                    expected: result);

                // View.AsReadOnly()
                Succeed(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.View.AsReadOnly(),
                    adjustForBias: operation.AdjustForBias,
                    expected: result);
            }

            /// <summary>
            /// Determines whether the specified operation terminates successfully as expected.
            /// </summary>
            /// <param name="operation">The operation.</param>
            public static void Succeed(
                AlongDimensionAdjustableForBiasSummaryOperation<DoubleMatrixState> operation)
            {
                var result = operation.Expected;

                // Dense
                Succeed(
                    operators: operation.DataWritableOps,
                    data: operation.Data.Dense,
                    adjustForBias: operation.AdjustForBias,
                    dataOperation: operation.DataOperation,
                    expected: result);

                // Sparse
                Succeed(
                    operators: operation.DataWritableOps,
                    data: operation.Data.Sparse,
                    adjustForBias: operation.AdjustForBias,
                    dataOperation: operation.DataOperation,
                    expected: result);

                // View
                Succeed(
                    operators: operation.DataWritableOps,
                    data: operation.Data.View,
                    adjustForBias: operation.AdjustForBias,
                    dataOperation: operation.DataOperation,
                    expected: result);

                // Dense.AsReadOnly()
                Succeed(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.Dense.AsReadOnly(),
                    adjustForBias: operation.AdjustForBias,
                    dataOperation: operation.DataOperation,
                    expected: result);

                // Sparse.AsReadOnly()
                Succeed(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.Sparse.AsReadOnly(),
                    adjustForBias: operation.AdjustForBias,
                    dataOperation: operation.DataOperation,
                    expected: result);

                // View.AsReadOnly()
                Succeed(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.View.AsReadOnly(),
                    adjustForBias: operation.AdjustForBias,
                    dataOperation: operation.DataOperation,
                    expected: result);
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(
                OnAllAdjustableForBiasSummaryOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                // Dense
                Fail(
                    operators: operation.DataWritableOps,
                    data: operation.Data.Dense,
                    adjustForBias: operation.AdjustForBias,
                    exception: exception);

                // Sparse
                Fail(
                    operators: operation.DataWritableOps,
                    data: operation.Data.Sparse,
                    adjustForBias: operation.AdjustForBias,
                    exception: exception);

                // View
                Fail(
                    operators: operation.DataWritableOps,
                    data: operation.Data.View,
                    adjustForBias: operation.AdjustForBias,
                    exception: exception);

                // Dense.AsReadOnly()
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.Dense.AsReadOnly(),
                    adjustForBias: operation.AdjustForBias,
                    exception: exception);

                // Sparse.AsReadOnly()
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.Sparse.AsReadOnly(),
                    adjustForBias: operation.AdjustForBias,
                    exception: exception);

                // View.AsReadOnly()
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.View.AsReadOnly(),
                    adjustForBias: operation.AdjustForBias,
                    exception: exception);
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(
                AlongDimensionAdjustableForBiasSummaryOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                // Dense
                Fail(
                    operators: operation.DataWritableOps,
                    data: operation.Data.Dense,
                    adjustForBias: operation.AdjustForBias,
                    dataOperation: operation.DataOperation,
                    exception: exception);

                // Sparse
                Fail(
                    operators: operation.DataWritableOps,
                    data: operation.Data.Sparse,
                    adjustForBias: operation.AdjustForBias,
                    dataOperation: operation.DataOperation,
                    exception: exception);

                // View
                Fail(
                    operators: operation.DataWritableOps,
                    data: operation.Data.View,
                    adjustForBias: operation.AdjustForBias,
                    dataOperation: operation.DataOperation,
                    exception: exception);

                // Dense.AsReadOnly()
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.Dense.AsReadOnly(),
                    adjustForBias: operation.AdjustForBias,
                    dataOperation: operation.DataOperation,
                    exception: exception);

                // Sparse.AsReadOnly()
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.Sparse.AsReadOnly(),
                    adjustForBias: operation.AdjustForBias,
                    dataOperation: operation.DataOperation,
                    exception: exception);

                // View.AsReadOnly()
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.View.AsReadOnly(),
                    adjustForBias: operation.AdjustForBias,
                    dataOperation: operation.DataOperation,
                    exception: exception);
            }
        }

        /// <summary>
        /// Provides methods to test that 
        /// summary operations not adjustable for bias have
        /// been properly implemented.
        /// </summary>
        public static class NotAdjustableForBias
        {
            static void Fail<TData, TException>(
                Func<TData, double>[] operators,
                TData data,
                TException exception)
                where TData : class
                where TException : Exception
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    ExceptionAssert.Throw(
                        () =>
                        {
                            operators[i](data);
                        },
                        expectedType: exception.GetType(),
                        expectedMessage: exception.Message);
                }
            }

            static void Fail<TData, TException>(
                Func<TData, DataOperation, DoubleMatrix>[] operators,
                TData data,
                DataOperation dataOperation,
                TException exception)
                where TData : class
                where TException : Exception
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    ExceptionAssert.Throw(
                        () =>
                        {
                            operators[i](data, dataOperation);
                        },
                        expectedType: exception.GetType(),
                        expectedMessage: exception.Message);
                }
            }

            static void Succeed<TData>(
                Func<TData, double>[] operators,
                TData data,
                double expected)
                where TData : class
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    var actual = operators[i](data);
                    Assert.AreEqual(
                        expected: expected,
                        actual: actual,
                        delta: DoubleMatrixTest.Accuracy);
                }
            }

            static void Succeed<TData>(
                Func<TData, DataOperation, DoubleMatrix>[] operators,
                TData data,
                DataOperation dataOperation,
                DoubleMatrixState expected)
                where TData : class
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    var actual = operators[i](data, dataOperation);
                    DoubleMatrixAssert.IsStateAsExpected(
                        expectedState: expected,
                        actualMatrix: actual,
                        delta: DoubleMatrixTest.Accuracy);
                }
            }

            /// <summary>
            /// Tests an operation
            /// whose data operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void DataIsNull(
                OnAllNotAdjustableForBiasSummaryOperation<ArgumentNullException> operation)
            {
                var exception = operation.Expected;

                Fail(
                    operators: operation.DataWritableOps,
                    data: null,
                    exception: exception);
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: null,
                    exception: exception);
            }

            /// <summary>
            /// Tests an operation
            /// whose data operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void DataIsNull(
                AlongDimensionNotAdjustableForBiasSummaryOperation<ArgumentNullException> operation)
            {
                var exception = operation.Expected;

                Fail(
                    operators: operation.DataWritableOps,
                    data: null,
                    dataOperation: DataOperation.OnRows,
                    exception: exception);
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: null,
                    dataOperation: DataOperation.OnRows,
                    exception: exception);
            }

            /// <summary>
            /// Determines whether the specified operation terminates successfully as expected.
            /// </summary>
            /// <param name="operation">The operation.</param>
            public static void Succeed(
                OnAllNotAdjustableForBiasSummaryOperation<double> operation)
            {
                var result = operation.Expected;

                // Dense
                Succeed(
                    operators: operation.DataWritableOps,
                    data: operation.Data.Dense,
                    expected: result);

                // Sparse
                Succeed(
                    operators: operation.DataWritableOps,
                    data: operation.Data.Sparse,
                    expected: result);

                // View
                Succeed(
                    operators: operation.DataWritableOps,
                    data: operation.Data.View,
                    expected: result);

                // Dense.AsReadOnly()
                Succeed(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.Dense.AsReadOnly(),
                    expected: result);

                // Sparse.AsReadOnly()
                Succeed(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.Sparse.AsReadOnly(),
                    expected: result);

                // View.AsReadOnly()
                Succeed(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.View.AsReadOnly(),
                    expected: result);
            }

            /// <summary>
            /// Determines whether the specified operation terminates successfully as expected.
            /// </summary>
            /// <param name="operation">The operation.</param>
            public static void Succeed(
                AlongDimensionNotAdjustableForBiasSummaryOperation<DoubleMatrixState> operation)
            {
                var result = operation.Expected;

                // Dense
                Succeed(
                    operators: operation.DataWritableOps,
                    data: operation.Data.Dense,
                    dataOperation: operation.DataOperation,
                    expected: result);

                // Sparse
                Succeed(
                    operators: operation.DataWritableOps,
                    data: operation.Data.Sparse,
                    dataOperation: operation.DataOperation,
                    expected: result);

                // View
                Succeed(
                    operators: operation.DataWritableOps,
                    data: operation.Data.View,
                    dataOperation: operation.DataOperation,
                    expected: result);

                // Dense.AsReadOnly()
                Succeed(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.Dense.AsReadOnly(),
                    dataOperation: operation.DataOperation,
                    expected: result);

                // Sparse.AsReadOnly()
                Succeed(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.Sparse.AsReadOnly(),
                    dataOperation: operation.DataOperation,
                    expected: result);

                // View.AsReadOnly()
                Succeed(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.View.AsReadOnly(),
                    dataOperation: operation.DataOperation,
                    expected: result);
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(
                OnAllNotAdjustableForBiasSummaryOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                // Dense
                Fail(
                    operators: operation.DataWritableOps,
                    data: operation.Data.Dense,
                    exception: exception);

                // Sparse
                Fail(
                    operators: operation.DataWritableOps,
                    data: operation.Data.Sparse,
                    exception: exception);

                // View
                Fail(
                    operators: operation.DataWritableOps,
                    data: operation.Data.View,
                    exception: exception);

                // Dense.AsReadOnly()
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.Dense.AsReadOnly(),
                    exception: exception);

                // Sparse.AsReadOnly()
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.Sparse.AsReadOnly(),
                    exception: exception);

                // View.AsReadOnly()
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.View.AsReadOnly(),
                    exception: exception);
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(
                AlongDimensionNotAdjustableForBiasSummaryOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                // Dense
                Fail(
                    operators: operation.DataWritableOps,
                    data: operation.Data.Dense,
                    dataOperation: operation.DataOperation,
                    exception: exception);

                // Sparse
                Fail(
                    operators: operation.DataWritableOps,
                    data: operation.Data.Sparse,
                    dataOperation: operation.DataOperation,
                    exception: exception);

                // View
                Fail(
                    operators: operation.DataWritableOps,
                    data: operation.Data.View,
                    dataOperation: operation.DataOperation,
                    exception: exception);

                // Dense.AsReadOnly()
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.Dense.AsReadOnly(),
                    dataOperation: operation.DataOperation,
                    exception: exception);

                // Sparse.AsReadOnly()
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.Sparse.AsReadOnly(),
                    dataOperation: operation.DataOperation,
                    exception: exception);

                // View.AsReadOnly()
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.View.AsReadOnly(),
                    dataOperation: operation.DataOperation,
                    exception: exception);
            }
        }
    }
}