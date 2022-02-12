// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Multiplication
{
    /// <summary>
    /// Represents a testable multiplication between matrix operands.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result.</typeparam>
    class TestableComplexMatrixComplexMatrixMultiplication<TExpected> :
        TestableComplexMatrixComplexMatrixOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrixComplexMatrixMultiplication{TExpected}"/> class.
        /// </summary>
        /// <param name="expected">The expected result or exception.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        public TestableComplexMatrixComplexMatrixMultiplication(
            TExpected expected,
            TestableComplexMatrix left,
            TestableComplexMatrix right) :
            base(
                expected,
                left,
                right,
                leftWritableRightWritableOps:
                    new Func<ComplexMatrix, ComplexMatrix, ComplexMatrix>[2] {
                        (l, r) => l * r,
                        (l, r) => ComplexMatrix.Multiply(l, r)
                    },
                leftReadOnlyRightWritableOps:
                    new Func<ReadOnlyComplexMatrix, ComplexMatrix, ComplexMatrix>[2] {
                        (l, r) => l * r,
                        (l, r) => ReadOnlyComplexMatrix.Multiply(l, r)
                    },
                leftWritableRightReadOnlyOps:
                    new Func<ComplexMatrix, ReadOnlyComplexMatrix, ComplexMatrix>[2] {
                        (l, r) => l * r,
                        (l, r) => ReadOnlyComplexMatrix.Multiply(l, r)
                    },
                leftReadOnlyRightReadOnlyOps:
                    new Func<ReadOnlyComplexMatrix, ReadOnlyComplexMatrix, ComplexMatrix>[2] {
                        (l, r) => l * r,
                        (l, r) => ReadOnlyComplexMatrix.Multiply(l, r)
                    }
                )
        {
        }
    }

}
