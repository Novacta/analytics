// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using System;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Subtraction
{
    /// <summary>
    /// Represents a testable subtraction between a scalar and a matrix.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableComplexScalarDoubleMatrixSubtraction<TExpected> :
        TestableComplexScalarDoubleMatrixOperation<TExpected>
    {
        public TestableComplexScalarDoubleMatrixSubtraction(
            TExpected expected,
            Complex left,
            TestableDoubleMatrix right) :
            base(
                expected,
                left,
                right,
                leftScalarRightWritableOps:
                    new Func<Complex, DoubleMatrix, ComplexMatrix>[2] {
                        (l, r) => l - r,
                        (l, r) => DoubleMatrix.Subtract(l, r)
                    },
                leftScalarRightReadOnlyOps:
                    new Func<Complex, ReadOnlyDoubleMatrix, ComplexMatrix>[2] {
                        (l, r) => l - r,
                        (l, r) => ReadOnlyDoubleMatrix.Subtract(l, r)
                    }
                )
        {
        }
    }

}
