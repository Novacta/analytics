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
    /// (-1,-1)       <para /> 
    ///
    /// l + r  =         <para />
    /// (-1,-1)  (1,1)  (3,3)   <para /> 
    ///	( 0, 0)  (2,2)  (4,4)   <para />       
    ///	</summary>
    class RightIsScalarComplexMatrixComplexMatrixAddition :
        TestableComplexMatrixComplexMatrixAddition<ComplexMatrixState>
    {
        RightIsScalarComplexMatrixComplexMatrixAddition() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray:
                    [
                        new(-1, -1),
                        0, 
                        new(1, 1), 
                        new(2, 2), 
                        new(3, 3),
                        new(4, 4)
                    ],
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableComplexMatrix16.Get(),
                right: TestableComplexMatrix17.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsScalarComplexMatrixComplexMatrixAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsScalarComplexMatrixComplexMatrixAddition"/> class.</returns>
        public static RightIsScalarComplexMatrixComplexMatrixAddition Get()
        {
            return new RightIsScalarComplexMatrixComplexMatrixAddition();
        }
    }
}
