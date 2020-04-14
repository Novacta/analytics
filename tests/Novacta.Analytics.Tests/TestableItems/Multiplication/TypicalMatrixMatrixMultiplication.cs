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
    /// l =                    <para />
    /// 0.0  2.0  4.0          <para /> 
    /// 1.0  3.0  5.0          <para /> 
    ///
    /// r   =       <para /> 
    /// -5.0        <para /> 
    /// -4.0        <para /> 
    /// -3.0        <para /> 
    ///
    /// l * r  =           <para />
    /// -20.0              <para /> 
    ///	-32.0              <para />   
    /// </summary>
    /// <seealso cref="TestableMatrixMatrixMultiplication{DoubleMatrixState}" />
    class TypicalMatrixMatrixMultiplication :
        TestableMatrixMatrixMultiplication<DoubleMatrixState>
    {
        TypicalMatrixMatrixMultiplication() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[2] { -20, -32 },
                    numberOfRows: 2,
                    numberOfColumns: 1),
                left: TestableDoubleMatrix16.Get(),
                right: TestableDoubleMatrix20.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalMatrixMatrixMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalMatrixMatrixMultiplication"/> class.</returns>
        public static TypicalMatrixMatrixMultiplication Get()
        {
            return new TypicalMatrixMatrixMultiplication();
        }
    }
}
