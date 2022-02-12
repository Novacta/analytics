using System;

namespace Novacta.Analytics.Tests.TestableItems.Subtraction
{
    /// <summary>
    /// Represents a testable subtraction between a matrix and a scalar.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableDoubleMatrixDoubleScalarSubtraction<TExpected> :
        TestableDoubleMatrixDoubleScalarOperation<TExpected>
    {
        public TestableDoubleMatrixDoubleScalarSubtraction(
            TExpected expected,
            TestableDoubleMatrix left,
            double right) :
            base(
                expected,
                left,
                right,
                leftWritableRightScalarOps:
                    new Func<DoubleMatrix, double, DoubleMatrix>[2] {
                        (l, r) => l - r,
                        (l, r) => DoubleMatrix.Subtract(l, r)
                    },
                leftReadOnlyRightScalarOps:
                    new Func<ReadOnlyDoubleMatrix, double, DoubleMatrix>[2] {
                        (l, r) => l - r,
                        (l, r) => ReadOnlyDoubleMatrix.Subtract(l, r)
                    }
                )
        {
        }
    }

}
