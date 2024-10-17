using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Division
{
    /// <summary>
    /// Represents a testable division between a matrix and a scalar.
    /// </summary>
    /// <typeparam name="TExpected">The type of the expected result or exception.</typeparam>
    class TestableComplexMatrixComplexScalarDivision<TExpected> :
        TestableComplexMatrixComplexScalarOperation<TExpected>
    {
        public TestableComplexMatrixComplexScalarDivision(
            TExpected expected,
            TestableComplexMatrix left,
            Complex right) :
            base(
                expected,
                left,
                right,
                leftWritableRightScalarOps:
                    [
                        (l, r) => l / r,
                        (l, r) => ComplexMatrix.Divide(l, r)
                    ],
                leftReadOnlyRightScalarOps:
                    [
                        (l, r) => l / r,
                        (l, r) => ReadOnlyComplexMatrix.Divide(l, r)
                    ]
                )
        {
        }
    }

}
