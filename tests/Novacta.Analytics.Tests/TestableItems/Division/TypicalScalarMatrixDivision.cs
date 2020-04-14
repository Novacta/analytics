// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Division
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    ///
    /// l   =      <para /> 
    /// 10         <para /> 
    ///
    /// r =                    <para />
    /// 0.0  2.0  4.0          <para /> 
    /// 1.0  3.0  5.0          <para /> 
    /// 
    /// l / r  =         <para />
    /// Inf   5.0         2.5   <para /> 
    ///	10.0  3.33333333  2.0   <para />       
    ///	</summary>
    class TypicalScalarMatrixDivision :
        TestableScalarMatrixDivision<DoubleMatrixState>
    {
        TypicalScalarMatrixDivision() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[6] 
                        { double.PositiveInfinity, 10, 5, 3.3333333, 2.5, 2 },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: 10,
                right: TestableDoubleMatrix16.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalScalarMatrixDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalScalarMatrixDivision"/> class.</returns>
        public static TypicalScalarMatrixDivision Get()
        {
            return new TypicalScalarMatrixDivision();
        }
    }
}
