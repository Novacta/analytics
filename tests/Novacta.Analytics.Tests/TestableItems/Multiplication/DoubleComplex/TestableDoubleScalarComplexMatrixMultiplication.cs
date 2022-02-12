// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Multiplication
{
    /// <summary>
    /// Represents a testable multiplication between a scalar and a matrix.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableDoubleScalarComplexMatrixMultiplication<TExpected> :
        TestableDoubleScalarComplexMatrixOperation<TExpected>
    {
        public TestableDoubleScalarComplexMatrixMultiplication(
            TExpected expected,
            double left,
            TestableComplexMatrix right) :
            base(
                expected,
                left,
                right,
                leftScalarRightWritableOps:
                    new Func<double, ComplexMatrix, ComplexMatrix>[2] {
                        (l, r) => l * r,
                        (l, r) => ComplexMatrix.Multiply(l, r)
                    },
                leftScalarRightReadOnlyOps:
                    new Func<double, ReadOnlyComplexMatrix, ComplexMatrix>[2] {
                        (l, r) => l * r,
                        (l, r) => ReadOnlyComplexMatrix.Multiply(l, r)
                    }
                )
        {
        }
    }

}
