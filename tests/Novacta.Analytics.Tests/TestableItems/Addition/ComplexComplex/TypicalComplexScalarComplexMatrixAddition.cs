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
    ///
    /// l   =      <para /> 
    /// (-1,-1)       <para /> 
    ///
    /// r =                    <para />
    /// (0,0)  (2,2)  (4,4)          <para /> 
    /// (1,1)  (3,3)  (5,5)          <para /> 
    /// 
    /// l + r  =         <para />
    /// (-1,-1)  (1,1)  (3,3)   <para /> 
    ///	( 0, 0)  (2,2)  (4,4)   <para />       
    ///	</summary>
    class TypicalComplexScalarComplexMatrixAddition :
        TestableComplexScalarComplexMatrixAddition<ComplexMatrixState>
    {
        TypicalComplexScalarComplexMatrixAddition() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray:
                    [
                        new(-1, -1),
                        0,
                        new(1, 1),
                        new(2, 2),
                        new(3, 3),
                        new(4, 4)
                    ],
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: new Complex(-1, -1),
                right: TestableComplexMatrix16.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalComplexScalarComplexMatrixAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalComplexScalarComplexMatrixAddition"/> class.</returns>
        public static TypicalComplexScalarComplexMatrixAddition Get()
        {
            return new TypicalComplexScalarComplexMatrixAddition();
        }
    }
}
