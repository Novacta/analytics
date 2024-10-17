// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

namespace Novacta.Analytics.Tests.TestableItems.Addition
{
    /// <summary>
    /// Represents a testable addition between a scalar and a matrix.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableDoubleScalarComplexMatrixAddition<TExpected> :
        TestableDoubleScalarComplexMatrixOperation<TExpected>
    {
        public TestableDoubleScalarComplexMatrixAddition(
            TExpected expected,
            double left,
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
