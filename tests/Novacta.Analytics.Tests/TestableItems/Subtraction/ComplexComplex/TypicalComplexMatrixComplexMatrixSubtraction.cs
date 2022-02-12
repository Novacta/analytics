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
    /// l =                    <para />
    /// (0,0)  (2,2)  (4,4)          <para /> 
    /// (1,1)  (3,3)  (5,5)          <para /> 
    ///
    /// r   =                  <para /> 
    /// (-5,-5)  (-3,-3)  (-1,-1)       <para /> 
    /// (-4,-4)  (-2,-2)  (-0,-0)       <para /> 
    ///
    /// l - r  =           <para />
    /// (5,5)  (5,5)  (5,5)    <para /> 
    ///	(5,5)  (5,5)  (5,5)    <para />   
    /// </summary>
    /// <seealso cref="TestableComplexMatrixComplexMatrixSubtraction{ComplexMatrixState}" />
    class TypicalComplexMatrixComplexMatrixSubtraction :
        TestableComplexMatrixComplexMatrixSubtraction<ComplexMatrixState>
    {
        TypicalComplexMatrixComplexMatrixSubtraction() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[6] 
                    { 
                        new Complex(5, 5), 
                        new Complex(5, 5), 
                        new Complex(5, 5), 
                        new Complex(5, 5), 
                        new Complex(5, 5),
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
        /// Gets an instance of the <see cref="TypicalComplexMatrixComplexMatrixSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalComplexMatrixComplexMatrixSubtraction"/> class.</returns>
        public static TypicalComplexMatrixComplexMatrixSubtraction Get()
        {
            return new TypicalComplexMatrixComplexMatrixSubtraction();
        }
    }
}
