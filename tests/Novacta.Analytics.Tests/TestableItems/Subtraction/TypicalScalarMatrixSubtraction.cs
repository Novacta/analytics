﻿// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Subtraction
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    ///
    /// l   =      <para /> 
    /// -1.0       <para /> 
    ///
    /// r =                    <para />
    /// 0.0  2.0  4.0          <para /> 
    /// 1.0  3.0  5.0          <para /> 
    /// 
    /// l - r  =         <para />
    /// -1.0  -3.0  -5.0   <para /> 
    ///	-2.0  -4.0  -6.0   <para />       
    ///	</summary>
    class TypicalScalarMatrixSubtraction :
        TestableScalarMatrixSubtraction<DoubleMatrixState>
    {
        TypicalScalarMatrixSubtraction() :
            base(
                expected: new DoubleMatrixState(
                    asColumnMajorDenseArray: new double[6] { -1, -2, -3, -4, -5, -6 },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: -1.0,
                right: TestableDoubleMatrix16.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalScalarMatrixSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalScalarMatrixSubtraction"/> class.</returns>
        public static TypicalScalarMatrixSubtraction Get()
        {
            return new TypicalScalarMatrixSubtraction();
        }
    }
}
