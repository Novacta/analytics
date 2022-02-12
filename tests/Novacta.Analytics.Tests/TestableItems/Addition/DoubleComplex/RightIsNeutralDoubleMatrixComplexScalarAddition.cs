// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Addition
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// l =                    <para />
    /// 0  2  4          <para /> 
    /// 1  3  5          <para /> 
    ///
    /// r   =     <para /> 
    /// (0,0)       <para /> 
    ///
    /// l + r  =        <para />
    /// (0,0)  (2,0)  (4,0)   <para /> 
    ///	(1,0)  (3,0)  (5,0)   <para />       
    ///	</summary>
    class RightIsNeutralDoubleMatrixComplexScalarAddition :
        TestableDoubleMatrixComplexScalarAddition<ComplexMatrixState>
    {
        RightIsNeutralDoubleMatrixComplexScalarAddition() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[6] 
                    { 
                        0, 
                        new Complex(1, 0), 
                        new Complex(2, 0), 
                        new Complex(3, 0), 
                        new Complex(4, 0),
                        new Complex(5, 0)
                    },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableDoubleMatrix16.Get(),
                right: 0.0
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsNeutralDoubleMatrixComplexScalarAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsNeutralDoubleMatrixComplexScalarAddition"/> class.</returns>
        public static RightIsNeutralDoubleMatrixComplexScalarAddition Get()
        {
            return new RightIsNeutralDoubleMatrixComplexScalarAddition();
        }
    }
}
