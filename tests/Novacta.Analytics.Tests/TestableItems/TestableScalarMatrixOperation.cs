// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents a binary operation between a scalar and a matrix.
    /// </summary>
    class TestableScalarMatrixOperation<TExpected> :
        TestableBinaryOperation<double, TestableDoubleMatrix, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableScalarMatrixOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <param name="leftWritableRightWritableOps">The left double, right writable matrix ops.</param>
        /// <param name="leftRightReadOnlyOps">The left double, right read only matrix ops.</param>
        protected TestableScalarMatrixOperation(
                    TExpected expected,
                    double left,
                    TestableDoubleMatrix right,
                    Func<double, DoubleMatrix, DoubleMatrix>[]
                        leftScalarRightWritableOps,
                    Func<double, ReadOnlyDoubleMatrix, DoubleMatrix>[]
                        leftScalarRightReadOnlyOps)
        {
            this.Expected = expected;
            this.Left = left;
            this.Right = right;
            this.LeftScalarRightWritableOps = leftScalarRightWritableOps;
            this.LeftScalarRightReadOnlyOps = leftScalarRightReadOnlyOps;
        }


        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="double"/> and right operands of
        /// type <see cref="ReadOnlyDoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<double, ReadOnlyDoubleMatrix, DoubleMatrix>[]
        LeftScalarRightReadOnlyOps
        { get; private set; }


        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="double"/> and right operands of
        /// type <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<double, DoubleMatrix, DoubleMatrix>[]
        LeftScalarRightWritableOps
        { get; private set; }
    }
}
