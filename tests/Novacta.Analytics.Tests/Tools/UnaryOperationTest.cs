// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems;
using System;

namespace Novacta.Analytics.Tests.Tools
{
    /// <summary>
    /// Provides methods to test that 
    /// unary operations whose operands
    /// have types <see cref="DoubleMatrix"/>, 
    /// <see cref="ReadOnlyDoubleMatrix"/>,
    /// <see cref="ComplexMatrix"/>, or
    /// <see cref="ReadOnlyComplexMatrix"/> have
    /// been properly implemented.
    /// </summary>
    static class UnaryOperationTest
    {
        #region DoubleMatrix

        static void Fail<TOperand, TException>(
            Func<TOperand, DoubleMatrix>[] operators,
            TOperand operand,
            TException exception)
            where TOperand : class
            where TException : Exception
        {
            for (int i = 0; i < operators.Length; i++)
            {
                ExceptionAssert.Throw(
                    () =>
                    {
                        operators[i](operand);
                    },
                    expectedType: exception.GetType(),
                    expectedMessage: exception.Message);
            }
        }

        static void Succeed<TOperand>(
            Func<TOperand, DoubleMatrix>[] operators,
            TOperand operand,
            DoubleMatrixState expected)
            where TOperand : class
        {
            for (int i = 0; i < operators.Length; i++)
            {
                var actual = operators[i](operand);
                DoubleMatrixAssert.IsStateAsExpected(
                    expectedState: expected,
                    actualMatrix: actual,
                    delta: DoubleMatrixTest.Accuracy);
            }
        }

        /// <summary>
        /// Tests an operation
        /// whose operand is set through a value represented by a <b>null</b> instance.
        /// </summary>
        /// <param name="operation">The operation to test.</param>
        public static void OperandIsNull(TestableDoubleMatrixOperation<ArgumentNullException> operation)
        {
            var exception = operation.Expected;

            // Dense
            Fail(
                operators: operation.OperandWritableOps,
                operand: null,
                exception: exception);
            Fail(
                operators: operation.OperandReadOnlyOps,
                operand: null,
                exception: exception);
        }

        /// <summary>
        /// Determines whether the specified operation terminates successfully as expected.
        /// </summary>
        /// <param name="operation">The operation.</param>
        public static void Succeed(TestableDoubleMatrixOperation<DoubleMatrixState> operation)
        {
            var result = operation.Expected;

            // (Dense)
            Succeed(
                operators: operation.OperandWritableOps,
                operand: operation.Operand.AsDense,
                expected: result);

            // (Sparse)
            Succeed(
                operators: operation.OperandWritableOps,
                operand: operation.Operand.AsSparse,
                expected: result);

            // (Dense.AsReadOnly())
            Succeed(
                operators: operation.OperandReadOnlyOps,
                operand: operation.Operand.AsDense.AsReadOnly(),
                expected: result);

            // (Sparse.AsReadOnly())
            Succeed(
                operators: operation.OperandReadOnlyOps,
                operand: operation.Operand.AsSparse.AsReadOnly(),
                expected: result);
        }

        /// <summary>
        /// Determines whether the specified operation fails as expected.
        /// </summary>
        /// <param name="operation">The operation to test.</param>
        public static void Fail<TException>(TestableDoubleMatrixOperation<TException> operation)
            where TException : Exception
        {
            var exception = operation.Expected;

            // (Dense)
            Fail(
                operators: operation.OperandWritableOps,
                operand: operation.Operand.AsDense,
                exception: exception);

            // (Sparse)
            Fail(
                operators: operation.OperandWritableOps,
                operand: operation.Operand.AsSparse,
                exception: exception);

            // (Dense.AsReadOnly())
            Fail(
                operators: operation.OperandReadOnlyOps,
                operand: operation.Operand.AsDense.AsReadOnly(),
                exception: exception);

            // (Sparse.AsReadOnly())
            Fail(
                operators: operation.OperandReadOnlyOps,
                operand: operation.Operand.AsSparse.AsReadOnly(),
                exception: exception);
        }

        #endregion

        #region ComplexMatrix

        static void Fail<TOperand, TException>(
            Func<TOperand, ComplexMatrix>[] operators,
            TOperand operand,
            TException exception)
            where TOperand : class
            where TException : Exception
        {
            for (int i = 0; i < operators.Length; i++)
            {
                ExceptionAssert.Throw(
                    () =>
                    {
                        operators[i](operand);
                    },
                    expectedType: exception.GetType(),
                    expectedMessage: exception.Message);
            }
        }

        static void Succeed<TOperand>(
            Func<TOperand, ComplexMatrix>[] operators,
            TOperand operand,
            ComplexMatrixState expected)
            where TOperand : class
        {
            for (int i = 0; i < operators.Length; i++)
            {
                var actual = operators[i](operand);
                ComplexMatrixAssert.IsStateAsExpected(
                    expectedState: expected,
                    actualMatrix: actual,
                    delta: ComplexMatrixTest.Accuracy);
            }
        }

        /// <summary>
        /// Tests an operation
        /// whose operand is set through a value represented by a <b>null</b> instance.
        /// </summary>
        /// <param name="operation">The operation to test.</param>
        public static void OperandIsNull(TestableComplexMatrixOperation<ArgumentNullException> operation)
        {
            var exception = operation.Expected;

            // Dense
            Fail(
                operators: operation.OperandWritableOps,
                operand: null,
                exception: exception);
            Fail(
                operators: operation.OperandReadOnlyOps,
                operand: null,
                exception: exception);
        }

        /// <summary>
        /// Determines whether the specified operation terminates successfully as expected.
        /// </summary>
        /// <param name="operation">The operation.</param>
        public static void Succeed(TestableComplexMatrixOperation<ComplexMatrixState> operation)
        {
            var result = operation.Expected;

            // (Dense)
            Succeed(
                operators: operation.OperandWritableOps,
                operand: operation.Operand.AsDense,
                expected: result);

            // (Sparse)
            Succeed(
                operators: operation.OperandWritableOps,
                operand: operation.Operand.AsSparse,
                expected: result);

            // (Dense.AsReadOnly())
            Succeed(
                operators: operation.OperandReadOnlyOps,
                operand: operation.Operand.AsDense.AsReadOnly(),
                expected: result);

            // (Sparse.AsReadOnly())
            Succeed(
                operators: operation.OperandReadOnlyOps,
                operand: operation.Operand.AsSparse.AsReadOnly(),
                expected: result);
        }

        /// <summary>
        /// Determines whether the specified operation fails as expected.
        /// </summary>
        /// <param name="operation">The operation to test.</param>
        public static void Fail<TException>(TestableComplexMatrixOperation<TException> operation)
            where TException : Exception
        {
            var exception = operation.Expected;

            // (Dense)
            Fail(
                operators: operation.OperandWritableOps,
                operand: operation.Operand.AsDense,
                exception: exception);

            // (Sparse)
            Fail(
                operators: operation.OperandWritableOps,
                operand: operation.Operand.AsSparse,
                exception: exception);

            // (Dense.AsReadOnly())
            Fail(
                operators: operation.OperandReadOnlyOps,
                operand: operation.Operand.AsDense.AsReadOnly(),
                exception: exception);

            // (Sparse.AsReadOnly())
            Fail(
                operators: operation.OperandReadOnlyOps,
                operand: operation.Operand.AsSparse.AsReadOnly(),
                exception: exception);
        }

        #endregion

    }
}
