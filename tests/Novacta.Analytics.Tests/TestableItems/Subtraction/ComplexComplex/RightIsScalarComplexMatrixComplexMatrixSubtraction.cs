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
    /// (0,0)  (2,2)  (4,4)          <para /> 
    /// (1,1)  (3,3)  (5,5)          <para /> 
    ///
    /// r   =      <para /> 
    /// (-1,-1)       <para /> 
    ///
    /// l - r  =         <para />
    /// (1,1)  (3,3)  (5,5)    <para /> 
    ///	(2,2)  (4,4)  (6,6)    <para />       
    ///	</summary>
    class RightIsScalarComplexMatrixComplexMatrixSubtraction :
        TestableComplexMatrixComplexMatrixSubtraction<ComplexMatrixState>
    {
        RightIsScalarComplexMatrixComplexMatrixSubtraction() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray:
                    [
                        new(1, 1),
                        new(2, 2),
                        new(3, 3),
                        new(4, 4),
                        new(5, 5),
                        new(6, 6)
                    ],
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableComplexMatrix16.Get(),
                right: TestableComplexMatrix17.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsScalarComplexMatrixComplexMatrixSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsScalarComplexMatrixComplexMatrixSubtraction"/> class.</returns>
        public static RightIsScalarComplexMatrixComplexMatrixSubtraction Get()
        {
            return new RightIsScalarComplexMatrixComplexMatrixSubtraction();
        }
    }
}
