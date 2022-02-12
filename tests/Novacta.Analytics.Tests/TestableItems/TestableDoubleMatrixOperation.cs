// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents a testable unary operation whose
    /// operand is a double matrix.
    /// </summary>
    class TestableDoubleMatrixOperation<TExpected> :
        TestableUnaryOperation<TestableDoubleMatrix, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableMatrixOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="operand">The operand.</param>
        /// <param name="operandWritableOps">The ops having writable operands.</param>
        /// <param name="operandReadOnlyOps">The ops having read only operands.</param>
        protected TestableDoubleMatrixOperation(
                    TExpected expected,
                    TestableDoubleMatrix operand,
                    Func<DoubleMatrix, DoubleMatrix>[]
                        operandWritableOps,
                    Func<ReadOnlyDoubleMatrix, DoubleMatrix>[]
                        operandReadOnlyOps)
        {
            this.Expected = expected;
            this.Operand = operand;
            this.OperandWritableOps = operandWritableOps;
            this.OperandReadOnlyOps = operandReadOnlyOps;
        }

        /// <summary>
        /// Gets or sets the operators having operands of
        /// type <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<DoubleMatrix, DoubleMatrix>[]
        OperandWritableOps
        { get; private set; }


        /// <summary>
        /// Gets or sets the operators having operands of
        /// type <see cref="ReadOnlyDoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyDoubleMatrix, DoubleMatrix>[]
        OperandReadOnlyOps
        { get; private set; }
    }
}