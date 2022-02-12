// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents a testable binary operation whose
    /// operands are double matrices.
    /// </summary>
    class TestableDoubleMatrixDoubleMatrixOperation<TExpected> :
        TestableBinaryOperation<TestableDoubleMatrix, TestableDoubleMatrix, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of 
        /// the <see cref="TestableDoubleMatrixDoubleMatrixOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <param name="leftWritableRightWritableOps">The left writable, right writable ops.</param>
        /// <param name="leftReadOnlyRightWritableOps">The left read only, right writable ops.</param>
        /// <param name="leftWritableRightReadOnlyOps">The left writable, right read only ops.</param>
        /// <param name="leftReadOnlyRightReadOnlyOps">The left read only, right read only ops.</param>
        protected TestableDoubleMatrixDoubleMatrixOperation(
                    TExpected expected,
                    TestableDoubleMatrix left,
                    TestableDoubleMatrix right,
                    Func<DoubleMatrix, DoubleMatrix, DoubleMatrix>[]
                        leftWritableRightWritableOps,
                    Func<ReadOnlyDoubleMatrix, DoubleMatrix, DoubleMatrix>[]
                        leftReadOnlyRightWritableOps,
                    Func<DoubleMatrix, ReadOnlyDoubleMatrix, DoubleMatrix>[]
                        leftWritableRightReadOnlyOps,
                    Func<ReadOnlyDoubleMatrix, ReadOnlyDoubleMatrix, DoubleMatrix>[]
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
        /// type <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<DoubleMatrix, DoubleMatrix, DoubleMatrix>[]
        LeftWritableRightWritableOps
        { get; private set; }


        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="ReadOnlyDoubleMatrix"/> and right operands of
        /// type <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyDoubleMatrix, DoubleMatrix, DoubleMatrix>[]
        LeftReadOnlyRightWritableOps
        { get; private set; }

        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="DoubleMatrix"/> and right operands of
        /// type <see cref="ReadOnlyDoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<DoubleMatrix, ReadOnlyDoubleMatrix, DoubleMatrix>[]
        LeftWritableRightReadOnlyOps
        { get; private set; }

        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="ReadOnlyDoubleMatrix"/> and right operands of
        /// type <see cref="ReadOnlyDoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyDoubleMatrix, ReadOnlyDoubleMatrix, DoubleMatrix>[]
        LeftReadOnlyRightReadOnlyOps
        { get; private set; }
    }
}