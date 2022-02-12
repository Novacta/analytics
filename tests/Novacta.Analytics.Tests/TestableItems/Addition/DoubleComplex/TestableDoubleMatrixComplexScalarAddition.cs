using System;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Addition
{
    /// <summary>
    /// Represents a testable addition between a matrix and a scalar.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableDoubleMatrixComplexScalarAddition<TExpected> :
        TestableDoubleMatrixComplexScalarOperation<TExpected>
    {
        public TestableDoubleMatrixComplexScalarAddition(
            TExpected expected,
            TestableDoubleMatrix left,
            Complex right) :
            base(
                expected,
                left,
                right,
                leftWritableRightScalarOps:
                    new Func<DoubleMatrix, Complex, ComplexMatrix>[2] {
                        (l, r) => l + r,
                        (l, r) => DoubleMatrix.Add(l, r)
                    },
                leftReadOnlyRightScalarOps:
                    new Func<ReadOnlyDoubleMatrix, Complex, ComplexMatrix>[2] {
                        (l, r) => l + r,
                        (l, r) => ReadOnlyDoubleMatrix.Add(l, r)
                    }
                )
        {
        }
    }

}
