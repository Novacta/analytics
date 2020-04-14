// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents a binary operation between a matrix and a scalar.
    /// </summary>
    class TestableMatrixScalarOperation<TExpected> :
        TestableBinaryOperation<TestableDoubleMatrix, double, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableMatrixScalarOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <param name="leftWritableRightWritableOps">The left writable matrix, right double ops.</param>
        /// <param name="leftRightReadOnlyOps">The left read only matrix, right double ops.</param>
        protected TestableMatrixScalarOperation(
                    TExpected expected,
                    TestableDoubleMatrix left,
                    double right,
                    Func<DoubleMatrix, double, DoubleMatrix>[]
                        leftWritableRightScalarOps,
                    Func<ReadOnlyDoubleMatrix, double, DoubleMatrix>[]
                        leftReadOnlyRightScalarOps)
        {
            this.Expected = expected;
            this.Left = left;
            this.Right = right;
            this.LeftWritableRightScalarOps = leftWritableRightScalarOps;
            this.LeftReadOnlyRightScalarOps = leftReadOnlyRightScalarOps;
        }


        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="ReadOnlyDoubleMatrix"/> and right operands of
        /// type <see cref="double"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyDoubleMatrix, double, DoubleMatrix>[]
        LeftReadOnlyRightScalarOps
        { get; private set; }


        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="DoubleMatrix"/> and right operands of
        /// type <see cref="double"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<DoubleMatrix, double, DoubleMatrix>[]
        LeftWritableRightScalarOps
        { get; private set; }
    }
}
