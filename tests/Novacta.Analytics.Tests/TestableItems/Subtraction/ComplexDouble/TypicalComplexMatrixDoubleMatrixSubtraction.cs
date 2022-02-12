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
    /// -5  -3  -1       <para /> 
    /// -4  -2  -0       <para /> 
    ///
    /// l - r  =           <para />
    /// (5,0)  (5,2)  (5,4)    <para /> 
    ///	(5,1)  (5,3)  (5,5)    <para />   
    /// </summary>
    /// <seealso cref="TestableComplexMatrixDoubleMatrixSubtraction{ComplexMatrixState}" />
    class TypicalComplexMatrixDoubleMatrixSubtraction :
        TestableComplexMatrixDoubleMatrixSubtraction<ComplexMatrixState>
    {
        TypicalComplexMatrixDoubleMatrixSubtraction() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[6] 
                    { 
                        new Complex(5, 0), 
                        new Complex(5, 1), 
                        new Complex(5, 2), 
                        new Complex(5, 3), 
                        new Complex(5, 4),
                        new Complex(5, 5)
                    },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableComplexMatrix16.Get(),
                right: TestableDoubleMatrix18.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalComplexMatrixDoubleMatrixSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalComplexMatrixDoubleMatrixSubtraction"/> class.</returns>
        public static TypicalComplexMatrixDoubleMatrixSubtraction Get()
        {
            return new TypicalComplexMatrixDoubleMatrixSubtraction();
        }
    }
}
