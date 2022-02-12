// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Multiplication
{
    /// <summary>
    /// Represents a testable multiplication between a matrix and a scalar.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableComplexMatrixDoubleScalarMultiplication<TExpected> :
        TestableComplexMatrixDoubleScalarOperation<TExpected>
    {
        public TestableComplexMatrixDoubleScalarMultiplication(
            TExpected expected,
            TestableComplexMatrix left,
            double right) :
            base(
                expected,
                left,
                right,
                leftWritableRightScalarOps:
                    new Func<ComplexMatrix, double, ComplexMatrix>[2] {
                        (l, r) => l * r,
                        (l, r) => ComplexMatrix.Multiply(l, r)
                    },
                leftReadOnlyRightScalarOps:
                    new Func<ReadOnlyComplexMatrix, double, ComplexMatrix>[2] {
                        (l, r) => l * r,
                        (l, r) => ReadOnlyComplexMatrix.Multiply(l, r)
                    }
                )
        {
        }
    }

}
