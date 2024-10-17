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
    /// r   =      <para /> 
    /// (-1,-1)       <para /> 
    ///
    /// l - r  =         <para />
    /// (1,1)  (3,1)  (5,1)    <para /> 
    ///	(2,1)  (4,1)  (6,1)    <para />       
    ///	</summary>
    class RightIsScalarDoubleMatrixComplexMatrixSubtraction :
        TestableDoubleMatrixComplexMatrixSubtraction<ComplexMatrixState>
    {
        RightIsScalarDoubleMatrixComplexMatrixSubtraction() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray:
                    [
                        new(1, 1),
                        new(2, 1),
                        new(3, 1),
                        new(4, 1),
                        new(5, 1),
                        new(6, 1)
                    ],
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableDoubleMatrix16.Get(),
                right: TestableComplexMatrix17.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsScalarDoubleMatrixComplexMatrixSubtraction"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsScalarDoubleMatrixComplexMatrixSubtraction"/> class.</returns>
        public static RightIsScalarDoubleMatrixComplexMatrixSubtraction Get()
        {
            return new RightIsScalarDoubleMatrixComplexMatrixSubtraction();
        }
    }
}
