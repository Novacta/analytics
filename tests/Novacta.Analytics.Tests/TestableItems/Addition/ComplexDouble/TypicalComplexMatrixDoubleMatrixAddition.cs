// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Addition
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// l =                    <para />
    /// (0,0)  (2,2)  (4,4)          <para /> 
    /// (1,1)  (3,3)  (5,5)          <para /> 
    ///
    /// r   =                  <para /> 
    /// -5  -3  -1       <para /> 
    /// -4  -2  -0       <para /> 
    ///
    /// l + r  =           <para />
    /// (-5,0)  (-1,2)  (3,4)    <para /> 
    ///	(-3,1)  ( 1,3)  (5,5)    <para />   
    /// </summary>
    /// <seealso cref="TestableComplexMatrixDoubleMatrixAddition{ComplexMatrixState}" />
    class TypicalComplexMatrixDoubleMatrixAddition :
        TestableComplexMatrixDoubleMatrixAddition<ComplexMatrixState>
    {
        TypicalComplexMatrixDoubleMatrixAddition() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray:
                    [
                        new(-5, 0), 
                        new(-3, 1),
                        new(-1, 2), 
                        new(1, 3), 
                        new(3, 4),
                        new(5, 5)
                    ],
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableComplexMatrix16.Get(),
                right: TestableDoubleMatrix18.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalComplexMatrixDoubleMatrixAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalComplexMatrixDoubleMatrixAddition"/> class.</returns>
        public static TypicalComplexMatrixDoubleMatrixAddition Get()
        {
            return new TypicalComplexMatrixDoubleMatrixAddition();
        }
    }
}
