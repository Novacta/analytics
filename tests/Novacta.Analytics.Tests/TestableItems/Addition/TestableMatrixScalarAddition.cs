using System;

namespace Novacta.Analytics.Tests.TestableItems.Addition
{
    /// <summary>
    /// Represents a testable addition between a matrix and a scalar.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    /// <seealso cref="Tools.TestableScalarMatrixOperation{TExpected}" />
    class TestableMatrixScalarAddition<TExpected> :
        TestableMatrixScalarOperation<TExpected>
    {
        public TestableMatrixScalarAddition(
            TExpected expected,
            TestableDoubleMatrix left,
            double right) :
            base(
                expected,
                left,
                right,
                leftWritableRightScalarOps:
                    new Func<DoubleMatrix, double, DoubleMatrix>[2] {
                        (l, r) => l + r,
                        (l, r) => DoubleMatrix.Add(l, r)
                    },
                leftReadOnlyRightScalarOps:
                    new Func<ReadOnlyDoubleMatrix, double, DoubleMatrix>[2] {
                        (l, r) => l + r,
                        (l, r) => ReadOnlyDoubleMatrix.Add(l, r)
                    }
                )
        {
        }
    }

}
