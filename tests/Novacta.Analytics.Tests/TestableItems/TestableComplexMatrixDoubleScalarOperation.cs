// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents a binary operation between a complex matrix and a 
    /// double scalar.
    /// </summary>
    class TestableComplexMatrixDoubleScalarOperation<TExpected> :
        TestableBinaryOperation<TestableComplexMatrix, double, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrixDoubleScalarOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <param name="leftWritableRightWritableOps">The left writable matrix, right ops.</param>
        /// <param name="leftRightReadOnlyOps">The left read only matrix, right ops.</param>
        protected TestableComplexMatrixDoubleScalarOperation(
                    TExpected expected,
                    TestableComplexMatrix left,
                    double right,
                    Func<ComplexMatrix, double, ComplexMatrix>[]
                        leftWritableRightScalarOps,
                    Func<ReadOnlyComplexMatrix, double, ComplexMatrix>[]
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
        /// type <see cref="ReadOnlyComplexMatrix"/> and right operands of
        /// type <see cref="double"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyComplexMatrix, double, ComplexMatrix>[]
        LeftReadOnlyRightScalarOps
        { get; private set; }


        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="ComplexMatrix"/> and right operands of
        /// type <see cref="double"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ComplexMatrix, double, ComplexMatrix>[]
        LeftWritableRightScalarOps
        { get; private set; }
    }
}
