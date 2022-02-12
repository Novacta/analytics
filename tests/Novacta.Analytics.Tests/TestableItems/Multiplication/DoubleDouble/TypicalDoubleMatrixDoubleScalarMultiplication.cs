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
    /// r   =      <para /> 
    /// 2.0       <para /> 
    ///
    /// l * r  =         <para />
    ///  0.0  4.0   8.0   <para /> 
    ///	 2.0  6.0  10.0   <para />       
    ///	</summary>
    class TypicalDoubleMatrixDoubleScalarMultiplication :
        TestableDoubleMatrixDoubleScalarMultiplication<DoubleMatrixState>
    {
        TypicalDoubleMatrixDoubleScalarMultiplication() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[6] { 0, 2, 4, 6, 8, 10 },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableDoubleMatrix16.Get(),
                right: 2.0
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalDoubleMatrixDoubleScalarMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalDoubleMatrixDoubleScalarMultiplication"/> class.</returns>
        public static TypicalDoubleMatrixDoubleScalarMultiplication Get()
        {
            return new TypicalDoubleMatrixDoubleScalarMultiplication();
        }
    }
}
