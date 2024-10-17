// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Division
{
    /// <summary>
    /// Represents a testable division between a scalar and a matrix.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableComplexScalarDoubleMatrixDivision<TExpected> :
        TestableComplexScalarDoubleMatrixOperation<TExpected>
    {
        public TestableComplexScalarDoubleMatrixDivision(
            TExpected expected,
            Complex left,
            TestableDoubleMatrix right) :
            base(
                expected,
                left,
                right,
                leftScalarRightWritableOps:
                    [
                        (l, r) => l / r,
                        (l, r) => DoubleMatrix.Divide(l, r)
                    ],
                leftScalarRightReadOnlyOps:
                    [
                        (l, r) => l / r,
                        (l, r) => ReadOnlyDoubleMatrix.Divide(l, r)
                    ]
                )
        {
        }
    }

}
