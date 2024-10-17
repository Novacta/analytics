// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Subtraction
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
    /// l - r  =           <para />
    /// 5.0  5.0  5.0    <para /> 
    ///	5.0  5.0  5.0    <para />   
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixDoubleMatrixSubtraction{DoubleMatrixState}" />
    class TypicalDoubleMatrixDoubleMatrixSubtraction :
        TestableDoubleMatrixDoubleMatrixSubtraction<DoubleMatrixState>
    {
        TypicalDoubleMatrixDoubleMatrixSubtraction() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: [5, 5, 5, 5, 5, 5],
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableDoubleMatrix16.Get(),
                right: TestableDoubleMatrix18.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalDoubleMatrixDoubleMatrixSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalDoubleMatrixDoubleMatrixSubtraction"/> class.</returns>
        public static TypicalDoubleMatrixDoubleMatrixSubtraction Get()
        {
            return new TypicalDoubleMatrixDoubleMatrixSubtraction();
        }
    }
}
