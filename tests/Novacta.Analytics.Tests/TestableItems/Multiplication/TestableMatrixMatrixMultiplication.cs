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
    /// <seealso cref="Tools.TestableMatrixMatrixOperation{TExpected}" />
    class TestableMatrixMatrixMultiplication<TExpected> :
        TestableMatrixMatrixOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableMatrixMatrixMultiplication{TExpected}"/> class.
        /// </summary>
        /// <param name="expected">The expected result or exception.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        public TestableMatrixMatrixMultiplication(
            TExpected expected,
            TestableDoubleMatrix left,
            TestableDoubleMatrix right) :
            base(
                expected,
                left,
                right,
                leftWritableRightWritableOps:
                    new Func<DoubleMatrix, DoubleMatrix, DoubleMatrix>[2] {
                        (l, r) => l * r,
                        (l, r) => DoubleMatrix.Multiply(l, r)
                    },
                leftReadOnlyRightWritableOps:
                    new Func<ReadOnlyDoubleMatrix, DoubleMatrix, DoubleMatrix>[2] {
                        (l, r) => l * r,
                        (l, r) => ReadOnlyDoubleMatrix.Multiply(l, r)
                    },
                leftWritableRightReadOnlyOps:
                    new Func<DoubleMatrix, ReadOnlyDoubleMatrix, DoubleMatrix>[2] {
                        (l, r) => l * r,
                        (l, r) => ReadOnlyDoubleMatrix.Multiply(l, r)
                    },
                leftReadOnlyRightReadOnlyOps:
                    new Func<ReadOnlyDoubleMatrix, ReadOnlyDoubleMatrix, DoubleMatrix>[2] {
                        (l, r) => l * r,
                        (l, r) => ReadOnlyDoubleMatrix.Multiply(l, r)
                    }
                )
        {
        }
    }

}
