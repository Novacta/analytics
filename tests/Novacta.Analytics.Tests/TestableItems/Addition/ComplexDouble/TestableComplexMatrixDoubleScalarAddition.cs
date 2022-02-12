using System;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Addition
{
    /// <summary>
    /// Represents a testable addition between a matrix and a scalar.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableComplexMatrixDoubleScalarAddition<TExpected> :
        TestableComplexMatrixDoubleScalarOperation<TExpected>
    {
        public TestableComplexMatrixDoubleScalarAddition(
            TExpected expected,
            TestableComplexMatrix left,
            double right) :
            base(
                expected,
                left,
                right,
                leftWritableRightScalarOps:
                    new Func<ComplexMatrix, double, ComplexMatrix>[2] {
                        (l, r) => l + r,
                        (l, r) => ComplexMatrix.Add(l, r)
                    },
                leftReadOnlyRightScalarOps:
                    new Func<ReadOnlyComplexMatrix, double, ComplexMatrix>[2] {
                        (l, r) => l + r,
                        (l, r) => ReadOnlyComplexMatrix.Add(l, r)
                    }
                )
        {
        }
    }

}
