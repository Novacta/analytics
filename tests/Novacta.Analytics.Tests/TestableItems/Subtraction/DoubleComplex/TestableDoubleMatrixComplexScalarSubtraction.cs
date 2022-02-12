using System;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Subtraction
{
    /// <summary>
    /// Represents a testable subtraction between a matrix and a scalar.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableDoubleMatrixComplexScalarSubtraction<TExpected> :
        TestableDoubleMatrixComplexScalarOperation<TExpected>
    {
        public TestableDoubleMatrixComplexScalarSubtraction(
            TExpected expected,
            TestableDoubleMatrix left,
            Complex right) :
            base(
                expected,
                left,
                right,
                leftWritableRightScalarOps:
                    new Func<DoubleMatrix, Complex, ComplexMatrix>[2] {
                        (l, r) => l - r,
                        (l, r) => DoubleMatrix.Subtract(l, r)
                    },
                leftReadOnlyRightScalarOps:
                    new Func<ReadOnlyDoubleMatrix, Complex, ComplexMatrix>[2] {
                        (l, r) => l - r,
                        (l, r) => ReadOnlyDoubleMatrix.Subtract(l, r)
                    }
                )
        {
        }
    }

}
