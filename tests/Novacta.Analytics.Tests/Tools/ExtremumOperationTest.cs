// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems;
using System;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Provides methods to test that 
    /// operations searching for data extrema have
    /// been properly implemented.
    /// </summary>
    static class ExtremumOperationTest
    {
        static void Fail<TData, TException>(
            Func<TData, IndexValuePair>[] operators,
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
            Func<TData, DataOperation, IndexValuePair[]>[] operators,
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
            Func<TData, IndexValuePair>[] operators,
            TData data,
            IndexValuePair expected)
            where TData : class
        {
            for (int i = 0; i < operators.Length; i++)
            {
                var actual = operators[i](data);
                IndexValuePairAssert.AreEqual(
                    expected: expected,
                    actual: actual,
                    delta: DoubleMatrixTest.Accuracy);
            }
        }

        static void Succeed<TData>(
            Func<TData, DataOperation, IndexValuePair[]>[] operators,
            TData data,
            DataOperation dataOperation,
            IndexValuePair[] expected)
            where TData : class
        {
            for (int i = 0; i < operators.Length; i++)
            {
                var actual = operators[i](data, dataOperation);
                IndexValuePairAssert.AreEqual(
                    expected: expected,
                    actual: actual,
                    delta: DoubleMatrixTest.Accuracy);
            }
        }

        /// <summary>
        /// Tests an operation
        /// whose data operand is set through a value represented by a <b>null</b> instance.
        /// </summary>
        /// <param name="operation">The operation to test.</param>
        public static void DataIsNull(
            OnAllExtremumOperation<ArgumentNullException> operation)
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
            AlongDimensionExtremumOperation<ArgumentNullException> operation)
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
            OnAllExtremumOperation<IndexValuePair> operation)
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
            AlongDimensionExtremumOperation<IndexValuePair[]> operation)
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
            OnAllExtremumOperation<TException> operation)
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
            AlongDimensionExtremumOperation<TException> operation)
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