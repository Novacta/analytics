// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Multiplication
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// l   =      <para /> 
    /// 2.0       <para /> 
    /// 
    /// r =                    <para />
    /// 0.0  2.0  4.0          <para /> 
    /// 1.0  3.0  5.0          <para /> 
    ///
    /// l * r  =         <para />
    /// 0.0  4.0   8.0   <para /> 
    ///	2.0  6.0  10.0   <para />       
    ///	</summary>
    class LeftIsScalarMatrixMatrixMultiplication :
        TestableMatrixMatrixMultiplication<DoubleMatrixState>
    {
        LeftIsScalarMatrixMatrixMultiplication() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[6] { 0, 2, 4, 6, 8, 10 },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableDoubleMatrix19.Get(),
                right: TestableDoubleMatrix16.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsScalarMatrixMatrixMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsScalarMatrixMatrixMultiplication"/> class.</returns>
        public static LeftIsScalarMatrixMatrixMultiplication Get()
        {
            return new LeftIsScalarMatrixMatrixMultiplication();
        }
    }

}
