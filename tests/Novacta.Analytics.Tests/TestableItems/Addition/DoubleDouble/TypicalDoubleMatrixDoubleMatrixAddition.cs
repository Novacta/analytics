// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

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
    /// r   =                  <para /> 
    /// -5.0  -3.0  -1.0       <para /> 
    /// -4.0  -2.0  -0.0       <para /> 
    ///
    /// l + r  =           <para />
    /// -5.0  -1.0  3.0    <para /> 
    ///	-3.0   1.0  5.0    <para />   
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixDoubleMatrixAddition{DoubleMatrixState}" />
    class TypicalDoubleMatrixDoubleMatrixAddition :
        TestableDoubleMatrixDoubleMatrixAddition<DoubleMatrixState>
    {
        TypicalDoubleMatrixDoubleMatrixAddition() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: [-5, -3, -1, 1, 3, 5],
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableDoubleMatrix16.Get(),
                right: TestableDoubleMatrix18.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalDoubleMatrixDoubleMatrixAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalDoubleMatrixDoubleMatrixAddition"/> class.</returns>
        public static TypicalDoubleMatrixDoubleMatrixAddition Get()
        {
            return new TypicalDoubleMatrixDoubleMatrixAddition();
        }
    }
}
