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
    /// r   =      <para /> 
    /// 2       <para /> 
    ///
    /// l * r  =         <para />
    /// (0,0)  (4,4)  ( 8, 8)   <para /> 
    ///	(2,2)  (6,6)  (10,10)   <para />       
    ///	</summary>
    class RightIsScalarComplexMatrixDoubleMatrixMultiplication :
        TestableComplexMatrixDoubleMatrixMultiplication<ComplexMatrixState>
    {
        RightIsScalarComplexMatrixDoubleMatrixMultiplication() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[6]
                    {
                        0,
                        new Complex(2, 2),
                        new Complex(4, 4),
                        new Complex(6, 6),
                        new Complex(8, 8),
                        new Complex(10, 10)
                    },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: TestableComplexMatrix16.Get(),
                right: TestableDoubleMatrix19.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="RightIsScalarComplexMatrixDoubleMatrixMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="RightIsScalarComplexMatrixDoubleMatrixMultiplication"/> class.</returns>
        public static RightIsScalarComplexMatrixDoubleMatrixMultiplication Get()
        {
            return new RightIsScalarComplexMatrixDoubleMatrixMultiplication();
        }
    }
}
