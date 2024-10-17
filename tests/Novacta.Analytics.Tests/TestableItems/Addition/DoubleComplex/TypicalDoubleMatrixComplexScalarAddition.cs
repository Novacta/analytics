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
    /// r   =      <para /> 
    /// (-1,-1)       <para /> 
    ///
    /// l + r  =         <para />
    /// (-1,-1)  (1,-1)  (3,-1)   <para /> 
    ///	( 0,-1)  (2,-1)  (4,-1)   <para />       
    ///	</summary>
    class TypicalDoubleMatrixComplexScalarAddition :
        TestableDoubleMatrixComplexScalarAddition<ComplexMatrixState>
    {
        TypicalDoubleMatrixComplexScalarAddition() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray:
                    [
                        new(-1, -1),
                        new(0, -1), 
                        new(1, -1), 
                        new(2, -1),
                        new(3, -1),
                        new(4, -1)
                    ],
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableDoubleMatrix16.Get(),
                right: new Complex(-1, -1)
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalDoubleMatrixComplexScalarAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalDoubleMatrixComplexScalarAddition"/> class.</returns>
        public static TypicalDoubleMatrixComplexScalarAddition Get()
        {
            return new TypicalDoubleMatrixComplexScalarAddition();
        }
    }
}
