// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Addition
{
    /// <summary>
    /// Represents a testable addition between matrix operands.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result.</typeparam>
    class TestableComplexMatrixDoubleMatrixAddition<TExpected> :
        TestableComplexMatrixDoubleMatrixOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableComplexMatrixDoubleMatrixAddition{TExpected}"/> class.
        /// </summary>
        /// <param name="expected">The expected result or exception.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        public TestableComplexMatrixDoubleMatrixAddition(
            TExpected expected,
            TestableComplexMatrix left,
            TestableDoubleMatrix right) :
            base(
                expected,
                left,
                right,
                leftWritableRightWritableOps:
                    new Func<ComplexMatrix, DoubleMatrix, ComplexMatrix>[2] {
                        (l, r) => l + r,
                        (l, r) => ComplexMatrix.Add(l, r)
                    },
                leftReadOnlyRightWritableOps:
                    new Func<ReadOnlyComplexMatrix, DoubleMatrix, ComplexMatrix>[2] {
                        (l, r) => l + r,
                        (l, r) => ReadOnlyComplexMatrix.Add(l, r)
                    },
                leftWritableRightReadOnlyOps:
                    new Func<ComplexMatrix, ReadOnlyDoubleMatrix, ComplexMatrix>[2] {
                        (l, r) => l + r,
                        (l, r) => ComplexMatrix.Add(l, r)
                    },
                leftReadOnlyRightReadOnlyOps:
                    new Func<ReadOnlyComplexMatrix, ReadOnlyDoubleMatrix, ComplexMatrix>[2] {
                        (l, r) => l + r,
                        (l, r) => ReadOnlyComplexMatrix.Add(l, r)
                    }
                )
        {
        }
    }

}
