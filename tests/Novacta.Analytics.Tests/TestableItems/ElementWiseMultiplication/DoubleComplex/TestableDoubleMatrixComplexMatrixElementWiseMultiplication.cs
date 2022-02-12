// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.ElementWiseMultiplication
{
    /// <summary>
    /// Represents a testable element-wise multiplication between matrix operands.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result.</typeparam>
    class TestableDoubleMatrixComplexMatrixElementWiseMultiplication<TExpected> :
        TestableDoubleMatrixComplexMatrixOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrixComplexMatrixElementWiseMultiplication{TExpected}"/> class.
        /// </summary>
        /// <param name="expected">The expected result or exception.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        public TestableDoubleMatrixComplexMatrixElementWiseMultiplication(
            TExpected expected,
            TestableDoubleMatrix left,
            TestableComplexMatrix right) :
            base(
                expected,
                left,
                right,
                leftWritableRightWritableOps:
                    new Func<DoubleMatrix, ComplexMatrix, ComplexMatrix>[1] {
                        (l, r) => ComplexMatrix.ElementWiseMultiply(l, r)
                    },
                leftReadOnlyRightWritableOps:
                    new Func<ReadOnlyDoubleMatrix, ComplexMatrix, ComplexMatrix>[1] {
                        (l, r) => ComplexMatrix.ElementWiseMultiply(l, r)
                    },
                leftWritableRightReadOnlyOps:
                    new Func<DoubleMatrix, ReadOnlyComplexMatrix, ComplexMatrix>[1] {
                        (l, r) => ReadOnlyComplexMatrix.ElementWiseMultiply(l, r)
                    },
                leftReadOnlyRightReadOnlyOps:
                    new Func<ReadOnlyDoubleMatrix, ReadOnlyComplexMatrix, ComplexMatrix>[1] {
                        (l, r) => ReadOnlyComplexMatrix.ElementWiseMultiply(l, r)
                    }
                )
        {
        }
    }

}
