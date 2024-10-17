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
    ///
    /// l   =      <para /> 
    /// (2,2)       <para /> 
    ///
    /// r =                    <para />
    /// 0  2  4          <para /> 
    /// 1  3  5          <para /> 
    /// 
    /// l * r  =         <para />
    /// (0,0)  (4,4)  ( 8, 8)  <para /> 
    ///	(2,2)  (6,6)  (10,10)  <para />       
    ///	</summary>
    class TypicalComplexScalarDoubleMatrixMultiplication :
        TestableComplexScalarDoubleMatrixMultiplication<ComplexMatrixState>
    {
        TypicalComplexScalarDoubleMatrixMultiplication() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray:
                    [
                        0,
                        new(2, 2),
                        new(4, 4),
                        new(6, 6),
                        new(8, 8),
                        new(10, 10)
                    ],
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: new Complex(2, 2),
                right: TestableDoubleMatrix16.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalComplexScalarDoubleMatrixMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalComplexScalarDoubleMatrixMultiplication"/> class.</returns>
        public static TypicalComplexScalarDoubleMatrixMultiplication Get()
        {
            return new TypicalComplexScalarDoubleMatrixMultiplication();
        }
    }
}
