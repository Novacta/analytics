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
    class TestableDoubleMatrixDoubleMatrixElementWiseMultiplication<TExpected> :
        TestableDoubleMatrixDoubleMatrixOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableDoubleMatrixDoubleMatrixElementWiseMultiplication{TExpected}"/> class.
        /// </summary>
        /// <param name="expected">The expected result or exception.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        public TestableDoubleMatrixDoubleMatrixElementWiseMultiplication(
            TExpected expected,
            TestableDoubleMatrix left,
            TestableDoubleMatrix right) :
            base(
                expected,
                left,
                right,
                leftWritableRightWritableOps:
                    new Func<DoubleMatrix, DoubleMatrix, DoubleMatrix>[1] {
                        (l, r) => DoubleMatrix.ElementWiseMultiply(l, r)
                    },
                leftReadOnlyRightWritableOps:
                    new Func<ReadOnlyDoubleMatrix, DoubleMatrix, DoubleMatrix>[1] {
                        (l, r) => ReadOnlyDoubleMatrix.ElementWiseMultiply(l, r)
                    },
                leftWritableRightReadOnlyOps:
                    new Func<DoubleMatrix, ReadOnlyDoubleMatrix, DoubleMatrix>[1] {
                        (l, r) => ReadOnlyDoubleMatrix.ElementWiseMultiply(l, r)
                    },
                leftReadOnlyRightReadOnlyOps:
                    new Func<ReadOnlyDoubleMatrix, ReadOnlyDoubleMatrix, DoubleMatrix>[1] {
                        (l, r) => ReadOnlyDoubleMatrix.ElementWiseMultiply(l, r)
                    }
                )
        {
        }
    }

}
