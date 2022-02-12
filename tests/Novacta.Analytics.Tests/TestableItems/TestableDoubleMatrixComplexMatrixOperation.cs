// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents a testable binary operation having double matrix 
    /// left operands and complex matrix right 
    /// operands.
    /// </summary>
    class TestableDoubleMatrixComplexMatrixOperation<TExpected> :
        TestableBinaryOperation<TestableDoubleMatrix, TestableComplexMatrix, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrixComplexMatrixOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <param name="leftWritableRightWritableOps">The left writable, right writable ops.</param>
        /// <param name="leftReadOnlyRightWritableOps">The left read only, right writable ops.</param>
        /// <param name="leftWritableRightReadOnlyOps">The left writable, right read only ops.</param>
        /// <param name="leftReadOnlyRightReadOnlyOps">The left read only, right read only ops.</param>
        protected TestableDoubleMatrixComplexMatrixOperation(
                    TExpected expected,
                    TestableDoubleMatrix left,
                    TestableComplexMatrix right,
                    Func<DoubleMatrix, ComplexMatrix, ComplexMatrix>[]
                        leftWritableRightWritableOps,
                    Func<ReadOnlyDoubleMatrix, ComplexMatrix, ComplexMatrix>[]
                        leftReadOnlyRightWritableOps,
                    Func<DoubleMatrix, ReadOnlyComplexMatrix, ComplexMatrix>[]
                        leftWritableRightReadOnlyOps,
                    Func<ReadOnlyDoubleMatrix, ReadOnlyComplexMatrix, ComplexMatrix>[]
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
        /// type <see cref="DoubleMatrix"/> and right operands of
        /// type <see cref="ComplexMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<DoubleMatrix, ComplexMatrix, ComplexMatrix>[]
        LeftWritableRightWritableOps
        { get; private set; }


        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="ReadOnlyDoubleMatrix"/> and right operands of
        /// type <see cref="ComplexMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyDoubleMatrix, ComplexMatrix, ComplexMatrix>[]
        LeftReadOnlyRightWritableOps
        { get; private set; }

        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="DoubleMatrix"/> and right operands of
        /// type <see cref="ReadOnlyComplexMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<DoubleMatrix, ReadOnlyComplexMatrix, ComplexMatrix>[]
        LeftWritableRightReadOnlyOps
        { get; private set; }

        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="ReadOnlyDoubleMatrix"/> and right operands of
        /// type <see cref="ReadOnlyComplexMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyDoubleMatrix, ReadOnlyComplexMatrix, ComplexMatrix>[]
        LeftReadOnlyRightReadOnlyOps
        { get; private set; }
    }
}