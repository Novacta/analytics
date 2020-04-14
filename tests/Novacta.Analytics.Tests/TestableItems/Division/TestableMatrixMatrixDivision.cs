// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Division
{
    /// <summary>
    /// Represents a testable division between matrix operands.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result.</typeparam>
    /// <seealso cref="Tools.TestableMatrixMatrixOperation{TExpected}" />
    class TestableMatrixMatrixDivision<TExpected> :
        TestableMatrixMatrixOperation<TExpected>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestableMatrixMatrixDivision{TExpected}"/> class.
        /// </summary>
        /// <param name="expected">The expected result or exception.</param>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        public TestableMatrixMatrixDivision(
            TExpected expected,
            TestableDoubleMatrix left,
            TestableDoubleMatrix right) :
            base(
                expected,
                left,
                right,
                leftWritableRightWritableOps:
                    new Func<DoubleMatrix, DoubleMatrix, DoubleMatrix>[2] {
                        (l, r) => l / r,
                        (l, r) => DoubleMatrix.Divide(l, r)
                    },
                leftReadOnlyRightWritableOps:
                    new Func<ReadOnlyDoubleMatrix, DoubleMatrix, DoubleMatrix>[2] {
                        (l, r) => l / r,
                        (l, r) => ReadOnlyDoubleMatrix.Divide(l, r)
                    },
                leftWritableRightReadOnlyOps:
                    new Func<DoubleMatrix, ReadOnlyDoubleMatrix, DoubleMatrix>[2] {
                        (l, r) => l / r,
                        (l, r) => ReadOnlyDoubleMatrix.Divide(l, r)
                    },
                leftReadOnlyRightReadOnlyOps:
                    new Func<ReadOnlyDoubleMatrix, ReadOnlyDoubleMatrix, DoubleMatrix>[2] {
                        (l, r) => l / r,
                        (l, r) => ReadOnlyDoubleMatrix.Divide(l, r)
                    }
                )
        {
        }
    }

}
