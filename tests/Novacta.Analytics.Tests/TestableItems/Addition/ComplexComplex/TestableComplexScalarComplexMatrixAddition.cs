// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Addition
{
    /// <summary>
    /// Represents a testable addition between a scalar and a matrix.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableComplexScalarComplexMatrixAddition<TExpected> :
        TestableComplexScalarComplexMatrixOperation<TExpected>
    {
        public TestableComplexScalarComplexMatrixAddition(
            TExpected expected,
            Complex left,
            TestableComplexMatrix right) :
            base(
                expected,
                left,
                right,
                leftScalarRightWritableOps:
                    [
                        (l, r) => l + r,
                        (l, r) => ComplexMatrix.Add(l, r)
                    ],
                leftScalarRightReadOnlyOps:
                    [
                        (l, r) => l + r,
                        (l, r) => ReadOnlyComplexMatrix.Add(l, r)
                    ]
                )
        {
        }
    }

}
