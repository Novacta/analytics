using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Addition
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// l =                    <para />
    /// (0,0)  (2,2)  (4,4)          <para /> 
    /// (1,1)  (3,3)  (5,5)          <para /> 
    ///
    /// r   =      <para /> 
    /// -1         <para /> 
    ///
    /// l + r  =         <para />
    /// (-1,0)  (1,2)  (3,4)   <para /> 
    ///	( 0,1)  (2,3)  (4,5)   <para />       
    ///	</summary>
    class RightIsScalarComplexMatrixDoubleMatrixAddition :
        TestableComplexMatrixDoubleMatrixAddition<ComplexMatrixState>
    {
        RightIsScalarComplexMatrixDoubleMatrixAddition() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray:
                    [
                        new(-1, 0),
                        new(0, 1), 
                        new(1, 2), 
                        new(2, 3), 
                        new(3, 4),
                        new(4, 5)
                    ],
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableComplexMatrix16.Get(),
                right: TestableDoubleMatrix17.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsScalarComplexMatrixDoubleMatrixAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsScalarComplexMatrixDoubleMatrixAddition"/> class.</returns>
        public static RightIsScalarComplexMatrixDoubleMatrixAddition Get()
        {
            return new RightIsScalarComplexMatrixDoubleMatrixAddition();
        }
    }
}
