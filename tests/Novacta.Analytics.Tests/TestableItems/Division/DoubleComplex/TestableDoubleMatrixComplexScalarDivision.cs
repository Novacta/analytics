using System;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Division
{
    /// <summary>
    /// Represents a testable division between a matrix and a scalar.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableDoubleMatrixComplexScalarDivision<TExpected> :
        TestableDoubleMatrixComplexScalarOperation<TExpected>
    {
        public TestableDoubleMatrixComplexScalarDivision(
            TExpected expected,
            TestableDoubleMatrix left,
            Complex right) :
            base(
                expected,
                left,
                right,
                leftWritableRightScalarOps:
                    new Func<DoubleMatrix, Complex, ComplexMatrix>[2] {
                        (l, r) => l / r,
                        (l, r) => DoubleMatrix.Divide(l, r)
                    },
                leftReadOnlyRightScalarOps:
                    new Func<ReadOnlyDoubleMatrix, Complex, ComplexMatrix>[2] {
                        (l, r) => l / r,
                        (l, r) => ReadOnlyDoubleMatrix.Divide(l, r)
                    }
                )
        {
        }
    }

}
