// Copyright (c) Giovanni Lafratta. All rights reserved.
// Licensed under the MIT license. 
// See the LICENSE file in the project root for more information.

using Novacta.Analytics.Tests.TestableItems.Matrices;
using Novacta.Analytics.Tests.Tools;

namespace Novacta.Analytics.Tests.TestableItems.Multiplication
{
    /// <summary>
    /// Tests the following operation
    /// <para /> 
    /// l =                    <para />
    /// (0,0)  (2,2)  (4,4)           <para /> 
    /// (1,1)  (3,3)  (5,5)           <para /> 
    ///
    /// r   =      <para /> 
    /// 2       <para /> 
    ///
    /// l * r  =         <para />
    /// (0,0)  (4,4)  ( 8, 8)   <para /> 
    ///	(2,2)  (6,6)  (10,10)   <para />       
    ///	</summary>
    class TypicalComplexMatrixDoubleScalarMultiplication :
        TestableComplexMatrixDoubleScalarMultiplication<ComplexMatrixState>
    {
        TypicalComplexMatrixDoubleScalarMultiplication() :
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
                left: TestableComplexMatrix16.Get(),
                right: 2.0
            )
        {
        }

        /// <summary>
        /// Gets an instance of the <see cref="TypicalComplexMatrixDoubleScalarMultiplication"/> class.
        /// </summary>
        /// <returns>An instance of the 
        /// <see cref="TypicalComplexMatrixDoubleScalarMultiplication"/> class.</returns>
        public static TypicalComplexMatrixDoubleScalarMultiplication Get()
        {
            return new TypicalComplexMatrixDoubleScalarMultiplication();
        }
    }
}
