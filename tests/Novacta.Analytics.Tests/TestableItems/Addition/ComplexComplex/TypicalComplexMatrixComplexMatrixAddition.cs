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
    /// (0,0)  (2,2)  (4,4)          <para /> 
    /// (1,1)  (3,3)  (5,5)          <para /> 
    ///
    /// r   =                  <para /> 
    /// (-5,-5)  (-3,-3)  (-1,-1)       <para /> 
    /// (-4,-4)  (-2,-2)  (-0,-0)       <para /> 
    ///
    /// l + r  =           <para />
    /// (-5,-5)  (-1,-1)  (3,3)    <para /> 
    ///	(-3,-3)  ( 1, 1)  (5,5)    <para />   
    /// </summary>
    /// <seealso cref="TestableComplexMatrixComplexMatrixAddition{ComplexMatrixState}" />
    class TypicalComplexMatrixComplexMatrixAddition :
        TestableComplexMatrixComplexMatrixAddition<ComplexMatrixState>
    {
        TypicalComplexMatrixComplexMatrixAddition() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[6] 
                    { 
                        new Complex(-5, -5), 
                        new Complex(-3, -3),
                        new Complex(-1, -1), 
                        new Complex(1, 1), 
                        new Complex(3, 3),
                        new Complex(5, 5)
                    },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableComplexMatrix16.Get(),
                right: TestableComplexMatrix18.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalComplexMatrixComplexMatrixAddition"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalComplexMatrixComplexMatrixAddition"/> class.</returns>
        public static TypicalComplexMatrixComplexMatrixAddition Get()
        {
            return new TypicalComplexMatrixComplexMatrixAddition();
        }
    }
}
