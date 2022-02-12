// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents a binary operation between a complex matrix and a 
    /// complex scalar.
    /// </summary>
    class TestableComplexMatrixComplexScalarOperation<TExpected> :
        TestableBinaryOperation<TestableComplexMatrix, Complex, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrixComplexScalarOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <param name="leftWritableRightWritableOps">The left writable matrix, right Complex ops.</param>
        /// <param name="leftRightReadOnlyOps">The left read only matrix, right Complex ops.</param>
        protected TestableComplexMatrixComplexScalarOperation(
                    TExpected expected,
                    TestableComplexMatrix left,
                    Complex right,
                    Func<ComplexMatrix, Complex, ComplexMatrix>[]
                        leftWritableRightScalarOps,
                    Func<ReadOnlyComplexMatrix, Complex, ComplexMatrix>[]
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
        /// type <see cref="Complex"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyComplexMatrix, Complex, ComplexMatrix>[]
        LeftReadOnlyRightScalarOps
        { get; private set; }


        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="ComplexMatrix"/> and right operands of
        /// type <see cref="Complex"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ComplexMatrix, Complex, ComplexMatrix>[]
        LeftWritableRightScalarOps
        { get; private set; }
    }
}
