// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents a testable binary operation having complex matrix 
    /// left operands and double matrix right 
    /// operands.
    /// </summary>
    class TestableComplexMatrixDoubleMatrixOperation<TExpected> :
        TestableBinaryOperation<TestableComplexMatrix, TestableDoubleMatrix, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="TestableComplexMatrixDoubleMatrixOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <param name="leftWritableRightWritableOps">The left writable, right writable ops.</param>
        /// <param name="leftReadOnlyRightWritableOps">The left read only, right writable ops.</param>
        /// <param name="leftWritableRightReadOnlyOps">The left writable, right read only ops.</param>
        /// <param name="leftReadOnlyRightReadOnlyOps">The left read only, right read only ops.</param>
        protected TestableComplexMatrixDoubleMatrixOperation(
                    TExpected expected,
                    TestableComplexMatrix left,
                    TestableDoubleMatrix right,
                    Func<ComplexMatrix, DoubleMatrix, ComplexMatrix>[]
                        leftWritableRightWritableOps,
                    Func<ReadOnlyComplexMatrix, DoubleMatrix, ComplexMatrix>[]
                        leftReadOnlyRightWritableOps,
                    Func<ComplexMatrix, ReadOnlyDoubleMatrix, ComplexMatrix>[]
                        leftWritableRightReadOnlyOps,
                    Func<ReadOnlyComplexMatrix, ReadOnlyDoubleMatrix, ComplexMatrix>[]
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
        /// type <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ComplexMatrix, DoubleMatrix, ComplexMatrix>[]
        LeftWritableRightWritableOps
        { get; private set; }


        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="ReadOnlyComplexMatrix"/> and right operands of
        /// type <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyComplexMatrix, DoubleMatrix, ComplexMatrix>[]
        LeftReadOnlyRightWritableOps
        { get; private set; }

        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="ComplexMatrix"/> and right operands of
        /// type <see cref="ReadOnlyDoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ComplexMatrix, ReadOnlyDoubleMatrix, ComplexMatrix>[]
        LeftWritableRightReadOnlyOps
        { get; private set; }

        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="ReadOnlyComplexMatrix"/> and right operands of
        /// type <see cref="ReadOnlyDoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyComplexMatrix, ReadOnlyDoubleMatrix, ComplexMatrix>[]
        LeftReadOnlyRightReadOnlyOps
        { get; private set; }
    }
}