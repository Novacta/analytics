// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.ElementWiseMultiplication
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// l =                    <para />
    /// 0.0  2.0  4.0          <para /> 
    /// 1.0  3.0  5.0          <para /> 
    ///
    /// r   =                  <para /> 
    /// -5.0  -3.0  -1.0       <para /> 
    /// -4.0  -2.0  -0.0       <para /> 
    ///
    /// DoubleMatrix.ElementWiseMultiply(l, r)  => <para />
    ///  0.0  -6.0  -4.0    <para /> 
    ///	-4.0  -6.0   0.0    <para />   
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixDoubleMatrixElementWiseMultiplication{DoubleMatrixState}" />
    class TypicalDoubleMatrixDoubleMatrixElementWiseMultiplication :
        TestableDoubleMatrixDoubleMatrixElementWiseMultiplication<DoubleMatrixState>
    {
        TypicalDoubleMatrixDoubleMatrixElementWiseMultiplication() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: [0, -4, -6, -6, -4, 0],
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableDoubleMatrix16.Get(),
                right: TestableDoubleMatrix18.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalDoubleMatrixDoubleMatrixElementWiseMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalDoubleMatrixDoubleMatrixElementWiseMultiplication"/> class.</returns>
        public static TypicalDoubleMatrixDoubleMatrixElementWiseMultiplication Get()
        {
            return new TypicalDoubleMatrixDoubleMatrixElementWiseMultiplication();
        }
    }
}
