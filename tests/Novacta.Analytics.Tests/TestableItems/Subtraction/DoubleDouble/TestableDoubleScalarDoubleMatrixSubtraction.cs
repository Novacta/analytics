// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;

namespace Novacta.Analytics.Tests.TestableItems.Subtraction
{
    /// <summary>
    /// Represents a testable subtraction between a scalar and a matrix.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableDoubleScalarDoubleMatrixSubtraction<TExpected> :
        TestableDoubleScalarDoubleMatrixOperation<TExpected>
    {
        public TestableDoubleScalarDoubleMatrixSubtraction(
            TExpected expected,
            double left,
            TestableDoubleMatrix right) :
            base(
                expected,
                left,
                right,
                leftScalarRightWritableOps:
                    new Func<double, DoubleMatrix, DoubleMatrix>[2] {
                        (l, r) => l - r,
                        (l, r) => DoubleMatrix.Subtract(l, r)
                    },
                leftScalarRightReadOnlyOps:
                    new Func<double, ReadOnlyDoubleMatrix, DoubleMatrix>[2] {
                        (l, r) => l - r,
                        (l, r) => ReadOnlyDoubleMatrix.Subtract(l, r)
                    }
                )
        {
        }
    }

}
