// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents a testable unary operation whose
    /// operand is a complex matrix.
    /// </summary>
    class TestableComplexMatrixOperation<TExpected> :
        TestableUnaryOperation<TestableComplexMatrix, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableMatrixOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="operand">The operand.</param>
        /// <param name="operandWritableOps">The ops having writable operands.</param>
        /// <param name="operandReadOnlyOps">The ops having read only operands.</param>
        protected TestableComplexMatrixOperation(
                    TExpected expected,
                    TestableComplexMatrix operand,
                    Func<ComplexMatrix, ComplexMatrix>[]
                        operandWritableOps,
                    Func<ReadOnlyComplexMatrix, ComplexMatrix>[]
                        operandReadOnlyOps)
        {
            this.Expected = expected;
            this.Operand = operand;
            this.OperandWritableOps = operandWritableOps;
            this.OperandReadOnlyOps = operandReadOnlyOps;
        }

        /// <summary>
        /// Gets or sets the operators having operands of
        /// type <see cref="ComplexMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ComplexMatrix, ComplexMatrix>[]
        OperandWritableOps
        { get; private set; }


        /// <summary>
        /// Gets or sets the operators having operands of
        /// type <see cref="ReadOnlyComplexMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyComplexMatrix, ComplexMatrix>[]
        OperandReadOnlyOps
        { get; private set; }
    }
}