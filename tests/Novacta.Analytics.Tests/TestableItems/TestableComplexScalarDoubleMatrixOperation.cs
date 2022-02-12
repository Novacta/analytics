// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents a binary operation between a complex scalar and a 
    /// double matrix.
    /// </summary>
    class TestableComplexScalarDoubleMatrixOperation<TExpected> :
        TestableBinaryOperation<Complex, TestableDoubleMatrix, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexScalarDoubleMatrixOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <param name="leftWritableRightWritableOps">The left complex, right writable matrix ops.</param>
        /// <param name="leftRightReadOnlyOps">The left complex, right read only matrix ops.</param>
        protected TestableComplexScalarDoubleMatrixOperation(
                    TExpected expected,
                    Complex left,
                    TestableDoubleMatrix right,
                    Func<Complex, DoubleMatrix, ComplexMatrix>[]
                        leftScalarRightWritableOps,
                    Func<Complex, ReadOnlyDoubleMatrix, ComplexMatrix>[]
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
        /// type <see cref="Complex"/> and right operands of
        /// type <see cref="ReadOnlyDoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<Complex, ReadOnlyDoubleMatrix, ComplexMatrix>[]
        LeftScalarRightReadOnlyOps
        { get; private set; }


        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="Complex"/> and right operands of
        /// type <see cref="DoubleMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<Complex, DoubleMatrix, ComplexMatrix>[]
        LeftScalarRightWritableOps
        { get; private set; }
    }
}
