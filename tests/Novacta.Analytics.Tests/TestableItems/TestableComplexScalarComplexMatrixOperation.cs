// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems
{
    /// <summary>
    /// Represents a binary operation between a complex scalar and a 
    /// complex matrix.
    /// </summary>
    class TestableComplexScalarComplexMatrixOperation<TExpected> :
        TestableBinaryOperation<Complex, TestableComplexMatrix, TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexScalarComplexMatrixOperation" /> class.
        /// </summary>
        /// <param name="expected">The expected result.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <param name="leftWritableRightWritableOps">The left complex, right writable matrix ops.</param>
        /// <param name="leftRightReadOnlyOps">The left complex, right read only matrix ops.</param>
        protected TestableComplexScalarComplexMatrixOperation(
                    TExpected expected,
                    Complex left,
                    TestableComplexMatrix right,
                    Func<Complex, ComplexMatrix, ComplexMatrix>[]
                        leftScalarRightWritableOps,
                    Func<Complex, ReadOnlyComplexMatrix, ComplexMatrix>[]
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
        /// type <see cref="ReadOnlyComplexMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<Complex, ReadOnlyComplexMatrix, ComplexMatrix>[]
        LeftScalarRightReadOnlyOps
        { get; private set; }


        /// <summary>
        /// Gets or sets the operators having left operands of
        /// type <see cref="Complex"/> and right operands of
        /// type <see cref="ComplexMatrix"/>.
        /// </summary>
        /// <value>The operators.</value>
        public Func<Complex, ComplexMatrix, ComplexMatrix>[]
        LeftScalarRightWritableOps
        { get; private set; }
    }
}
