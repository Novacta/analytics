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
    /// have types <see cref="DoubleMatrix"/>, or
    /// <see cref="ReadOnlyDoubleMatrix"/> have
    /// been properly implemented.
    /// </summary>
    static class UnaryOperationTest
    {
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
        public static void OperandIsNull(TestableMatrixOperation<ArgumentNullException> operation)
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
        public static void Succeed(TestableMatrixOperation<DoubleMatrixState> operation)
        {
            var result = operation.Expected;

            // (Dense)
            Succeed(
                operators: operation.OperandWritableOps,
                operand: operation.Operand.Dense,
                expected: result);

            // (Sparse)
            Succeed(
                operators: operation.OperandWritableOps,
                operand: operation.Operand.Sparse,
                expected: result);

            // (View)
            Succeed(
                operators: operation.OperandWritableOps,
                operand: operation.Operand.View,
                expected: result);

            // (Dense.AsReadOnly())
            Succeed(
                operators: operation.OperandReadOnlyOps,
                operand: operation.Operand.Dense.AsReadOnly(),
                expected: result);

            // (Sparse.AsReadOnly())
            Succeed(
                operators: operation.OperandReadOnlyOps,
                operand: operation.Operand.Sparse.AsReadOnly(),
                expected: result);

            // (View.AsReadOnly())
            Succeed(
                operators: operation.OperandReadOnlyOps,
                operand: operation.Operand.View.AsReadOnly(),
                expected: result);
        }

        /// <summary>
        /// Determines whether the specified operation fails as expected.
        /// </summary>
        /// <param name="operation">The operation to test.</param>
        public static void Fail<TException>(TestableMatrixOperation<TException> operation)
            where TException : Exception
        {
            var exception = operation.Expected;

            // (Dense)
            Fail(
                operators: operation.OperandWritableOps,
                operand: operation.Operand.Dense,
                exception: exception);

            // (Sparse)
            Fail(
                operators: operation.OperandWritableOps,
                operand: operation.Operand.Sparse,
                exception: exception);

            // (View)
            Fail(
                operators: operation.OperandWritableOps,
                operand: operation.Operand.View,
                exception: exception);

            // (Dense.AsReadOnly())
            Fail(
                operators: operation.OperandReadOnlyOps,
                operand: operation.Operand.Dense.AsReadOnly(),
                exception: exception);

            // (Sparse.AsReadOnly())
            Fail(
                operators: operation.OperandReadOnlyOps,
                operand: operation.Operand.Sparse.AsReadOnly(),
                exception: exception);

            // (View.AsReadOnly())
            Fail(
                operators: operation.OperandReadOnlyOps,
                operand: operation.Operand.View.AsReadOnly(),
                exception: exception);
        }
    }
}
