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
    /// l   =     <para /> 
    /// 1.0       <para /> 
    ///
    /// r =                    <para />
    /// (0,0)  (2,2)  (4,4)          <para /> 
    /// (1,1)  (3,3)  (5,5)          <para /> 
    /// 
    /// l * r  =        <para />
    /// (0,0)  (2,2)  (4,4)   <para /> 
    ///	(1,1)  (3,3)  (5,5)   <para />       
    ///	</summary>
    class LeftIsNeutralComplexScalarComplexMatrixMultiplication :
        TestableComplexScalarComplexMatrixMultiplication<ComplexMatrixState>
    {
        LeftIsNeutralComplexScalarComplexMatrixMultiplication() :
            base(
                expected: new ComplexMatrixState(
                    asColumnMajorDenseArray: new Complex[6] 
                    {
                        0,
                        new Complex(1, 1),
                        new Complex(2, 2),
                        new Complex(3, 3),
                        new Complex(4, 4),
                        new Complex(5, 5)
                    },
                    numberOfRows: 2,
                    numberOfColumns: 3),
                left: 1.0,
                right: TestableComplexMatrix16.Get()
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="LeftIsNeutralComplexScalarComplexMatrixMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="LeftIsNeutralComplexScalarComplexMatrixMultiplication"/> class.</returns>
        public static LeftIsNeutralComplexScalarComplexMatrixMultiplication Get()
        {
            return new LeftIsNeutralComplexScalarComplexMatrixMultiplication();
        }
    }
}
