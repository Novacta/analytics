// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Division
{
    /// <summary>
    /// Represents a testable division between a scalar and a matrix.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableComplexScalarComplexMatrixDivision<TExpected> :
        TestableComplexScalarComplexMatrixOperation<TExpected>
    {
        public TestableComplexScalarComplexMatrixDivision(
            TExpected expected,
            Complex left,
            TestableComplexMatrix right) :
            base(
                expected,
                left,
                right,
                leftScalarRightWritableOps:
                    new Func<Complex, ComplexMatrix, ComplexMatrix>[2] {
                        (l, r) => l / r,
                        (l, r) => ComplexMatrix.Divide(l, r)
                    },
                leftScalarRightReadOnlyOps:
                    new Func<Complex, ReadOnlyComplexMatrix, ComplexMatrix>[2] {
                        (l, r) => l / r,
                        (l, r) => ReadOnlyComplexMatrix.Divide(l, r)
                    }
                )
        {
        }
    }

}
