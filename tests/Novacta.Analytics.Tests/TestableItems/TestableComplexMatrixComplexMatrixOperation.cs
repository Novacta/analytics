// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents a testable binary operation whose
    /// operands are complex matrices.
    /// </summary>
    class TestableComplexMatrixComplexMatrixOperation<TExpected> :
        TestableBinaryOperation<TestableComplexMatrix, TestableComplexMatrix, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="TestableComplexMatrixComplexMatrixOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <param name="leftWritableRightWritableOps">The left writable, right writable ops.</param>
        /// <param name="leftReadOnlyRightWritableOps">The left read only, right writable ops.</param>
        /// <param name="leftWritableRightReadOnlyOps">The left writable, right read only ops.</param>
        /// <param name="leftReadOnlyRightReadOnlyOps">The left read only, right read only ops.</param>
        protected TestableComplexMatrixComplexMatrixOperation(
                    TExpected expected,
                    TestableComplexMatrix left,
                    TestableComplexMatrix right,
                    Func<ComplexMatrix, ComplexMatrix, ComplexMatrix>[]
                        leftWritableRightWritableOps,
                    Func<ReadOnlyComplexMatrix, ComplexMatrix, ComplexMatrix>[]
                        leftReadOnlyRightWritableOps,
                    Func<ComplexMatrix, ReadOnlyComplexMatrix, ComplexMatrix>[]
                        leftWritableRightReadOnlyOps,
                    Func<ReadOnlyComplexMatrix, ReadOnlyComplexMatrix, ComplexMatrix>[]
                        leftReadOnlyRightReadOnlyOps)
        {
            this.Expected = expected;
            this.Left = left;
            this.Right = right;
            this.LeftWritableRightWritableOps = leftWritableRightWritableOps;
            this.LeftReadOnlyRightWritableOps = leftReadOnlyRightWritableOps;
            this.LeftWritableRightReadOnlyOps = leftWritableRightReadOnlyOps;
            this.LeftReadOnlyRightReadOnlyOps = leftReadOnlyRightReadOnlyOps;
        }


        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="ComplexMatrix"/> and right operands of
        /// type <see cref="ComplexMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ComplexMatrix, ComplexMatrix, ComplexMatrix>[]
        LeftWritableRightWritableOps
        { get; private set; }


        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="ReadOnlyComplexMatrix"/> and right operands of
        /// type <see cref="ComplexMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyComplexMatrix, ComplexMatrix, ComplexMatrix>[]
        LeftReadOnlyRightWritableOps
        { get; private set; }

        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="ComplexMatrix"/> and right operands of
        /// type <see cref="ReadOnlyComplexMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ComplexMatrix, ReadOnlyComplexMatrix, ComplexMatrix>[]
        LeftWritableRightReadOnlyOps
        { get; private set; }

        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="ReadOnlyComplexMatrix"/> and right operands of
        /// type <see cref="ReadOnlyComplexMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyComplexMatrix, ReadOnlyComplexMatrix, ComplexMatrix>[]
        LeftReadOnlyRightReadOnlyOps
        { get; private set; }
    }
}