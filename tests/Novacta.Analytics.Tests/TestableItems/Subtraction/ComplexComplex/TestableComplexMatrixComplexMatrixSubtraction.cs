// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Subtraction
{
    /// <summary>
    /// Represents a testable subtraction between matrix operands.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result.</typeparam>
    class TestableComplexMatrixComplexMatrixSubtraction<TExpected> :
        TestableComplexMatrixComplexMatrixOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrixComplexMatrixSubtraction{TExpected}"/> class.
        /// </summary>
        /// <param name="expected">The expected result or exception.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        public TestableComplexMatrixComplexMatrixSubtraction(
            TExpected expected,
            TestableComplexMatrix left,
            TestableComplexMatrix right) :
            base(
                expected,
                left,
                right,
                leftWritableRightWritableOps:
                    new Func<ComplexMatrix, ComplexMatrix, ComplexMatrix>[2] {
                        (l, r) => l - r,
                        (l, r) => ComplexMatrix.Subtract(l, r)
                    },
                leftReadOnlyRightWritableOps:
                    new Func<ReadOnlyComplexMatrix, ComplexMatrix, ComplexMatrix>[2] {
                        (l, r) => l - r,
                        (l, r) => ReadOnlyComplexMatrix.Subtract(l, r)
                    },
                leftWritableRightReadOnlyOps:
                    new Func<ComplexMatrix, ReadOnlyComplexMatrix, ComplexMatrix>[2] {
                        (l, r) => l - r,
                        (l, r) => ReadOnlyComplexMatrix.Subtract(l, r)
                    },
                leftReadOnlyRightReadOnlyOps:
                    new Func<ReadOnlyComplexMatrix, ReadOnlyComplexMatrix, ComplexMatrix>[2] {
                        (l, r) => l - r,
                        (l, r) => ReadOnlyComplexMatrix.Subtract(l, r)
                    }
                )
        {
        }
    }

}
