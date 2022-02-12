// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents a binary operation between a double scalar and a 
    /// complex matrix.
    /// </summary>
    class TestableDoubleScalarComplexMatrixOperation<TExpected> :
        TestableBinaryOperation<double, TestableComplexMatrix, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleScalarComplexMatrixOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <param name="leftWritableRightWritableOps">The left double, right writable matrix ops.</param>
        /// <param name="leftRightReadOnlyOps">The left double, right read only matrix ops.</param>
        protected TestableDoubleScalarComplexMatrixOperation(
                    TExpected expected,
                    double left,
                    TestableComplexMatrix right,
                    Func<double, ComplexMatrix, ComplexMatrix>[]
                        leftScalarRightWritableOps,
                    Func<double, ReadOnlyComplexMatrix, ComplexMatrix>[]
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
        /// type <see cref="ReadOnlyComplexMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<double, ReadOnlyComplexMatrix, ComplexMatrix>[]
        LeftScalarRightReadOnlyOps
        { get; private set; }


        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="double"/> and right operands of
        /// type <see cref="ComplexMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<double, ComplexMatrix, ComplexMatrix>[]
        LeftScalarRightWritableOps
        { get; private set; }
    }
}
