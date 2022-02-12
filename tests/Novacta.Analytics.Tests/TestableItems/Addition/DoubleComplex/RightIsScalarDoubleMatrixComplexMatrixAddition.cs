using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Addition
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// l =                    <para />
    /// 0  2  4          <para /> 
    /// 1  3  5          <para /> 
    ///
    /// r   =      <para /> 
    /// (-1,-1)       <para /> 
    ///
    /// l + r  =         <para />
    /// (-1,-1)  (1,-1)  (3,-1)   <para /> 
    ///	( 0,-1)  (2,-1)  (4,-1)   <para />       
    ///	</summary>
    class RightIsScalarDoubleMatrixComplexMatrixAddition :
        TestableDoubleMatrixComplexMatrixAddition<ComplexMatrixState>
    {
        RightIsScalarDoubleMatrixComplexMatrixAddition() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[6] 
                    {
                        new Complex(-1, -1),
                        new Complex(0, -1), 
                        new Complex(1, -1), 
                        new Complex(2, -1), 
                        new Complex(3, -1),
                        new Complex(4, -1)
                    },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableDoubleMatrix16.Get(),
                right: TestableComplexMatrix17.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsScalarDoubleMatrixComplexMatrixAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsScalarDoubleMatrixComplexMatrixAddition"/> class.</returns>
        public static RightIsScalarDoubleMatrixComplexMatrixAddition Get()
        {
            return new RightIsScalarDoubleMatrixComplexMatrixAddition();
        }
    }
}
