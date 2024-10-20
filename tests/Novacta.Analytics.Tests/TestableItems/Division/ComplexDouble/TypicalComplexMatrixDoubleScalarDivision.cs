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
    /// l =                    <para />
    /// (0,0)  (2,2)  (4,4)         <para /> 
    /// (1,1)  (3,3)  (5,5)         <para /> 
    ///
    /// r   =      <para /> 
    /// 2          <para /> 
    ///
    /// l / r  =         <para />
    /// (0  ,0  )  (1  ,1  )  (2  ,2  )   <para /> 
    ///	(0.5,0.5)  (1.5,1.5)  (2.5,2.5)   <para />       
    ///	</summary>
    class TypicalComplexMatrixDoubleScalarDivision :
        TestableComplexMatrixDoubleScalarDivision<ComplexMatrixState>
    {
        TypicalComplexMatrixDoubleScalarDivision() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: [ 
                        0, 
                        new(.5, .5), 
                        new(1, 1),
                         new(1.5, 1.5), 
                        new(2, 2),
                         new(2.5, 2.5)
                     ],
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableComplexMatrix16.Get(),
                right: 2
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalComplexMatrixDoubleScalarDivision"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalComplexMatrixDoubleScalarDivision"/> class.</returns>
        public static TypicalComplexMatrixDoubleScalarDivision Get()
        {
            return new TypicalComplexMatrixDoubleScalarDivision();
        }
    }
}
