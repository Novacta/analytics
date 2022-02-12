// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems;
using System;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Provides methods to test that 
    /// operations for data sorting have
    /// been properly implemented.
    /// </summary>
    static class SortingOperationTest
    {
        /// <summary>
        /// Provides methods to test that 
        /// Sort methods have
        /// been properly implemented.
        /// </summary>
        public static class Sort
        {
            static void Fail<TData, TException>(
                Func<TData, SortDirection, DoubleMatrix>[] operators,
                TData data,
                SortDirection sortDirection,
                TException exception)
                where TData : class
                where TException : Exception
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    ExceptionAssert.Throw(
                        () =>
                        {
                            operators[i](data, sortDirection);
                        },
                        expectedType: exception.GetType(),
                        expectedMessage: exception.Message);
                }
            }

            static void Succeed<TData>(
                Func<TData, SortDirection, DoubleMatrix>[] operators,
                TData data,
                SortDirection sortDirection,
                DoubleMatrixState expected)
                where TData : class
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    var actual = operators[i](data, sortDirection);
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
                SortOperation<ArgumentNullException> operation)
            {
                var exception = operation.Expected;

                Fail(
                    operators: operation.DataWritableOps,
                    data: null,
                    sortDirection: SortDirection.Ascending,
                    exception: exception);
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: null,
                    sortDirection: SortDirection.Descending,
                    exception: exception);
            }

            /// <summary>
            /// Determines whether the specified operation terminates successfully as expected.
            /// </summary>
            /// <param name="operation">The operation.</param>
            public static void Succeed(
                SortOperation<DoubleMatrixState> operation)
            {
                var result = operation.Expected;

                // Dense
                Succeed(
                    operators: operation.DataWritableOps,
                    data: operation.Data.AsDense,
                    sortDirection: operation.SortDirection,
                    expected: result);

                // Sparse
                Succeed(
                    operators: operation.DataWritableOps,
                    data: operation.Data.AsSparse,
                    sortDirection: operation.SortDirection,
                    expected: result);

                // Dense.AsReadOnly()
                Succeed(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.AsDense.AsReadOnly(),
                    sortDirection: operation.SortDirection,
                    expected: result);

                // Sparse.AsReadOnly()
                Succeed(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.AsSparse.AsReadOnly(),
                    sortDirection: operation.SortDirection,
                    expected: result);
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(
                SortOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                // Dense
                Fail(
                    operators: operation.DataWritableOps,
                    data: operation.Data.AsDense,
                    sortDirection: operation.SortDirection,
                    exception: exception);

                // Sparse
                Fail(
                    operators: operation.DataWritableOps,
                    data: operation.Data.AsSparse,
                    sortDirection: operation.SortDirection,
                    exception: exception);

                // Dense.AsReadOnly()
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.AsDense.AsReadOnly(),
                    sortDirection: operation.SortDirection,
                    exception: exception);

                // Sparse.AsReadOnly()
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.AsSparse.AsReadOnly(),
                    sortDirection: operation.SortDirection,
                    exception: exception);
            }
        }

        /// <summary>
        /// Provides methods to test that 
        /// SortIndex methods have
        /// been properly implemented.
        /// </summary>
        public static class SortIndex
        {
            static void Fail<TData, TException>(
                Func<TData, SortDirection, SortIndexResults>[] operators,
                TData data,
                SortDirection sortDirection,
                TException exception)
                where TData : class
                where TException : Exception
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    ExceptionAssert.Throw(
                        () =>
                        {
                            operators[i](data, sortDirection);
                        },
                        expectedType: exception.GetType(),
                        expectedMessage: exception.Message);
                }
            }

            static void Succeed<TData>(
                Func<TData, SortDirection, SortIndexResults>[] operators,
                TData data,
                SortDirection sortDirection,
                SortIndexResults expected)
                where TData : class
            {
                for (int i = 0; i < operators.Length; i++)
                {
                    var actual = operators[i](data, sortDirection);
                    DoubleMatrixAssert.AreEqual(
                        expected: expected.SortedData,
                        actual: actual.SortedData,
                        delta: DoubleMatrixTest.Accuracy);
                    IndexCollectionAssert.AreEqual(
                        expected: expected.SortedIndexes,
                        actual: actual.SortedIndexes);
                }
            }

            /// <summary>
            /// Tests an operation
            /// whose data operand is set through a value represented by a <b>null</b> instance.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void DataIsNull(
                SortIndexOperation<ArgumentNullException> operation)
            {
                var exception = operation.Expected;

                Fail(
                    operators: operation.DataWritableOps,
                    data: null,
                    sortDirection: SortDirection.Ascending,
                    exception: exception);
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: null,
                    sortDirection: SortDirection.Descending,
                    exception: exception);
            }

            /// <summary>
            /// Determines whether the specified operation terminates successfully as expected.
            /// </summary>
            /// <param name="operation">The operation.</param>
            public static void Succeed(
                SortIndexOperation<SortIndexResults> operation)
            {
                var result = operation.Expected;

                // Dense
                Succeed(
                    operators: operation.DataWritableOps,
                    data: operation.Data.AsDense,
                    sortDirection: operation.SortDirection,
                    expected: result);

                // Sparse
                Succeed(
                    operators: operation.DataWritableOps,
                    data: operation.Data.AsSparse,
                    sortDirection: operation.SortDirection,
                    expected: result);

                // Dense.AsReadOnly()
                Succeed(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.AsDense.AsReadOnly(),
                    sortDirection: operation.SortDirection,
                    expected: result);

                // Sparse.AsReadOnly()
                Succeed(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.AsSparse.AsReadOnly(),
                    sortDirection: operation.SortDirection,
                    expected: result);
            }

            /// <summary>
            /// Determines whether the specified operation fails as expected.
            /// </summary>
            /// <param name="operation">The operation to test.</param>
            public static void Fail<TException>(
                SortIndexOperation<TException> operation)
                where TException : Exception
            {
                var exception = operation.Expected;

                // Dense
                Fail(
                    operators: operation.DataWritableOps,
                    data: operation.Data.AsDense,
                    sortDirection: operation.SortDirection,
                    exception: exception);

                // Sparse
                Fail(
                    operators: operation.DataWritableOps,
                    data: operation.Data.AsSparse,
                    sortDirection: operation.SortDirection,
                    exception: exception);

                // Dense.AsReadOnly()
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.AsDense.AsReadOnly(),
                    sortDirection: operation.SortDirection,
                    exception: exception);

                // Sparse.AsReadOnly()
                Fail(
                    operators: operation.DataReadOnlyOps,
                    data: operation.Data.AsSparse.AsReadOnly(),
                    sortDirection: operation.SortDirection,
                    exception: exception);
            }
        }
    }
}