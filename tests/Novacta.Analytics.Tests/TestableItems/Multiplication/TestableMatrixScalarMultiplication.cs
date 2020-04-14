using System;

namespace Novacta.Analytics.Tests.TestableItems.Multiplication
{
    /// <summary>
    /// Represents a testable multiplication between a matrix and a scalar.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    /// <seealso cref="Tools.TestableScalarMatrixOperation{TExpected}" />
    class TestableMatrixScalarMultiplication<TExpected> :
        TestableMatrixScalarOperation<TExpected>
    {
        public TestableMatrixScalarMultiplication(
            TExpected expected,
            TestableDoubleMatrix left,
            double right) :
            base(
                expected,
                left,
                right,
                leftWritableRightScalarOps:
                    new Func<DoubleMatrix, double, DoubleMatrix>[2] {
                        (l, r) => l * r,
                        (l, r) => DoubleMatrix.Multiply(l, r)
                    },
                leftReadOnlyRightScalarOps:
                    new Func<ReadOnlyDoubleMatrix, double, DoubleMatrix>[2] {
                        (l, r) => l * r,
                        (l, r) => ReadOnlyDoubleMatrix.Multiply(l, r)
                    }
                )
        {
        }
    }

}
