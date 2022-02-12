// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents a binary operation between a double matrix and a 
    /// double scalar.
    /// </summary>
    class TestableDoubleMatrixComplexScalarOperation<TExpected> :
        TestableBinaryOperation<TestableDoubleMatrix, Complex, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrixComplexScalarOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <param name="leftWritableRightWritableOps">The left writable matrix, right double ops.</param>
        /// <param name="leftRightReadOnlyOps">The left read only matrix, right double ops.</param>
        protected TestableDoubleMatrixComplexScalarOperation(
                    TExpected expected,
                    TestableDoubleMatrix left,
                    Complex right,
                    Func<DoubleMatrix, Complex, ComplexMatrix>[]
                        leftWritableRightScalarOps,
                    Func<ReadOnlyDoubleMatrix, Complex, ComplexMatrix>[]
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
        /// type <see cref="Complex"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<ReadOnlyDoubleMatrix, Complex, ComplexMatrix>[]
        LeftReadOnlyRightScalarOps
        { get; private set; }


        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="DoubleMatrix"/> and right operands of
        /// type <see cref="Complex"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<DoubleMatrix, Complex, ComplexMatrix>[]
        LeftWritableRightScalarOps
        { get; private set; }
    }
}
