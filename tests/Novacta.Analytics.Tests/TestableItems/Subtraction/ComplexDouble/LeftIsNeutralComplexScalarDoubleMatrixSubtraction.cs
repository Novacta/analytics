// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Subtraction
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    ///
    /// l   =     <para /> 
    /// (0,0)       <para /> 
    ///
    /// r =                    <para />
    /// 0  2  4          <para /> 
    /// 1  3  5          <para /> 
    /// 
    /// l - r  =        <para />
    /// ( 0,0)  (-2,0)  (-4,0)   <para /> 
    ///	(-1,0)  (-3,0)  (-5,0)   <para />       
    ///	</summary>
    class LeftIsNeutralComplexScalarDoubleMatrixSubtraction :
        TestableComplexScalarDoubleMatrixSubtraction<ComplexMatrixState>
    {
        LeftIsNeutralComplexScalarDoubleMatrixSubtraction() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[6] 
                    { 
                        0, 
                        new Complex(-1, 0), 
                        new Complex(-2, 0), 
                        new Complex(-3, 0), 
                        new Complex(-4, 0),
                        new Complex(-5, 0)
                    },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: 0.0,
                right: TestableDoubleMatrix16.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNeutralComplexScalarDoubleMatrixSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNeutralComplexScalarDoubleMatrixSubtraction"/> class.</returns>
        public static LeftIsNeutralComplexScalarDoubleMatrixSubtraction Get()
        {
            return new LeftIsNeutralComplexScalarDoubleMatrixSubtraction();
        }
    }
}
