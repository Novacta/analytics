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
    /// operations searching for quantiles have
    /// been properly implemented.
    /// </summary>
    static class QuantileOperationTest
    {
        static void Fail<TData, TException>(
            Func<TData, DoubleMatrix, DoubleMatrix>[] operators,
            TData data,
            DoubleMatrix probabilities,
            TException exception)
            where TData : class
            where TException : Exception
        {
            for (int i = 0; i < operators.Length; i++)
            {
                ExceptionAssert.Throw(
                    () =>
                    {
                        operators[i](data, probabilities);
                    },
                    expectedType: exception.GetType(),
                    expectedMessage: exception.Message);
            }
        }

        static void Fail<TData, TException>(
            Func<TData, DoubleMatrix, DataOperation, DoubleMatrix[]>[] operators,
            TData data,
            DoubleMatrix probabilities,
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
                        operators[i](data, probabilities, dataOperation);
                    },
                    expectedType: exception.GetType(),
                    expectedMessage: exception.Message);
            }
        }

        static void Succeed<TData>(
            Func<TData, DoubleMatrix, DoubleMatrix>[] operators,
            TData data,
            DoubleMatrix probabilities,
            DoubleMatrixState expected)
            where TData : class
        {
            for (int i = 0; i < operators.Length; i++)
            {
                var actual = operators[i](data, probabilities);
                DoubleMatrixAssert.IsStateAsExpected(
                    expectedState: expected,
                    actualMatrix: actual,
                    delta: DoubleMatrixTest.Accuracy);
            }
        }

        static void Succeed<TData>(
            Func<TData, DoubleMatrix, DataOperation, DoubleMatrix[]>[] operators,
            TData data,
            DoubleMatrix probabilities,
            DataOperation dataOperation,
            DoubleMatrixState[] expected)
            where TData : class
        {
            for (int i = 0; i < operators.Length; i++)
            {
                var actual = operators[i](data, probabilities, dataOperation);

                Assert.AreEqual(expected.Length, actual.Length);

                for (int j = 0; j < expected.Length; j++)
                {
                    DoubleMatrixAssert.IsStateAsExpected(
                        expectedState: expected[j],
                        actualMatrix: actual[j],
                        delta: DoubleMatrixTest.Accuracy);
                }
            }
        }

        /// <summary>
        /// Tests an operation
        /// whose data operand is set through a value represented by a <b>null</b> instance.
        /// </summary>
        /// <param name="operation">The operation to test.</param>
        public static void DataIsNull(
            OnAllQuantileOperation<ArgumentNullException> operation)
        {
            var exception = operation.Expected;
            var probabilities = DoubleMatrix.Dense(1, 3);

            Fail(
                operators: operation.DataWritableOps,
                data: null,
                probabilities: probabilities,
                exception: exception);
            Fail(
                operators: operation.DataReadOnlyOps,
                data: null,
                probabilities: probabilities,
                exception: exception);
        }

        /// <summary>
        /// Tests an operation
        /// whose probabilities operand is set through a value represented by a <b>null</b> instance.
        /// </summary>
        /// <param name="operation">The operation to test.</param>
        public static void ProbabilitiesIsNull(
            OnAllQuantileOperation<ArgumentNullException> operation)
        {
            var exception = operation.Expected;
            var data = DoubleMatrix.Dense(1, 3);

            Fail(
                operators: operation.DataWritableOps,
                data: data,
                probabilities: null,
                exception: exception);
            Fail(
                operators: operation.DataReadOnlyOps,
                data: data.AsReadOnly(),
                probabilities: null,
                exception: exception);
        }

        /// <summary>
        /// Tests an operation
        /// whose data operand is set through a value represented by a <b>null</b> instance.
        /// </summary>
        /// <param name="operation">The operation to test.</param>
        public static void DataIsNull(
            AlongDimensionQuantileOperation<ArgumentNullException> operation)
        {
            var exception = operation.Expected;
            var probabilities = DoubleMatrix.Dense(1, 3);

            Fail(
                operators: operation.DataWritableOps,
                data: null,
                probabilities: probabilities,
                dataOperation: DataOperation.OnRows,
                exception: exception);
            Fail(
                operators: operation.DataReadOnlyOps,
                data: null,
                probabilities: probabilities,
                dataOperation: DataOperation.OnRows,
                exception: exception);
        }

        /// <summary>
        /// Tests an operation
        /// whose probabilities operand is set through a value represented by a <b>null</b> instance.
        /// </summary>
        /// <param name="operation">The operation to test.</param>
        public static void ProbabilitiesIsNull(
            AlongDimensionQuantileOperation<ArgumentNullException> operation)
        {
            var exception = operation.Expected;
            var data = DoubleMatrix.Dense(1, 3);

            Fail(
                operators: operation.DataWritableOps,
                data: data,
                probabilities: null,
                dataOperation: DataOperation.OnRows,
                exception: exception);
            Fail(
                operators: operation.DataReadOnlyOps,
                data: data.AsReadOnly(),
                probabilities: null,
                dataOperation: DataOperation.OnRows,
                exception: exception);
        }

        /// <summary>
        /// Determines whether the specified operation terminates successfully as expected.
        /// </summary>
        /// <param name="operation">The operation.</param>
        public static void Succeed(
            OnAllQuantileOperation<DoubleMatrixState> operation)
        {
            var result = operation.Expected;

            // Dense
            Succeed(
                operators: operation.DataWritableOps,
                data: operation.Data.AsDense,
                probabilities: operation.Probabilities,
                expected: result);

            // Sparse
            Succeed(
                operators: operation.DataWritableOps,
                data: operation.Data.AsSparse,
                probabilities: operation.Probabilities,
                expected: result);

            // Dense.AsReadOnly()
            Succeed(
                operators: operation.DataReadOnlyOps,
                data: operation.Data.AsDense.AsReadOnly(),
                probabilities: operation.Probabilities,
                expected: result);

            // Sparse.AsReadOnly()
            Succeed(
                operators: operation.DataReadOnlyOps,
                data: operation.Data.AsSparse.AsReadOnly(),
                probabilities: operation.Probabilities,
                expected: result);
        }

        /// <summary>
        /// Determines whether the specified operation terminates successfully as expected.
        /// </summary>
        /// <param name="operation">The operation.</param>
        public static void Succeed(
            AlongDimensionQuantileOperation<DoubleMatrixState[]> operation)
        {
            var result = operation.Expected;

            // Dense
            Succeed(
                operators: operation.DataWritableOps,
                data: operation.Data.AsDense,
                probabilities: operation.Probabilities,
                dataOperation: operation.DataOperation,
                expected: result);

            // Sparse
            Succeed(
                operators: operation.DataWritableOps,
                data: operation.Data.AsSparse,
                probabilities: operation.Probabilities,
                dataOperation: operation.DataOperation,
                expected: result);

            // Dense.AsReadOnly()
            Succeed(
                operators: operation.DataReadOnlyOps,
                data: operation.Data.AsDense.AsReadOnly(),
                probabilities: operation.Probabilities,
                dataOperation: operation.DataOperation,
                expected: result);

            // Sparse.AsReadOnly()
            Succeed(
                operators: operation.DataReadOnlyOps,
                data: operation.Data.AsSparse.AsReadOnly(),
                probabilities: operation.Probabilities,
                dataOperation: operation.DataOperation,
                expected: result);
        }

        /// <summary>
        /// Determines whether the specified operation fails as expected.
        /// </summary>
        /// <param name="operation">The operation to test.</param>
        public static void Fail<TException>(
            OnAllQuantileOperation<TException> operation)
            where TException : Exception
        {
            var exception = operation.Expected;

            // Dense
            Fail(
                operators: operation.DataWritableOps,
                data: operation.Data.AsDense,
                probabilities: operation.Probabilities,
                exception: exception);

            // Sparse
            Fail(
                operators: operation.DataWritableOps,
                data: operation.Data.AsSparse,
                probabilities: operation.Probabilities,
                exception: exception);

            // Dense.AsReadOnly()
            Fail(
                operators: operation.DataReadOnlyOps,
                data: operation.Data.AsDense.AsReadOnly(),
                probabilities: operation.Probabilities,
                exception: exception);

            // Sparse.AsReadOnly()
            Fail(
                operators: operation.DataReadOnlyOps,
                data: operation.Data.AsSparse.AsReadOnly(),
                probabilities: operation.Probabilities,
                exception: exception);
        }

        /// <summary>
        /// Determines whether the specified operation fails as expected.
        /// </summary>
        /// <param name="operation">The operation to test.</param>
        public static void Fail<TException>(
            AlongDimensionQuantileOperation<TException> operation)
            where TException : Exception
        {
            var exception = operation.Expected;

            // Dense
            Fail(
                operators: operation.DataWritableOps,
                data: operation.Data.AsDense,
                probabilities: operation.Probabilities,
                dataOperation: operation.DataOperation,
                exception: exception);

            // Sparse
            Fail(
                operators: operation.DataWritableOps,
                data: operation.Data.AsSparse,
                probabilities: operation.Probabilities,
                dataOperation: operation.DataOperation,
                exception: exception);

            // Dense.AsReadOnly()
            Fail(
                operators: operation.DataReadOnlyOps,
                data: operation.Data.AsDense.AsReadOnly(),
                probabilities: operation.Probabilities,
                dataOperation: operation.DataOperation,
                exception: exception);

            // Sparse.AsReadOnly()
            Fail(
                operators: operation.DataReadOnlyOps,
                data: operation.Data.AsSparse.AsReadOnly(),
                probabilities: operation.Probabilities,
                dataOperation: operation.DataOperation,
                exception: exception);
        }
    }
}