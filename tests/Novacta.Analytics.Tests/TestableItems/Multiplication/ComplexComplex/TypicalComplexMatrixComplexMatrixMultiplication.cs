// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;
using System.Numerics;

namespace Novacta.Analytics.Tests.TestableItems.Multiplication
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// l =                    <para />
    /// (0,0)  (2,2)  (4,4)          <para /> 
    /// (1,1)  (3,3)  (5,5)          <para /> 
    ///
    /// r   =       <para /> 
    /// (-5,-5)        <para /> 
    /// (-4,-4)        <para /> 
    /// (-3,-3)        <para /> 
    ///
    /// l * r  =           <para />
    /// (0,-40)              <para /> 
    ///	(0,-64)              <para />   
    /// </summary>
    /// <seealso cref="TestableComplexMatrixComplexMatrixMultiplication{ComplexMatrixState}" />
    class TypicalComplexMatrixComplexMatrixMultiplication :
        TestableComplexMatrixComplexMatrixMultiplication<ComplexMatrixState>
    {
        TypicalComplexMatrixComplexMatrixMultiplication() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[2] 
                    {
                        new Complex(0, -40), 
                        new Complex(0, -64) 
                    },
                    numberOfRows: 2,
                    numberOfColumns: 1),
                left: TestableComplexMatrix16.Get(),
                right: TestableComplexMatrix20.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalComplexMatrixComplexMatrixMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalComplexMatrixComplexMatrixMultiplication"/> class.</returns>
        public static TypicalComplexMatrixComplexMatrixMultiplication Get()
        {
            return new TypicalComplexMatrixComplexMatrixMultiplication();
        }
    }
}
