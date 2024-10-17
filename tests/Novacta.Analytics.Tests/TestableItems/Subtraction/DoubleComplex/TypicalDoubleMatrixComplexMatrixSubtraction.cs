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
    /// 0  2  4          <para /> 
    /// 1  3  5          <para /> 
    ///
    /// r   =                  <para /> 
    /// (-5,-5)  (-3,-3)  (-1,-1)       <para /> 
    /// (-4,-4)  (-2,-2)  (-0,-0)       <para /> 
    ///
    /// l - r  =           <para />
    /// (5,5)  (5,3)  (5,1)    <para /> 
    ///	(5,4)  (5,2)  (5,0)    <para />   
    /// </summary>
    /// <seealso cref="TestableDoubleMatrixComplexMatrixSubtraction{ComplexMatrixState}" />
    class TypicalDoubleMatrixComplexMatrixSubtraction :
        TestableDoubleMatrixComplexMatrixSubtraction<ComplexMatrixState>
    {
        TypicalDoubleMatrixComplexMatrixSubtraction() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray:
                    [
                        new(5, 5), 
                        new(5, 4), 
                        new(5, 3), 
                        new(5, 2), 
                        new(5, 1),
                        new(5, 0)
                    ],
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableDoubleMatrix16.Get(),
                right: TestableComplexMatrix18.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalDoubleMatrixComplexMatrixSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalDoubleMatrixComplexMatrixSubtraction"/> class.</returns>
        public static TypicalDoubleMatrixComplexMatrixSubtraction Get()
        {
            return new TypicalDoubleMatrixComplexMatrixSubtraction();
        }
    }
}
