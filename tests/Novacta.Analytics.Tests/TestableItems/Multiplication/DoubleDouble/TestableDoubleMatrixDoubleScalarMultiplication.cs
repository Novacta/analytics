// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Multiplication
{
    /// <summary>
    /// Represents a testable multiplication between a matrix and a scalar.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableDoubleMatrixDoubleScalarMultiplication<TExpected> :
        TestableDoubleMatrixDoubleScalarOperation<TExpected>
    {
        public TestableDoubleMatrixDoubleScalarMultiplication(
            TExpected expected,
            TestableDoubleMatrix left,
            double right) :
            base(
                expected,
                left,
                right,
                leftWritableRightScalarOps:
                    [
                        (l, r) => l * r,
                        (l, r) => DoubleMatrix.Multiply(l, r)
                    ],
                leftReadOnlyRightScalarOps:
                    [
                        (l, r) => l * r,
                        (l, r) => ReadOnlyDoubleMatrix.Multiply(l, r)
                    ]
                )
        {
        }
    }

}
