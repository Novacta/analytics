using System;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Subtraction
{
    /// <summary>
    /// Represents a testable subtraction between a matrix and a scalar.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableComplexMatrixDoubleScalarSubtraction<TExpected> :
        TestableComplexMatrixDoubleScalarOperation<TExpected>
    {
        public TestableComplexMatrixDoubleScalarSubtraction(
            TExpected expected,
            TestableComplexMatrix left,
            double right) :
            base(
                expected,
                left,
                right,
                leftWritableRightScalarOps:
                    new Func<ComplexMatrix, double, ComplexMatrix>[2] {
                        (l, r) => l - r,
                        (l, r) => ComplexMatrix.Subtract(l, r)
                    },
                leftReadOnlyRightScalarOps:
                    new Func<ReadOnlyComplexMatrix, double, ComplexMatrix>[2] {
                        (l, r) => l - r,
                        (l, r) => ReadOnlyComplexMatrix.Subtract(l, r)
                    }
                )
        {
        }
    }

}
