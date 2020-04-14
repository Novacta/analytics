using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Addition
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// l =                    <para />
    /// 0.0  2.0  4.0          <para /> 
    /// 1.0  3.0  5.0          <para /> 
    ///
    /// r   =      <para /> 
    /// -1.0       <para /> 
    ///
    /// l + r  =         <para />
    /// -1.0  1.0  3.0   <para /> 
    ///	 0.0  2.0  4.0   <para />       
    ///	</summary>
    class RightIsScalarMatrixMatrixAddition :
        TestableMatrixMatrixAddition<DoubleMatrixState>
    {
        RightIsScalarMatrixMatrixAddition() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[6] { -1, 0, 1, 2, 3, 4 },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableDoubleMatrix16.Get(),
                right: TestableDoubleMatrix17.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsScalarMatrixMatrixAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsScalarMatrixMatrixAddition"/> class.</returns>
        public static RightIsScalarMatrixMatrixAddition Get()
        {
            return new RightIsScalarMatrixMatrixAddition();
        }
    }
}
