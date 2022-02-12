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
    /// (0,0)  (2,2)  (4,4)         <para /> 
    /// (1,1)  (3,3)  (5,5)         <para /> 
    /// 
    /// l * r  =         <para />
    /// (0,0)  (0,8)  (0,16)    <para /> 
    ///	(0,4)  (0,12)  (0,20)   <para />       
    ///	</summary>
    class TypicalComplexScalarComplexMatrixMultiplication :
        TestableComplexScalarComplexMatrixMultiplication<ComplexMatrixState>
    {
        TypicalComplexScalarComplexMatrixMultiplication() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[6]
                    {
                        0,
                        new Complex(0, 4),
                        new Complex(0, 8),
                        new Complex(0, 12),
                        new Complex(0, 16),
                        new Complex(0, 20)
                    },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: new Complex(2, 2),
                right: TestableComplexMatrix16.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalComplexScalarComplexMatrixMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalComplexScalarComplexMatrixMultiplication"/> class.</returns>
        public static TypicalComplexScalarComplexMatrixMultiplication Get()
        {
            return new TypicalComplexScalarComplexMatrixMultiplication();
        }
    }
}
